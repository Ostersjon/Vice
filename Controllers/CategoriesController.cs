using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Models;
using Venna.Helpers;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IWebHostEnvironment webHost;
    private readonly JollyContext _context;
    private readonly IHelpers _helpers;
    private readonly IMapper _mapper;

    public CategoriesController(JollyContext context,IMapper mapper, IWebHostEnvironment webHost,IHelpers helpers)
    {
        _context = context;
        _mapper = mapper;
        this.webHost = webHost;
        _helpers = helpers;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
    {
        return await _context.Category.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Category.FindAsync(id);
        if (category == null) return NotFound();
        return category;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategory(CategoryDTO category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPut("ChangeImg")]
    public async Task<IActionResult> PutCategoryImg([FromForm] int id ,IFormFile img)
    {
        var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return NotFound();
        category.photo = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Category>> PostCategory([FromForm] CategoryDTO category)
    {
        var MCategory = Mapping(category);
        await _context.Category.AddAsync(MCategory);
        await _context.SaveChangesAsync();
        PostCategoryCover( MCategory.Id );
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Category.FindAsync(id);
        if (category == null) return NotFound();
        _context.Category.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    private void PostCategoryCover(int id)
    {
        var cover = new CategoryCover(){id=0,Categoryid=id};
         _context.CategoryCover.AddAsync(cover);
         _context.SaveChanges();
    }
    private Category Mapping(CategoryDTO req)
    {
        var mapped = _mapper.Map<Category>(req);
        mapped.photo = _helpers.ImgToStr(req.photo);
        return mapped;
    }
}
