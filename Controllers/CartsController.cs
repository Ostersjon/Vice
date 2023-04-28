using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly JollyContext _context;
    private readonly IMapper _mapper;

    public CartsController(JollyContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
    {
      if (_context.Cart == null) return NotFound();
      return await _context.Cart.ToListAsync();
    }

    [HttpGet("GetByID")]
    public async Task<ActionResult<Cart>> GetCartbyid()
    {
        var userid = Request.Cookies["User"];
        var cart =  _context.Cart.Where(c => c.Userid == int.Parse(userid)).Include(c => c.Cartitems).ThenInclude(x => x.Product);
        if (cart == null) return NotFound();
        return Ok(cart);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCart(int id, Cart cart)
    {
        if (id != cart.Id) return BadRequest();
        _context.Entry(cart).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CartExists(id)) return NotFound();else throw;
        }
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Cart>> PostCart(Cart cart)
    {
        _context.Cart.Add(cart);
        await _context.SaveChangesAsync();
        return Ok(cart);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCart(int id)
    {
        if (_context.Cart == null) return NotFound();
        var cart = await _context.Cart.FindAsync(id);
        if (cart == null) return NotFound();
        _context.Cart.Remove(cart);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    private bool CartExists(int id) => (_context.Cart?.Any(e => e.Id == id)).GetValueOrDefault();
    
}
