namespace Venna.Dtos;

public class MainImageDTO
{
    public int id { get; set; }
    public IFormFile FirstImg { get; set; }
    public int FirstLink { get; set; }
    public IFormFile SecImg { get; set; }
    public int SecLink { get; set; }
    public IFormFile ThrdImg { get; set; }
    public int ThrdLink { get; set; }

    public IFormFile FrthImg { get; set; }
    public int FrthLink { get; set; }

    public IFormFile FifImg { get; set; }
    public int FifLink { get; set; }

}
