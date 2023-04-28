using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartitemsController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IMapper _mapper;

    public CartitemsController(JollyContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cartitems>>> GetCartitems()
    {
        return Ok(await _context.Cartitems.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cartitems>> GetCartitems(int id)
    {
      if (_context.Cartitems == null) return NotFound();      
      var cartitems = await _context.Cartitems.FindAsync(id);
      if (cartitems == null) return NotFound();
      return cartitems;
    } 

    [HttpGet("ByCart")]
    public async Task<IActionResult> GetCartfitems()
    {
        int UserID = Convert.ToInt32(Request.Cookies["User"]);
        var Cartitems = await _context.Cartitems.Where(x => x.Cartid == UserID).ToListAsync();
        return Ok(Cartitems);
    }

    [HttpPost]
    public async Task<ActionResult<Cartitems>> PostCartitems(CartitemsDTO cartitems)
    {
        int UserID = Convert.ToInt32(Request.Cookies["User"]);
        var Cart = await _context.Cart.FirstOrDefaultAsync(x => x.Userid == UserID);
        if (UserID == null || Cart == null || cartitems.Quantity == 0) return BadRequest();
        var CartItem =await _context.Cartitems.FirstOrDefaultAsync(x =>  x.Cartid == Cart.Id && x.Productid == cartitems.Productid);
        var product = await _context.Product.FirstOrDefaultAsync(x => x.id == cartitems.Productid);
        if(CartItem is null)
        {
        var mapped = _mapper.Map<Cartitems>(cartitems);
        await _context.Cartitems.AddAsync(mapped);
        }
        else CartItem.Quantity = cartitems.Quantity;
        await _context.SaveChangesAsync();
        return Ok(cartitems);
    }

    [HttpPut("Quantity")]
    public async Task<IActionResult> PutQuantity(int id,int quntity)
    {
        var Cartitem = await _context.Cartitems.FirstOrDefaultAsync(x => x.Id == id);
        if (Cartitem is null) return BadRequest();
        Cartitem.Quantity= quntity;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartitems(int id)
    {
        var cartitems = await _context.Cartitems.FindAsync(id);
        if (cartitems is null) return NotFound();
        _context.Cartitems.Remove(cartitems);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
