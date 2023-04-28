using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Dtos;
using Venna.Models;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubCategoriesController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IMapper _mapper;


    public SubCategoriesController(JollyContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/SubCategories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategorys()
    {
        return Ok(await _context.SubCategorys.Include(x => x.SubCovers).ToListAsync());
    }
    [HttpGet("gegege")]
    public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubChgeategorys()
    {
        return Ok(await _context.SubCategorys.Include(x => x.SubCovers).ToListAsync());
    }

    // GET: api/SubCategories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SubCategory>> GetSubCategory(int id)
    {
        var subCategory = await _context.SubCategorys.FindAsync(id);

        if (subCategory == null) return NotFound();
        

        return subCategory;
    }
    [HttpGet("GetbyCategory{id}")]
    public async Task<ActionResult<List<SubCategory>>> GetbyCategory(int id)
    {
        var subCategorys = await _context.SubCategorys.Where(x => x.Categoryid == id).ToListAsync();
        return Ok(subCategorys);
    }
    [HttpGet("fullsub{id}")]
    public async Task<ActionResult<List<SubCategory>>> fukk(int id)
    {
        var subCategorys =  _context.SubCategorys.Where(x => x.id == id).Include(x=>x.SubCovers);
        return Ok(subCategorys);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubCategory(int id, SubCategory subCategory)
    {
        _context.Entry(subCategory).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubCategoryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    [HttpPut("ChangeImg")]
    public async Task<IActionResult> PutSubCategoryImg([FromForm] int id, IFormFile img)
    {
        var Subcategory = await _context.SubCategorys.FirstOrDefaultAsync(x => x.id == id);
        if (Subcategory == null) return NotFound();
        Subcategory.Photo = ImgToStr(img);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<SubCategory>> PostSubCategory([FromForm] SubCategoryDTO req)
    {
        if (req is null) return BadRequest();
        var MSub = Mapping(req);
        await _context.SubCategorys.AddAsync(MSub);
        await _context.SaveChangesAsync();
        PostSubCover(MSub.id);
        return Ok();
    }

    // DELETE: api/SubCategories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        var subCategory = await _context.SubCategorys.FindAsync(id);
        if (subCategory == null)
        {
            return NotFound();
        }

        _context.SubCategorys.Remove(subCategory);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    private SubCategory Mapping(SubCategoryDTO req)
    {
        var mapped = _mapper.Map<SubCategory>(req);
        mapped.Photo = ImgToStr(req.Photo);
        return mapped;
    }
    private void PostSubCover(int id)
    {
        var cover = new SubCover()
        {
            id = 0,
            FirstImg = "",
            SecImg = "",
            ThrdImg = "",
            FirstLink = 0,
            SecLink = 0,
            ThrdLink = 0,
            SubCategoryid = id
        };
        _context.SubCovers.Add(cover);
        _context.SaveChanges();
    }
    private bool SubCategoryExists(int id)
    {
        return (_context.SubCategorys?.Any(e => e.id == id)).GetValueOrDefault();
    }


    private string ImgzToStrs(List<IFormFile> imgs)
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
    private string ImgToStr(IFormFile img)
    {
        var name = Guid.NewGuid().ToString() + ".png";
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
        var FullPath = Path.Combine(path, name);
        img.CopyTo(new FileStream(FullPath, FileMode.Create));
        return name;
    }


}
