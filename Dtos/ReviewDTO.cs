using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;
public class ReviewDTO
{
    public int Productid { get; set; }
    public byte Rate { get; set; }
    public string RateDesc { get; set; }
}
