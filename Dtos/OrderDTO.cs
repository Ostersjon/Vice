using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Venna.Models;

public class OrderDTO
{
    public int Id { get; set; }
    public int Userid { get; set; }
    [JsonIgnore]
    public List<OrderitemsDTO> Orderitems { get; set; } = null!;
    public DateTime DateOrderd { get; set; } = DateTime.Now;
    public string SippingAddress { get; set; } = null!;
    public decimal TotalPrice { get; set; } 
}
