using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IMapper _mapper;

    public OrdersController(JollyContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<Order>>> GetOrder() =>  await _context.Order.ToListAsync();
    
    [HttpGet("UserOrders")]
    public async Task<ActionResult> UserOrders()
    {
    var id =  Request.Cookies["User"];
    if (id is null) return NotFound();
        
     var Orders = await _context.Order.Where(x => x.Userid == Convert.ToInt64(id)).Include(x => x.Orderitems)
        .ThenInclude(x => x.Product).ThenInclude(x => x.brand).Include(x=>x.Orderitems)
        .ThenInclude(x => x.Product).ThenInclude(x => x.category).ToListAsync();
     return Ok(Orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
      if (_context.Order == null) return NotFound();
      var order = await _context.Order.FindAsync(id);
      if (order == null) return NotFound();
      return order;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, Order order)
    {
        if (id != order.Id) return BadRequest();
        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrderExists(id)) return NotFound();else throw;
        }
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(OrderDTO order)
    {
        var mapped = _mapper.Map<Order>(order);   
        await _context.Order.AddAsync(mapped);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetOrder", new { id = order.Id }, order);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {        
        var order = await _context.Order.FindAsync(id);
        if (order == null) return NotFound();
        _context.Order.Remove(order);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    private bool OrderExists(int id) => (_context.Order?.Any(e => e.Id == id)).GetValueOrDefault();
}