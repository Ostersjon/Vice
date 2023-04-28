using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Dtos;
using Venna.Helpers;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IWebHostEnvironment webHost;
    private readonly IMapper _mapper;
    private readonly IHelpers _helpers;

    public ProductsController(JollyContext context,IWebHostEnvironment web,IMapper mapper,IHelpers helpers)
    {
        _context = context;
        webHost = web;
        _mapper = mapper;
        _helpers = helpers;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
      if (_context.Product == null) return NotFound();
      return Ok(await _context.Product.ToListAsync());
    }

    [HttpGet("Getone")]
    public async Task<ActionResult> Search(string keyword,int? CatID,int? SubID)
    {
        if(CatID is null && SubID is null)
        return Ok(await _context.Product.Where(x => x.name.Contains(keyword)).ToListAsync());
        if(CatID is null && SubID != null)
        return Ok(await _context.Product.Where(x => x.name.Contains(keyword) && x.Subcategoryid == SubID).ToListAsync());
        if(CatID != null && SubID is null)
        return Ok(await _context.Product.Where(x => x.name.Contains(keyword) && x.categoryid == CatID).ToListAsync());
        return Ok(await _context.Product.Where(x => x.name.Contains(keyword) && x.categoryid == CatID 
        && x.Subcategoryid == SubID).ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Product.Where(x => x.id == id)
            .Include(x => x.brand).Include(x => x.category).FirstOrDefaultAsync();
        if (product is null) return NotFound();
        return Ok(product);
    }

    [HttpGet("ProductbyCategory")]
    public async Task<IActionResult> ProductbyCategory([FromQuery] int CatID , int Page)
    {
        var category = await _context.Category.FirstOrDefaultAsync(x => x.Id == CatID);
        if (category is null) return NotFound();
        var products = _context.Product.Where(x => x.categoryid == CatID).Include(x => x.brand);
        var ProductsPerPage = products.Skip((Page - 1) *40).Take(40).ToList();
        
        return Ok(ToMeta(Page, ProductsPerPage, category.Name,products.Count()).Result);
    }  


    [HttpGet("General")]
    public async Task<IActionResult> GeneralProduct(string? Keyword,int? CatID,int? BrandID,int? SubID,string order,int page)
    {
        List<Product> selected =new List<Product>();
        var products = _context.Product.Where
        (x => (Keyword != null? x.name.Contains(Keyword):x.id !=0)
        &&(CatID != null ? x.categoryid == CatID : x.categoryid != 0)
        && (BrandID != null ? x.brandid == BrandID : x.brandid != 0)
        && (SubID != null ? x.Subcategoryid == SubID : x.Subcategoryid != 0)).ToList();

        if (order == "Top") {
            selected = products.OrderBy(x => x.AvrRate).Reverse().ToList();
        }else if (order == "High") {
            selected =  products.OrderBy(x => x.Price).Reverse().ToList();
        }else if (order == "Low") {
            selected = products.OrderBy(x => x.Price).ToList();
        }else if (order == "New") {
            selected = products.OrderBy(x => x.id).Reverse().ToList();
        }else selected = products.OrderBy(x => x.id).ToList().ToList();

        selected = selected.Skip(40 * (page-1)).Take(40).ToList(); 
        return Ok(ToMeta(page, selected, "",products.Count()).Result);
    }   
    
    
    [HttpGet("ProductbyBrand")]
    public async Task<IActionResult> ProductbyBrand([FromQuery] int BrandID , int Page)
    {
        if (!(BrandID is int) || (BrandID == null)) return BadRequest();
        var Brand = await _context.Brand.FirstOrDefaultAsync(x => x.Id == BrandID);
        if (Brand is null) return NotFound();
        var products = _context.Product.Where(x => x.brandid == BrandID);
        var ProductsPerPage = products.Skip((Page - 1) *40).Take(40).ToList();
        return Ok(ToMeta(Page, ProductsPerPage, Brand.Name,products.Count()).Result);
    }

    [HttpGet("HomeProducts")]
    public async Task<IActionResult> ProductsHome()
    {
        var products = await _context.Product.ToListAsync();
        return Ok(ToHomeProducts(products).Result);
    }

    [HttpGet("TrashHomeProducts")]
    public async Task<IActionResult> TrashProductsHome()
    {
        var products = await _context.Product.ToListAsync();
        return Ok(ToHomeProducts(products).Result);
    }

    [HttpGet("CategoriesHomeProduct")]
    public async Task<IActionResult> TrashProductsHome(int id)
    {
        var products = await _context.Product.Where(x => x.categoryid == id).ToListAsync();
        return Ok(ToHomeProducts(products).Result);
    }

    [HttpGet("SubCategoriesProductByCategory")]
    public async Task<IActionResult> SubCategoriesProductByCategory(int id)
    {
        var subcategoreis = await _context.SubCategorys.Where(x => x.Categoryid == id).Select(x=> x.id).Take(6).ToListAsync();
        var CategoriesArray = new List<object>();
        CategoriesArray.Add(subcategoreis);
        foreach (var sub in subcategoreis)
        {
        var products = await _context.Product.Where(x => x.Subcategoryid == sub).Take(6).ToListAsync();
            CategoriesArray.Add(products);
        }
        return Ok(CategoriesArray);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct([FromForm] ProductDTO product)
    {
        if (product == null) return BadRequest("Enter a valid data");
          var mapped = _mapper.Map<Product>(product); 
          mapped.ProductPhoto = _helpers.ImgToStr(product.ProductPhoto);
          mapped.Productinnerphotos = _helpers.ImgzToStrs(product.Productinnerphotos);
          await _context.Product.AddAsync(mapped);
          await  _context.SaveChangesAsync();
          return Ok(mapped);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id , ProductPUT productPUT)
    {        
        var oldproduct =await _context.Product.FirstOrDefaultAsync(x => x.id == id);

        oldproduct.name = productPUT.Pname;
        oldproduct.desciption = productPUT.PDesc;
        oldproduct.QuantityinStock = productPUT.PQunt;
        oldproduct.brandid = productPUT.PBrand;
        oldproduct.categoryid = productPUT.PCat;
        oldproduct.Price = productPUT.PPrice;
        try
        {
            await _context.SaveChangesAsync();
            return Ok(oldproduct);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id)) return NotFound(); throw;
        }
    }

    [HttpPut("Img/{id}")]
    public async Task<IActionResult> PutProduct(int id, IFormFile pic)
    {
        var product = await _context.Product.FirstOrDefaultAsync(x => x.id == id);
        if (id <= 0 || pic is null|| product is null) return BadRequest("Enter a Valid data");
        product.ProductPhoto = _helpers.ImgToStr(pic);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null) return NotFound(); 
        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return (_context.Product?.Any(e => e.id == id)).GetValueOrDefault();
    }


    private async Task<MetaData> ToMeta(int page,List<Product> products,dynamic Property,int TotalProductCount)
    {
        var mapped = _mapper.Map<List<ProductEndDTO>>(products);
        var Meta = new  MetaData()
        {
            MyProperty = Property,
            CurrentPage = page,
            TotalPages =(int)Math.Ceiling((double)TotalProductCount / 40),
            Products =  mapped
        };
        return Meta;
    } 
    private async Task<HomeProduct> ToHomeProducts(List<Product> Products)
    {

        var mapped = _mapper.Map<List<ProductEndDTO>>(Products);
        var homeProduct = new HomeProduct()
        {
            NewProducts = mapped.OrderBy(x => x.id).Reverse().Take(18).ToList(),
            BudgetProducts = mapped.OrderBy(x => x.Price).Take(18).ToList(),
            TopProducts = mapped.OrderBy(x => x.AvrRate).Reverse().Take(18).ToList(),
            HighEndProduct = mapped.OrderBy(x => x.Price).Reverse().Take(18).ToList()
        };
        return homeProduct;
    }
   
    private int TotalPages(int Count)
    {
        var result = Convert.ToDecimal(Count) / Convert.ToDecimal(12);
        int TotalPageNo = (int)Math.Ceiling(result);
        return TotalPageNo;
    }
}
