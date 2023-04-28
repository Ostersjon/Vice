using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Venna.Models;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [JsonIgnore]
    public User User { get; set; } = null!;
    public int Userid { get; set; }
    public List<Cartitems> Cartitems { get; set; } = null!;
    public decimal? TotalPrice { get; set; } 
}
