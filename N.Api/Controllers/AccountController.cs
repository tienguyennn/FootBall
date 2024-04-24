using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N.Api.ViewModels.Account;
using N.Service.Common;
using N.Service.Constant;
using N.Service.Dto;
using N.Service.FieldService.Dto;
using N.Service.UserService;
using N.Service.UserService.Dto;
using System.Security.Claims;

namespace N.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : NController
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _UserService;


        public AccountController(
            IUserService AuthService,
            ILogger<AccountController> logger
            )
        {
            _logger = logger;
            _UserService = AuthService;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<DataResponse> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var baseUri = GetUri();
                var result = await _UserService.RegisterUser(model.Email, model.Name, model.Phone, model.Gender, model.Type, model.Password, model.ConfirmPassword,model.AreaIds, baseUri);
                return result;
            }

            return DataResponse.False("Some properties are not valid");
        }



        [HttpGet("/ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _UserService.ConfirmEmail(email, token);
            if (result.Success)
            {
                var html = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Core\\Email\\Templete\\ConfirmSuccess.html"));
                return Content(html, "text/html");
                //return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<DataResponse<AppUserDto>> Login([FromBody] LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _UserService.LoginUser(model.Email, model.Password);
                return result;
            }

            return DataResponse<AppUserDto>.False("Some properties are not valid", ModelStateError);
        }

        //[HttpPost("CreateStaff")]
        //public async Task<DataResponse> CreateStaff([FromBody] RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var baseUri = GetUri();
        //        var result = await _UserService.RegisterUser(model.Email, model.Name, model.Phone, model.Gender, AccountTypeConstant.Staff, model.Password, model.ConfirmPassword, baseUri);
        //        return result;

        //    }

        //    return DataResponse.False("Some properties are not valid");
        //}


        //[HttpPost("CreateManager")]
        //public async Task<DataResponse> CreateManager([FromBody] RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var baseUri = GetUri();
        //        var result = await _UserService.RegisterUser(model.Email, model.Name, model.Phone, model.Gender, AccountTypeConstant.Manager, model.Password, model.ConfirmPassword, baseUri);
        //        return result;

        //    }

        //    return DataResponse.False("Some properties are not valid");
        //}

        //[HttpPost("RegisterFieldOwner")]
        //[AllowAnonymous]
        //public async Task<DataResponse> RegisterFieldOwner([FromBody] RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var baseUri = GetUri();
        //        var result = await _UserService.RegisterUser(model.Email, model.Name, model.Phone, model.Gender, AccountTypeConstant.FieldOwner, model.Password, model.ConfirmPassword, baseUri);
        //        return result;

        //    }

        //    return DataResponse.False("Some properties are not valid");
        //}

        [HttpPost("SetStaff")]
        [AllowAnonymous]
        public async Task<DataResponse> SetStaff([FromBody] SetStaffVM model)
        {
            if (ModelState.IsValid)
            {

                var fieldOwner = _UserService.GetById(model.FieldOwnerId);
                if (fieldOwner == null)
                    return DataResponse.False("Field owner not found");

                if (fieldOwner.Type != AccountTypeConstant.FieldOwner)
                    return DataResponse.False("User is not a field owner");

                var staff = _UserService.GetById(model.StaffId);
                if (staff != null)
                {
                    fieldOwner.StaffId = staff.Id;
                }
                fieldOwner.StaffId = null;
                return new DataResponse()
                {
                    Success = true,
                    Message = "Success"
                };
            }
            return DataResponse.False("Some properties are not valid");
        }

        [HttpPost("GetData")]
        public async Task<DataResponse<PagedList<AppUserDto>>> GetData([FromBody] AppUserSearch search)
        {
            return await _UserService.GetData(search);
        }

        [HttpPost("ChangePassword")]
        public async Task<DataResponse<AppUserDto>> ChangePassword([FromBody] ChangePasswordViewModel model)
        {

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _UserService.ChangePassword(userId, model.OldPassword, model.NewPassword, model.ConfirmPassword);
                return result;

            }
            return DataResponse<AppUserDto>.False("Some properties are not valid");
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<DataResponse<string>> ResetPassword([FromBody] string email)
        {

            var baseUri = this.GetUri();
            var result = await _UserService.ResetPassword(email, baseUri);

            return result;


        }


        [HttpGet("ResetPassword")]
        [AllowAnonymous]
        public async Task<DataResponse<string>> ResetPassword(string email, string token)
        {
            if (ModelState.IsValid)
            {
                var baseUri = this.GetUri();
                var result = await _UserService.ResetPassword(email, baseUri);

                return result;

            }

            return DataResponse<string>.False("Some properties are not valid");
        }


        [HttpGet("CheckLogin")]
        [Authorize]
        public async Task<DataResponse<AppUserDto>> CheckLogin()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _UserService.CheckLogin(userId);
            return result;
        }

        [HttpPost("Logout")]
        public async Task<DataResponse> Logout()
        {
            var result = await _UserService.LogoutUser();
            return result;
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<DataResponse<AppUserDto>> RefreshToken([FromBody] string refreshToken)
        {

            if (ModelState.IsValid)
            {
                var result = await _UserService.RefreshToken(refreshToken);
                return result;
            }

            return DataResponse<AppUserDto>.False("Some properties are not valid");
        }

        [HttpGet("Test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok("ok!");
        }

        [HttpPost("UpdateProfile")]
        [Authorize]
        public async Task<DataResponse<AppUserDto>> UpdateProfile([FromBody] AppUserEditViewModel model)
        {
            var userId = "";
            if (model.Id.HasValue)
            {
                userId = model.Id.ToString();
            }
            else
            {
                userId = UserId.ToString();
            }
            if (ModelState.IsValid)
            {
                var user = await _UserService.GetUser(userId);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Gender = model.Gender;
                    user.PhoneNumber = model.Phone;
                    if (!string.IsNullOrEmpty(model.Type))
                    {
                        user.Type = model.Type;
                    }
                    if (model.AreaIds != null && model.AreaIds.Any())
                    {
                        user.AreaIds = string.Join(",", model.AreaIds);
                    }
                    else
                    {
                        user.AreaIds = "";
                    }

                    var result = await _UserService.UpdateUser(user);
                    return result;
                }
                return DataResponse<AppUserDto>.False("Can't find user");
            }
            return DataResponse<AppUserDto>.False("Some properties are not valid");
        }

        [HttpPost]
        [Route("UploadAvatar")]
        public async Task<DataResponse<AppUserDto>> UploadAvatar(IFormFile file)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _UserService.UploadAvatar(userId, file);
            return result;
        }


        [HttpGet("GetUser/{id}")]
        public async Task<DataResponse<AppUserDto>> GetUser(Guid id)
        {
            var user = await _UserService.GetUserDto(id);
            if (user == null)
                return DataResponse<AppUserDto>.False("Can't find user");
            return new DataResponse<AppUserDto>()
            {
                Success = true,
                Data = user,
            };
        }


        [HttpDelete("Delete/{id}")]
        public async Task<DataResponse> Delete(Guid id)
        {
            try
            {
                var entity = _UserService.GetById(id);
                if (entity == null)
                    return DataResponse.False("Can't find user");
                await _UserService.Delete(entity);
                return new DataResponse()
                {
                    Success = true,
                    Message = "Success",
                };
            }
            catch (Exception ex)
            {

                return DataResponse.False(ex.Message);
            }
        }


    }
}