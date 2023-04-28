using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class Login
{
    [DataType(DataType.EmailAddress,ErrorMessage ="Enter a Valid Email")]
    [MinLength(6, ErrorMessage = "Email must be more than 6 characters")]
    [MaxLength(25, ErrorMessage = "Email must be less than 25 characters")]
    public string Email { get; set; } = string.Empty;
    [MinLength(6, ErrorMessage = "Password must be more than 6 characters")]
    [MaxLength(25, ErrorMessage = "Password must be less than 25 characters")]
    public string Password { get; set; } = null!;
}