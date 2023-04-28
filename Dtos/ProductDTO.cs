using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venna.Models;

public class ProductDTO
{
    public int id { get; set; }
    public string name { get; set; }= string.Empty;
    public string desciption { get; set; } = string.Empty;
    public IFormFile ProductPhoto { get; set; } = null!;
    public List<IFormFile> Productinnerphotos { get; set; } = null!;
    public int QuantityinStock { get; set; }
    public decimal Price { get; set; }
    public int categoryid { get; set; }
    public int Subcategoryid { get; set; }
    public int brandid { get; set; }
    public int rate { get; set; } 
    public int rateNO { get; set; } 
    //public double? AvrRate => (rateNO/rate);
}
