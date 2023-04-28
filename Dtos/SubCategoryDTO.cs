using Venna.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Dtos;

public class SubCategoryDTO
{
    public int id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IFormFile Photo { get; set; } 
    public int Categoryid { get; set; }
}