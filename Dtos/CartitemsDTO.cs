using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class CartitemsDTO
{
    public int Id { get; set; }
    public int Productid { get; set; }
    public int Cartid { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}
