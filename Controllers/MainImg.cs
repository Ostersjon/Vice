using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Dtos;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainImg : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IWebHostEnvironment webHost;
    public MainImg(JollyContext context,IWebHostEnvironment web)
    {
       _context = context;
        webHost = web;
    }
    [HttpGet("Get")]
    public async Task<IActionResult> Get()
    {
        var Main = await _context.Mainimg.FirstOrDefaultAsync(x => x.id == 1);
        return Ok(Main);
    }

    [HttpPost("Post")]
    public async Task<IActionResult> PostImg([FromForm] MainImageDTO req)
    {
        var folder = Path.Combine(webHost.WebRootPath, "img","Cover");
        if(!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var Firstname = Guid.NewGuid().ToString()+".png";
        var Secname = Guid.NewGuid().ToString() + ".png";
        var ThrdName = Guid.NewGuid().ToString() + ".png"; 
        var ForthName = Guid.NewGuid().ToString() + ".png"; 
        var FifName = Guid.NewGuid().ToString() + ".png"; 
        var FirstnamePath = Path.Combine(folder, Firstname);
        var FirstSecname = Path.Combine(folder, Secname);
        var ThrdNamePath = Path.Combine(folder, ThrdName);
        var ForthNamePath = Path.Combine(folder, ForthName);
        var FifNamePath = Path.Combine(folder, FifName);
        var DTO = new MainImage()
       {
            id = 0,
            FirstImg = Firstname,
            SecImg = Secname,
            ThrdImg = ThrdName,
            FrthImg = ForthName,
            FifImg = FifName,
            FirstLink = req.FirstLink,
            SecLink= req.SecLink,
            ThrdLink= req.ThrdLink,
            FrthLink= req.FrthLink,
            FifLink= req.FifLink
        };
        await req.FirstImg.CopyToAsync(new FileStream(FirstnamePath, FileMode.Create));
        await req.SecImg.CopyToAsync(new FileStream(FirstSecname, FileMode.Create));
        await req.ThrdImg.CopyToAsync(new FileStream(ThrdNamePath, FileMode.Create));
        await req.FrthImg.CopyToAsync(new FileStream(ForthNamePath, FileMode.Create));
        await req.FifImg.CopyToAsync(new FileStream(FifNamePath, FileMode.Create));
        await _context.Mainimg.AddAsync(DTO);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("PutImg")]
    public async Task<IActionResult> Putimage([FromForm] MainImageDTO req)
    {
        var Main = await _context.Mainimg.FirstOrDefaultAsync(x => x.id == 1);

        var folder = Path.Combine(webHost.WebRootPath, "img", "Cover");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var Firstname = Guid.NewGuid().ToString() + ".png";
        var Secname = Guid.NewGuid().ToString() + ".png";
        var ThrdName = Guid.NewGuid().ToString() + ".png";
        var ForthName = Guid.NewGuid().ToString() + ".png";
        var FifName = Guid.NewGuid().ToString() + ".png";
        var FirstnamePath = Path.Combine(folder, Firstname);
        var FirstSecname = Path.Combine(folder, Secname);
        var ThrdNamePath = Path.Combine(folder, ThrdName);
        var ForthNamePath = Path.Combine(folder, ForthName);
        var FifNamePath = Path.Combine(folder, FifName);
        Main.id = 1;
        Main.FirstImg = Firstname;
        Main.SecImg = Secname;
        Main.ThrdImg = ThrdName;
        Main.FrthImg = ForthName;
        Main.FifImg = FifName;
        Main.FirstLink = req.FirstLink;
        Main.SecLink = req.SecLink;
        Main.ThrdLink = req.ThrdLink;
        Main.FrthLink = req.FrthLink;
        Main.FifLink = req.FifLink;

        await req.FirstImg.CopyToAsync(new FileStream(FirstnamePath, FileMode.Create));
        await req.SecImg.CopyToAsync(new FileStream(FirstSecname, FileMode.Create));
        await req.ThrdImg.CopyToAsync(new FileStream(ThrdNamePath, FileMode.Create));
        await req.FrthImg.CopyToAsync(new FileStream(ForthNamePath, FileMode.Create));
        await req.FifImg.CopyToAsync(new FileStream(FifNamePath, FileMode.Create));
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpPut("Put1")]
    public async Task<IActionResult> PutFirst([FromForm]  int link, IFormFile img)
    {
       var Main = await _context.Mainimg.FirstOrDefaultAsync(x => x.id == 1);
        var folder = Path.Combine(webHost.WebRootPath, "img", "Cover");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var Firstname = Guid.NewGuid().ToString()+".png";
        var fullpath = Path.Combine(folder, Firstname);
        Main.FirstImg = Firstname;
        Main.FirstLink = link;
        await _context.SaveChangesAsync();
        await img.CopyToAsync(new FileStream(fullpath, FileMode.Create));
        return Ok();
    }
    [HttpPut("Put2")]
    public async Task<IActionResult> PutSec([FromForm]  int link, IFormFile img)
    {
        var Main = await _context.Mainimg.FirstOrDefaultAsync(x => x.id == 1);
        var folder = Path.Combine(webHost.WebRootPath, "img", "Cover");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var name = Guid.NewGuid().ToString()+".png";
        var fullpath = Path.Combine(folder, name);
        Main.SecImg = name;
        Main.SecLink = link;
        await _context.SaveChangesAsync();
        await img.CopyToAsync(new FileStream(fullpath, FileMode.Create));
        return Ok();
    }
    [HttpPut("Put3")]
    public async Task<IActionResult> PutThrd([FromForm] int link, IFormFile img)
    {
        var Main = await _context.Mainimg.FirstOrDefaultAsync(x => x.id == 1);
        var folder = Path.Combine(webHost.WebRootPath, "img", "Cover");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var name = Guid.NewGuid().ToString()+".png";
        var fullpath = Path.Combine(folder, name);
        Main.ThrdImg = name;
        Main.ThrdLink = link;
        await _context.SaveChangesAsync();
        await img.CopyToAsync(new FileStream(fullpath, FileMode.Create));
        return Ok();
    }
    [HttpPut("Put4")]
    public async Task<IActionResult> PutForth([FromForm]int link,IFormFile img)
    {
        var Main = await _context.Mainimg.FirstOrDefaultAsync(x => x.id == 1);
        var folder = Path.Combine(webHost.WebRootPath, "img", "Cover");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var name = Guid.NewGuid().ToString()+".png";
        var fullpath = Path.Combine(folder, name);
        Main.FrthImg = name;
        Main.FrthLink = link;
        await _context.SaveChangesAsync();
        await img.CopyToAsync(new FileStream(fullpath, FileMode.Create));
        return Ok();
    }
    [HttpPut("Put5")]
    public async Task<IActionResult> PutFifth([FromForm] int link, IFormFile img)
    {
        var Main = await _context.Mainimg.FirstOrDefaultAsync(x => x.id == 1);
        var folder = Path.Combine(webHost.WebRootPath, "img", "Cover");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
        var name = Guid.NewGuid().ToString()+".png";
        var fullpath = Path.Combine(folder, name);
        Main.FifImg = name;
        Main.FifLink = link;
        await _context.SaveChangesAsync();
        await img.CopyToAsync(new FileStream(fullpath, FileMode.Create));
        return Ok();
    }


}
