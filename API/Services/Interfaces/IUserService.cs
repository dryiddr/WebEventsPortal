using Microsoft.AspNetCore.Http;
using Application.Dtos.User;
using Application.Models;

namespace Services.Interfaces;

public interface IUserService
{
    Task<UserModel> GetCurrentUser();
    Task<UserModel> GetUserByNickName(string nickName);
    Task<UserModel> UpdateUserData(UpdateUserDataDto userDataDto);
    Task<UserModel> UpdateUserPhoto(IFormFile avatar, string nickName);
    Task<UserModel> AddComplain(UpdateUserComplainDto complainDto);
}