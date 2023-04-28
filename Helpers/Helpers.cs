namespace Venna.Helpers;

public class Helpers:IHelpers
{
    public string ImgzToStrs(List<IFormFile> imgs)
    {
        if (imgs is null) return "";
        var PhotosList = "";
        foreach (var img in imgs)
        {
            var PhotoGuid = Guid.NewGuid().ToString() + ".png";
            PhotosList += PhotoGuid + " ";
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            var Fullpath = Path.Combine(folderpath, PhotoGuid);
            img.CopyTo(new FileStream(Fullpath, FileMode.Create));
        }
        return PhotosList;
    }
    public string ImgToStr(IFormFile img)
    {
        var name = Guid.NewGuid().ToString() + ".png";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
        var FullPath = Path.Combine(path, name);
        img.CopyTo(new FileStream(FullPath, FileMode.Create));
        return name;
    }
}
