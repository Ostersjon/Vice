using Venna.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venna.Models;

public class CategoryCover
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string FirstImg { get; set; } = string.Empty;
    public int FirstLink { get; set; } = 0;
    public string SecImg { get; set; } = string.Empty;
    public int SecLink { get; set; } = 0;
    public string ThrdImg { get; set; } = string.Empty;
    public int ThrdLink { get; set; } = 0;
    public Category Category { get; set; }
    public int Categoryid { get; set; } = 1;

}
