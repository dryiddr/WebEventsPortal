using Domain.Enums;

namespace Domain.Complaint;

public class UserComplaint : BaseComplaint
{
    public User.User User { get; set; }
    public UserComplainCategories Category { get; set; }
}