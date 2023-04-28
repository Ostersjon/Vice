using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Venna.Models;

public class BrandDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
