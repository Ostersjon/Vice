using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IMapper _mapper;

    public BrandsController(JollyContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
    {
      if (_context.Brand == null) return NotFound();
      var Brandlist = await _context.Brand.ToListAsync();
      var mappedlist = _mapper.Map<List<BrandDTO>>(Brandlist);
      return Ok(mappedlist);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrand(int id)
    {
        var brand = await _context.Brand.FindAsync(id);
        if (_context.Brand == null) return NotFound();
        if (brand == null) return NotFound();
        return brand;
    }

    [HttpGet("Categories{id}")]
    public async Task<ActionResult<Brand>> CategoryBrand(int id)
    {
        var brand =  _context.Product.Where(x => x.categoryid == id).Include(x => x.brand).Select(x => x.brand).Distinct().ToList();
        if (brand == null) return NotFound();
        return Ok(brand);
    }

    [HttpGet("Subs{id}")]
    public async Task<ActionResult<Brand>> SubCategoryBrand(int id)
    {
        var brand = _context.Product.Where(x => x.Subcategoryid == id).Include(x => x.brand).Select(x => x.brand).Distinct().ToList();
        if (brand == null) return NotFound();
        return Ok(brand);
    }

    [HttpGet("CatnSubs{id}")]
    public async Task<ActionResult<Brand>> CategoryGetSubs(int id)
    {
        var catsubs = _context.Product.Where(x => x.brandid == id).Include(x => x.SubCategory).Select(x => x.SubCategory).Distinct().ToList();
        if (catsubs == null) return NotFound();
        return Ok(catsubs);
    }

    [HttpPost]
    public async Task<ActionResult<Brand>> AddBrand(BrandDTO brand)
    {
      if (_context.Brand == null) return BadRequest("Brand is Empty");
      var mapped = _mapper.Map<Brand>(brand);
      await _context.Brand.AddAsync(mapped);
      await _context.SaveChangesAsync();
      return Ok(mapped);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(int id)
    {
        var brand = await _context.Brand.FindAsync(id);
        if (_context.Brand == null) return BadRequest("Brand is Empty");
        if (brand == null) return NotFound();
        _context.Brand.Remove(brand);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}