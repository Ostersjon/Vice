using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class Cartitems
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Product Product { get; set; } = null!;
    public int Productid { get; set; }
    public Cart Cart { get; set; } = null!;
    public int Cartid { get; set; }
    public int Quantity { get; set; }
}
