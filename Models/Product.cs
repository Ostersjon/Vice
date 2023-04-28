using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venna.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string name { get; set; }= string.Empty;
    public string desciption { get; set; } = string.Empty;
    public string ProductPhoto { get; set; } = string.Empty;
    public string Productinnerphotos { get; set; } = string.Empty;
    public int QuantityinStock { get; set; }
    public decimal Price { get; set; }
    public Brand brand { get; set; }
    public int brandid { get; set;}
    public Category category { get; set; }
    public int categoryid { get; set;}
    public SubCategory SubCategory { get; set; }
    public int Subcategoryid { get; set;}
    public List<Review> Reviews { get; set; }
    public int rate { get; set; } = 0;
    public int rateNO { get; set; } = 0;
    public double AvrRate { get { if (rateNO == 0) return 0; else return (double)rate / (double)rateNO; } }
}