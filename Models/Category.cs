using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Venna.Models;

namespace Venna.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public string photo { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = null!;
    public CategoryCover CategoryCovers { get; set; } = null!;
}
