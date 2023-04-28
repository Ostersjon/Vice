using Venna.Models;

namespace Venna.Dtos;

public class SubCoverDTO
{
    public int id { get; set; }
    public IFormFile FirstImg { get; set; } 
    public int FirstLink { get; set; }
    public IFormFile SecImg { get; set; }
    public int SecLink { get; set; }
    public IFormFile ThrdImg { get; set; }
    public int ThrdLink { get; set; }
    public int SubCategoryid { get; set; }
}
