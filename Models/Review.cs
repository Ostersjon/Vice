using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;
[PrimaryKey(nameof(Userid),nameof(Productid))]
public class Review
{
    public User User { get; set; } = null!;
    public int Userid { get; set; }
    public Product Product { get; set; } = null!;
    public int Productid { get; set; }
    public byte Rate { get; set; }
    public string RateDesc { get; set; }
}