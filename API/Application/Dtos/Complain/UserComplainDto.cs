using Domain.Enums;

namespace Application.Dtos.Complain;

public class UserComplainDto
{
    public UserComplainCategories Category { get; set; }
    public string? Text { get; set; }
    public string NickName { get; set; }
}