using Venna.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Dtos;

public class ProductEndDTO
{
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string desciption { get; set; } = string.Empty;
    public string ProductPhoto { get; set; } = string.Empty;
    public int QuantityinStock { get; set; }
    public decimal Price { get; set; }
    public int brandid { get; set; }
    public int categoryid { get; set; }
    public int Subcategoryid { get; set; }
    public int rate { get; set; } 
    public int rateNO { get; set; }
    public double AvrRate { get; set; }

}
