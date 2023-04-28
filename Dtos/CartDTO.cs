using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class CartDTO
{
    public int Id { get; set; }
    public int Userid { get; set; }
    public List<Cartitems> cartitemsDTOs { get; set; } = null!;
    public decimal? TotalPrice { get; set; }
}
