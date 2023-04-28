using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderitemsController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IMapper _mapper;
    
    public OrderitemsController(JollyContext context, IMapper map)
    {
        _context = context;
        _mapper = map;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Orderitems>>> GetOrderitems()
    {
      if (_context.Orderitems == null) return NotFound();
      return await _context.Orderitems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Orderitems>> GetOrderitems(int id)
    {
        var orderitems = await _context.Orderitems.FindAsync(id);
        if (orderitems == null) return NotFound();
        return orderitems;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrderitems(int id, Orderitems orderitems)
    {
        if (id != orderitems.Id) return BadRequest();
        _context.Entry(orderitems).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderitemsExists(id)) return NotFound(); else throw;
        }

        return NoContent();
    }


    [HttpPost("Checkout")]
    public  ActionResult Checkout(string address)
    {
        var Cartid = Convert.ToInt32(Request.Cookies["User"]);
        var Cartitems =  _context.Cartitems.Where(x=>x.Cartid == Cartid).Include(x => x.Product).ToList();
        var removecartireams = _context.Cartitems.Where(x => x.Cartid == Cartid).ToList();

        if (Cartitems is null) return BadRequest("Your Cart is Empty");
        
        var Order = new Order()
        {
            Id = 0,
            DateOrderd = DateTime.UtcNow,
            Userid = Cartid,
            SippingAddress = address,
            TotalPrice = 0,
            Orderitems = null,
            User = null
        };

        _context.Order.Add(Order);
        _context.SaveChanges();

        int idd = Order.Id;
        foreach (var item in Cartitems)
        {
            var orderitem = new Orderitems()
            {
                Id=0,
                Orderid = idd,
                Productid= item.Productid,
                TotalPrice = item.Product.Price * item.Quantity,
                Quantity_Orderd= item.Quantity,
                Order = null,
                Product=null
            };
           var product =  _context.Product.FirstOrDefault(x => x.id == item.Productid);
            product.QuantityinStock -= item.Quantity;
            var productsincart = _context.Cartitems.Where(x => x.Productid == product.id);
            foreach(var Thisproduct in productsincart)
            {
                if(Thisproduct.Quantity > product.QuantityinStock) Thisproduct.Quantity = product.QuantityinStock;
                if (Thisproduct.Quantity == 0) _context.Cartitems.Remove(Thisproduct);
            }
            _context.Orderitems.Add(orderitem);
            _context.Cartitems.Remove(item);
            _context.SaveChanges();
        }
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Orderitems>> PostOrderitems(OrderitemsDTO orderitems)
    {
        var mapped = _mapper.Map<Orderitems>(orderitems);
        _context.Orderitems.Add(mapped);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetOrderitems", new { id = orderitems.Id }, orderitems);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderitems(int id)
    {
        var orderitems = await _context.Orderitems.FindAsync(id);
        if (orderitems == null) return NotFound();
        _context.Orderitems.Remove(orderitems);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool OrderitemsExists(int id) => (_context.Orderitems?.Any(e => e.Id == id)).GetValueOrDefault();
    
}
