using Azure.Core;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using N.Core.Email;
using N.Core.QRCodeProvider;
using N.Extensions;
using N.Model;
using N.Model.Entities;
using N.Repository.FieldAreaRepository;
using N.Repository.NDirectoryRepository;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Common.TokenService;
using N.Service.Constant;
using N.Service.Core.Generator;
using N.Service.Dto;
using N.Service.FieldService.Dto;
using N.Service.UserService.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace N.Service.UserService
{
    public class UserService : Service<AppUser>, IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IFieldAreaRepository _fieldAreaRepository;
        private readonly IDistributedCache _cache;
        private readonly ITokenService _tokenService;
        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IUserRepository userRepository,
            IFieldAreaRepository fieldAreaRepository,
            IDistributedCache cache,
            ITokenService tokenService
            ) : base(userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._fieldAreaRepository = fieldAreaRepository;
            this._cache = cache;
            this._tokenService = tokenService;
        }

        public async Task<AppUser?> GetUser(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return null;


            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task<AppUserDto?> GetUserDto(Guid id)
        {
            var user = await this.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                var dto = AppUserDto.FromAppUser(user);
                if (dto != null && dto.AreaIds != null && dto.AreaIds.Any())
                {
                    dto.Areas = _fieldAreaRepository.GetQueryable().Where(x => dto.AreaIds.Contains(x.Id)).ToList();
                }
                return dto;
            }

            return null;
        }
        public async Task<DataResponse> RegisterUser(string email, string name, string phone, string gender, string type, string password, string confirmPassword, List<Guid>? areaIds, string baseUri)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return new DataResponse()
                {
                    Message = $"Email {email} is already taken",
                    Success = false,
                };
            }

            if (string.IsNullOrEmpty(password))
                return new DataResponse()
                {
                    Message = "The password is empty",
                    Success = false,
                };

            if (password.Length < 6)
                return new DataResponse()
                {
                    Message = "Passwords must be at least 6 characters",
                    Success = false,
                };


            if (password != confirmPassword)
                return new DataResponse()
                {
                    Message = "Confirm password dosen't match the password",
                    Success = false,
                };

            var user = new AppUser()
            {
                Email = email,
                UserName = email,
                Name = name,
                PhoneNumber = phone,
                Type = type,
                Gender = gender,
            };

            if (areaIds != null && areaIds.Any())
            {
                user.AreaIds = string.Join(",", areaIds);
            }
            if (type == AccountTypeConstant.Staff || type == AccountTypeConstant.Manager)
            {
                user.EmailConfirmed = true;
            }

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if (!user.EmailConfirmed)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    EmailProvider.SendMailCofirmEmail(user.Email, token, baseUri);
                }
                return new DataResponse()
                {
                    Message = "User created successfully",
                    Success = true,
                };
            }

            return new DataResponse()
            {
                Message = "User didn't created",
                Errors = result.Errors.Select(x => x.Description),
                Success = false,
            };

        }

        public async Task<DataResponse> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new DataResponse()
                {
                    Message = "There is no user with this Email address",
                    Success = false,
                };
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded == false)
            {
                return new DataResponse()
                {
                    Message = "Confirm email error",
                    Errors = result.Errors.Select(x => x.Description).ToList(),
                    Success = false,
                };
            }

            return new DataResponse()
            {
                Message = " Email confirmed",
                Success = true,
            };
        }


        public async Task<DataResponse<AppUserDto>> ChangePassword(string id, string oldPassword, string newPassword, string confirmPassword)
        {
            var result = new DataResponse<AppUserDto>();
            result.Success = false;


            if (string.IsNullOrEmpty(newPassword))
            {
                result.Message = "The password is empty";
                return result;
            }


            if (newPassword != confirmPassword)
            {
                result.Message = "Confirm password dosen't match the password";
                return result;
            }


            var user = await this.GetUser(id);
            if (!await _userManager.CheckPasswordAsync(user, oldPassword))
            {
                result.Message = "Invalid password";
                return result;
            }

            var data = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (data.Succeeded)
            {
                result.Success = true;
                result.Data = AppUserDto.FromAppUser(user);
                result.Message = "Change password successfully";
                return result;
            }
            result.Message = "Change password failed";
            result.Errors = data.Errors.Select(x => x.Description);
            return result;
        }

        public async Task<DataResponse<AppUserDto>> LoginUser(string email, string password)
        {
            if (string.IsNullOrEmpty(password))
                return new DataResponse<AppUserDto>()
                {
                    Message = "The password is empty",
                    Success = false,
                };

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new DataResponse<AppUserDto>()
                {
                    Message = "There is no user with this Email address",
                    Success = false,
                };
            if (!user.EmailConfirmed)
            {
                //return new DataResponse<AppUserDto>()
                //{
                //    Message = "Cannot sign in without confirmed email",
                //    Success = false,
                //};
            }

            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
            {
                return new DataResponse<AppUserDto>()
                {
                    Message = "Invalid password",
                    Success = false,
                };
            }

            return GenToken(user);
        }

        public async Task<DataResponse<AppUserDto>> RefreshToken(string refreshToken)
        {
            var userId = await _cache.GetStringAsync(refreshToken);
            var user = await GetUser(userId);
            if (user == null)
                return new DataResponse<AppUserDto>()
                {
                    Message = "There is no user with this token",
                    Success = false,
                };
            if (!user.EmailConfirmed)
            {
                return new DataResponse<AppUserDto>()
                {
                    Message = "Cannot sign in without confirmed email",
                    Success = false,
                };
            }
            return GenToken(user, refreshToken);
        }

        public async Task<DataResponse<AppUserDto>> CheckLogin(string? id)
        {
            var user = await this.GetUser(id);
            if (user == null)
                return new DataResponse<AppUserDto>()
                {
                    Success = false,
                    Message = "Can't find user",
                };
            var userDto = AppUserDto.FromAppUser(user);
            return new DataResponse<AppUserDto>()
            {
                Success = true,
                Data = userDto,
                Message = "Login success"
            };
        }
        public async Task<DataResponse<string>> ResetPassword(string email, string baseUri)
        {
            var result = new DataResponse<string>();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new DataResponse<string>()
                {
                    Success = false,
                    Message = "There is no user with this Email address",
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var data = await _userManager.ResetPasswordAsync(user, token, password);
            if (data.Succeeded)
            {
                EmailProvider.SendMailResetPassword(user.Email, password, baseUri);
                result.Success = true;
                result.Message = "Password changed";
            }
            else
            {
                result.Success = false;
                result.Message = data.Errors.FirstOrDefault()?.Code;
                result.Errors = data.Errors.Select(x => x.Code);
            }
            return result;

        }

        public async Task<DataResponse> LogoutUser()
        {
            try
            {
                await _tokenService.DeactivateCurrentToken();
                return new DataResponse()
                {
                    Message = "Sign out successed",
                    Success = true,
                };

            }
            catch (Exception)
            {
                return new DataResponse()
                {
                    Message = "Sign out failed",
                    Success = false,
                };

            }


        }

        public async Task<DataResponse<AppUserDto>> UpdateUser(AppUser user)
        {
            var result = new DataResponse<AppUserDto>();
            try
            {
                await Update(user);
                result.Success = true;
                result.Message = "Update user successfully";
                result.Data = AppUserDto.FromAppUser(user);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Update user failed";
                result.Errors = new string[] { ex.Message };
            }
            return result;

        }

        public async Task<DataResponse<AppUserDto>> UploadAvatar(string id, IFormFile file)
        {
            var result = new DataResponse<AppUserDto>();
            var user = await this.GetUser(id);
            if (user != null)
            {
                try
                {
                    var fileSplit = file.FileName.Split('.');
                    var extension = "." + fileSplit.LastOrDefault();

                    var fileName = Guid.NewGuid().ToString() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Root");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = Path.Combine(path, fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    user.Picture = fileName;
                    try
                    {
                        await Update(user);
                        result.Success = true;
                        result.Message = "Update user successfully";
                        result.Data = AppUserDto.FromAppUser(user);
                    }
                    catch (Exception ex)
                    {

                        result.Success = false;
                        result.Message = "Update user failed";
                        result.Errors = new string[] { ex.Message };
                    }

                }
                catch (Exception)
                {

                }







            }

            return result;

        }



        private DataResponse<AppUserDto> GenToken(AppUser? user, string? refreshToken = null)
        {
            var claims = new[]
            {
                new Claim("Email", user?.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), nameof(Guid)),
            };
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.AuthSettings.Key));
            //var token = new JwtSecurityToken(
            //    issuer: AppSettings.AuthSettings.Issuer,
            //    audience: AppSettings.AuthSettings.Audience,
            //    claims: claims,
            //    expires: DateTime.Now.AddHours(AppSettings.AuthSettings.SecondsExpires),
            //    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            //);
            //var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(AppSettings.AuthSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(15), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            if (string.IsNullOrEmpty(refreshToken))
            {
                refreshToken = GenRefreshToken(user.Id);
            }
            else
            {
                _cache.Refresh(refreshToken);
            }



            var userDto = AppUserDto.FromAppUser(user);
            userDto.Token = tokenString;
            userDto.RefreshToken = refreshToken;
            return new DataResponse<AppUserDto>()
            {
                Message = "Login success",
                Data = userDto,
                Success = true,
            };
        }

        private string GenRefreshToken(Guid? userId)
        {
            var refreshToken = Generator.Base64FromBytes(64);
            _cache.SetString(refreshToken, userId.ToString(), new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(AppSettings.AuthSettings.DaysRefreshTokenExpires)
            });
            return refreshToken;
        }

        public async Task<DataResponse<PagedList<AppUserDto>>> GetData(AppUserSearch search)
        {
            try
            {
                var query = (from q in GetQueryable().ToList()
                             select new AppUserDto()
                             {
                                 Id = q.Id,
                                 Name = q.Name,
                                 StaffId = q.StaffId,
                                 Picture = q.Picture,
                                 Type = q.Type,
                                 Email = q.Email,
                                 Gender = q.Gender,
                                 Phone = q.PhoneNumber,
                                 AreaIds = q.AreaIds?.Split(",")?.Select(x => Guid.Parse(x)).ToList(),
                             }).AsQueryable();

                if (search.StaffId.HasValue)
                {
                    query = query.Where(x => x.StaffId == search.StaffId);
                }
                if (!string.IsNullOrEmpty(search.Type))
                {
                    query = query.Where(x => x.Type == search.Type);
                }
                if (!string.IsNullOrEmpty(search.Email))
                {
                    query = query.Where(x => !string.IsNullOrEmpty(x.Email) && x.Email.ToLower().Contains(search.Email.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.Name))
                {
                    query = query.Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(search.Name.ToLower()));
                }
                if (!string.IsNullOrEmpty(search.Phone))
                {
                }

                var result = PagedList<AppUserDto>.Create(query, search);
                foreach (var item in result.Items)
                {
                    if (item.AreaIds != null && item.AreaIds.Any())
                    {
                        item.Areas = _fieldAreaRepository.GetQueryable().Where(x => item.AreaIds.Contains(x.Id)).ToList();
                    }
                }
                return new DataResponse<PagedList<AppUserDto>>()
                {
                    Data = result,
                    Message = "Success"
                };

            }
            catch (Exception ex)
            {
                return DataResponse<PagedList<AppUserDto>>.False(ex.Message);
            }
        }
    }
}
