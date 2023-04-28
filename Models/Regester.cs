using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class Regester
{
    [MinLength(3, ErrorMessage = "Your Name must be more than 3 characters")]
    [MaxLength(15, ErrorMessage = "Your Name must be less than 15 characters")]
    public string FirstName { get; set; } = string.Empty;
    [MinLength(3, ErrorMessage = "Your Name must be more than 3 characters")]
    [MaxLength(15, ErrorMessage = "Your Name must be less than 15 characters")]
    public string LastName { get; set; } = string.Empty;
    [DataType(DataType.EmailAddress, ErrorMessage = "Enter a Valid Email")]
    [MinLength(6, ErrorMessage = "Email must be more than 6 characters")]
    [MaxLength(25, ErrorMessage = "Email must be less than 25 characters")]
    public string Email { get; set; } = string.Empty;
    [MinLength(6, ErrorMessage = "Password must be more than 6 characters")]
    [MaxLength(25, ErrorMessage = "Password must be less than 25 characters")]
    public string Password { get; set; } = null!;
    [DataType(DataType.PhoneNumber)]
    [MinLength(10, ErrorMessage = "Phone must be more than 10 characters")]
    [MaxLength(12, ErrorMessage = "Phone Name must be less than 12 characters")]
    public string Phone { get; set; } = string.Empty;

}