using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Dtos.Complain;
using Application.Dtos.User;
using Application.Exceptions;
using Application.Extensions;
using Application.Models;
using Services.Interfaces;

namespace Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{nickName}")]
    public async Task<ActionResult<UserModel>> GetUserByNickName(string nickName)
    {
        var user = await _userService.GetUserByNickName(nickName);
        return Ok(user);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserModel>> GetUser()
    {
        var user = await _userService.GetCurrentUser();
        return Ok(user);
    }
    [HttpPut]
    [ActionName("user_data")]
    [Authorize]
    public async Task<ActionResult> UpdateUserDataAsync(UpdateUserDataDto updateUserDataDto)
    {
        var user = await _userService.UpdateUserData(updateUserDataDto);
        return Ok(user);
    }
    [HttpPost("{nickName}")]
    [ActionName("photo")]
    [Authorize]
    public async Task<ActionResult> UpdateUserPhoto(IFormFile file, string nickName)
    {
        var user = await _userService.UpdateUserPhoto(file, nickName);
        return Ok(user);
    }
    /*[HttpPut]
    public async Task<ActionResult> Complain(UserComplainDto userComplainDto)
    {
        return NoContent();
    }*/
}