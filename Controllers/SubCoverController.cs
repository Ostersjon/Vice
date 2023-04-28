using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Dtos;
using Venna.Helpers;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCoverController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IHelpers _helpers;
    public SubCoverController(JollyContext context,IHelpers helpers)
    {
        _context = context;
        _helpers = helpers;
    }
    [HttpGet("jjuuo")]
    public async Task<ActionResult<List<SubCover>>> GetsubCover()
    {
        return Ok(await _context.SubCovers.ToListAsync());
    }


    [HttpGet("GetbySubCategory")]
    public async Task<ActionResult<SubCover>> GetbySubCategory(int id)
    {
        var categoryCover = await _context.SubCovers.FirstOrDefaultAsync(x => x.SubCategoryid == id);
        if (categoryCover == null) return NotFound();
        return Ok(categoryCover);
    }

    [HttpPost]
    public async Task<ActionResult<SubCover>> PostSubCover([FromForm] SubCoverDTO req)
    {
        var mapped = ConvertDto(req);
        await _context.SubCovers.AddAsync(mapped);
        await _context.SaveChangesAsync();
        return Ok(mapped);
    }

    [HttpPut("FirstImg")]
    public async Task<IActionResult> PutFirstImg(int id, IFormFile img)
    {
        var SubCover = await _context.SubCovers.FirstOrDefaultAsync(x => x.SubCategoryid == id);
        if (SubCover == null) return NotFound();
        SubCover.FirstImg = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return Ok(SubCover);
    } 

    [HttpPut("SecImg")]
    public async Task<IActionResult> PutSecondImg(int id, IFormFile img)
    {
        var SubCover = await _context.SubCovers.FirstOrDefaultAsync(x => x.SubCategoryid == id);
        if (SubCover == null) return NotFound();
        SubCover.SecImg = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return Ok(SubCover);
    }  

    [HttpPut("ThirdImg")]
    public async Task<IActionResult> PutThirdImg(int id, IFormFile img)
    {
        var SubCover = await _context.SubCovers.FirstOrDefaultAsync(x => x.SubCategoryid == id);
        if (SubCover == null) return NotFound();
        SubCover.ThrdImg = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return Ok(SubCover);
    } 

    [HttpPut("Links")]
    public async Task<IActionResult> PutLinks(int id,int FLink,int SLink,int TLink)
    {
        var SubCover = await _context.SubCovers.FirstOrDefaultAsync(x => x.SubCategoryid == id);
        if (SubCover == null) return NotFound();
        SubCover.FirstLink = FLink;
        SubCover.SecLink = SLink;
        SubCover.ThrdLink = TLink;
        await _context.SaveChangesAsync();
        return Ok(SubCover);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryCover(int id)
    {
        var categoryCover = await _context.CategoryCover.FindAsync(id);
        if (categoryCover is null) return NotFound();
        _context.CategoryCover.Remove(categoryCover);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    private SubCover ConvertDto(SubCoverDTO Dto)
    {
        var sub = new SubCover()
        {
            id = 0,
            SubCategoryid = Dto.SubCategoryid,
            FirstImg = _helpers.ImgToStr(Dto.FirstImg),
            SecImg = _helpers.ImgToStr(Dto.SecImg),
            ThrdImg = _helpers.ImgToStr(Dto.ThrdImg),
            FirstLink = Dto.FirstLink,
            SecLink = Dto.SecLink,
            ThrdLink = Dto.ThrdLink
        };
        return sub;
    }
}
