using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Venna.Models;

namespace Venna.Models;

public class SubCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public Category category { get; set; } = null!;
    public SubCover SubCovers { get; set; } = null!;
    public int Categoryid { get; set; }
    public List<Product> products { get; set; }

}
