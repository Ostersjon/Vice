using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venna.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public User User { get; set; } = null!;
    public int Userid { get; set; }
    public virtual List<Orderitems> Orderitems { get; set; } = null!;
    public DateTime DateOrderd { get; set; } = DateTime.Now;
    public string SippingAddress { get; set; } = null!;
    public decimal TotalPrice { get; set; } 
}
