using Application.Dtos.Auth;
using Application.Dtos.User;
using Application.Models;

namespace Services.Interfaces.Identity;

public interface IAuthService
{
    public Task<(string, UserModel)> RegisterUserAsync(RegisterUserDto registerUserDto);
    public Task<(string, UserModel)> LoginUserAsync(AuthDto authDto);
}