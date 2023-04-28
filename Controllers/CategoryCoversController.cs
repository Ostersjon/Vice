using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Dtos;
using Venna.Helpers;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryCoversController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IHelpers _helpers;

    public CategoryCoversController(JollyContext context,IHelpers helpers)
    {
        _context = context;
        _helpers = helpers;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryCover>>> GetCategoryCover()
    {
        return await _context.CategoryCover.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryCover>> GetCategoryCover(int id)
    {
        var categoryCover = await _context.CategoryCover.FindAsync(id);
        if (categoryCover == null) return NotFound();
        return categoryCover;
    }
    [HttpGet("GetbyCategory")]
    public async Task<ActionResult<CategoryCover>> GetbyCategory(int id)
    {
        var categoryCover = await _context.CategoryCover.FirstOrDefaultAsync(x=>x.Categoryid == id);
        if (categoryCover == null) return NotFound();
        return Ok(categoryCover);
    }

    [HttpPut("FirstImg")]
    public async Task<IActionResult> PutFirstImg(int id, IFormFile img)
    {
        var categoryCover = await _context.CategoryCover.FirstOrDefaultAsync(x => x.Categoryid == id);
        if (categoryCover == null) return NotFound();
        categoryCover.FirstImg = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return Ok(categoryCover);
    }
    [HttpPut("SecImg")]
    public async Task<ActionResult<CategoryCoverDTO>> PutSecondImg(int id, IFormFile img)
    {
        var categoryCover = await _context.CategoryCover.FirstOrDefaultAsync(x => x.Categoryid == id);
        if (categoryCover == null) return NotFound();
        categoryCover.SecImg = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return Ok(categoryCover);
    } 
    [HttpPut("ThrdImg")]
    public async Task<ActionResult<CategoryCoverDTO>> PutThirdImg(int id, IFormFile img)
    {
        var categoryCover = await _context.CategoryCover.FirstOrDefaultAsync(x => x.Categoryid == id);
        if (categoryCover == null) return NotFound();
        categoryCover.ThrdImg = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return Ok(categoryCover);
    }
    [HttpPut("FirstLink")]
    public async Task<ActionResult<CategoryCoverDTO>> PutFirstLink(int id, int Link)
    {
        var categoryCover = await _context.CategoryCover.FirstOrDefaultAsync(x => x.Categoryid == id);
        if (categoryCover == null) return NotFound();
        categoryCover.FirstLink = Link;
        await _context.SaveChangesAsync();
        return Ok(categoryCover);
    }
    [HttpPut("SecondLink")]
    public async Task<ActionResult<CategoryCoverDTO>> PutSecondLink(int id, int Link)
    {
        var categoryCover = await _context.CategoryCover.FirstOrDefaultAsync(x => x.Categoryid == id);
        if (categoryCover == null) return NotFound();
        categoryCover.SecLink = Link;
        await _context.SaveChangesAsync();
        return Ok(categoryCover);
    }
    [HttpPut("ThirdLink")]
    public async Task<ActionResult<CategoryCoverDTO>> PutThirdLink(int id, int Link)
    {
        var categoryCover = await _context.CategoryCover.FirstOrDefaultAsync(x => x.Categoryid == id);
        if (categoryCover == null) return NotFound();
        categoryCover.ThrdLink = Link;
        await _context.SaveChangesAsync();
        return Ok(categoryCover);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryCover>> PostCategoryCover([FromForm]CategoryCoverDTO categoryCover)
    {
        var Category = ConvertDto(categoryCover);
        await _context.CategoryCover.AddAsync(Category);
        await _context.SaveChangesAsync();
        return Ok(Category);
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
    private CategoryCover ConvertDto(CategoryCoverDTO Dto)
    {
        var Category = new CategoryCover()
        {
            id = 0,
            Categoryid= Dto.Categoryid,
            FirstImg = _helpers.ImgToStr(Dto.FirstImg),
            SecImg = _helpers.ImgToStr(Dto.SecImg),
            ThrdImg = _helpers.ImgToStr(Dto.ThrdImg),
            FirstLink = Dto.FirstLink,
            SecLink= Dto.SecLink,
            ThrdLink = Dto.ThrdLink
        };
        return Category;
    }
}
