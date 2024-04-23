using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using N.Model.Entities;
using N.Service.Common;
using N.Service.Common.Service;
using N.Service.Dto;
using N.Service.UserService.Dto;

namespace N.Service.UserService
{
    public interface IUserService : IService<AppUser>
    {
        Task<AppUser?> GetUser(string? id);
        Task<AppUserDto?> GetUserDto(Guid id);
        Task<DataResponse> RegisterUser(string email, string name, string phone, string gender, string type, string password, string confirmPassword,List<Guid>? areaIds, string baseUri);
        Task<DataResponse> ConfirmEmail(string email, string token);
        Task<DataResponse<AppUserDto>> LoginUser(string email, string password);
        Task<DataResponse<string>> ResetPassword(string email, string baseUri);
        Task<DataResponse<AppUserDto>> ChangePassword(string id, string oldPassword, string newPassword, string confirmPassword);
        Task<DataResponse<AppUserDto>> RefreshToken(string refreshToken);
        Task<DataResponse<AppUserDto>> CheckLogin(string? id);
        Task<DataResponse> LogoutUser();
        Task<DataResponse<AppUserDto>> UpdateUser(AppUser user);
        Task<DataResponse<AppUserDto>> UploadAvatar(string id, IFormFile file);
        Task<DataResponse<PagedList<AppUserDto>>>GetData(AppUserSearch search);
    }
}
