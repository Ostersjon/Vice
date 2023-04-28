using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venna.Models;

public class UserDTO
{
    public string FirstName { get; set; } = string.Empty;
    [MinLength(3)]
    [MaxLength(15)]
    public string LastName { get; set; } = string.Empty;
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Roles { get; set; } = string.Empty;
}