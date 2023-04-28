using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venna.Models;

public class CategoryDTO
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IFormFile photo { get; set; } = null!;

}
