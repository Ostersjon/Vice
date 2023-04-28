using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class Orderitems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Product Product { get; set; } = null!;
    public int Productid { get; set; }
    public Order Order { get; set; } = null!;
    public int Orderid { get; set; }
    public int Quantity_Orderd { get; set; }
    public decimal TotalPrice { get; set; }

}
