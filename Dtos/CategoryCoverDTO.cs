namespace Venna.Dtos;

public class CategoryCoverDTO
{
    public int id { get; set; }
    public IFormFile FirstImg { get; set; }
    public int FirstLink { get; set; }
    public IFormFile SecImg { get; set; }
    public int SecLink { get; set; }
    public IFormFile ThrdImg { get; set; }
    public int ThrdLink { get; set; }
    public int Categoryid { get; set; }
}
