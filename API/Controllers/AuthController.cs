using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Application.Dtos.Auth;
using Application.Dtos.User;
using Services.Interfaces.Identity;

namespace Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    public AuthController(IAuthService authService, IMapper mapper) => (_authService, _mapper) = (authService, mapper);
    
    [HttpPost]
    [ActionName("login")]
    public async Task<IActionResult> Login(AuthDto authDto)
    {
        var (token, user) = await _authService.LoginUserAsync(authDto);
        var response = new
        {
            access_token = token,
            user = user
        };
        return Ok(response);
    }
    [HttpPost]
    [ActionName("register")]
    public async Task<ActionResult> Register([FromForm]RegisterUserDto registerUserDto)
    {
        var (token, user) = await _authService.RegisterUserAsync(registerUserDto);
        var response = new
        {
            access_token = token,
            user = user
        };
        return Ok(response);
    }
}