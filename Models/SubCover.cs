using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Venna.Models;

namespace Venna.Models;

public class SubCover
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string FirstImg { get; set; } = string.Empty;
    public int FirstLink { get; set; }
    public string SecImg { get; set; } = string.Empty;
    public int SecLink { get; set; }
    public string ThrdImg { get; set; } = string.Empty;
    public int ThrdLink { get; set; }
    public SubCategory SubCategory { get; set; }
    public int SubCategoryid { get; set; }
}
