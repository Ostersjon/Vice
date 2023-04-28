using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venna.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    [MinLength(3,ErrorMessage ="Your Name must be more than 3 characters")]
    [MaxLength(15, ErrorMessage = "Your Name must be less than 15 characters")]

    public string FirstName { get; set; } = string.Empty;
    [MinLength(3, ErrorMessage = "Your Name must be more than 3 characters")]
    [MaxLength(15, ErrorMessage = "Your Name must be less than 15 characters")]
    public string LastName { get; set; } = string.Empty;
    [DataType(DataType.EmailAddress,ErrorMessage ="Enter a Valid Email")]
    public string Email { get; set; } = string.Empty;
    public byte[] Password { get; set; } = null!;
    public string Image { get; set; } = "default-user-image.png";
    //public string Token { get; set; } = string.Empty;
    [DataType(DataType.PhoneNumber)]
    [MinLength(10, ErrorMessage = "Phone must be more than 10 characters")]
    [MaxLength(12, ErrorMessage = "Phone Name must be less than 12 characters")]
    public string Phone { get; set; } = string.Empty;
    public string Roles { get; set; } = "User";
    public Cart Cart { get; set; } = null!;
    public List<Order> Orders { get; set; } = null!;
    //public List<RefreshToken> RefreshTokens { get; set; } = null!;
}
