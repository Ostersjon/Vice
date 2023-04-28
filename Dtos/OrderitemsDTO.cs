using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class OrderitemsDTO
{
    public int Id { get; set; }
    public int Productid { get; set; }
    public int Orderid { get; set; }
    public int Quantity_Orderd { get; set; }
    public decimal TotalPrice { get; set; } 
}