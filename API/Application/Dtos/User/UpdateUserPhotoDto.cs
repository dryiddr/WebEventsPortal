using Microsoft.AspNetCore.Http;

namespace Application.Dtos.User;

public class UpdateUserPhotoDto
{
    public string? NickName { get; set; }
    public IFormFile Image { get; set; }
}