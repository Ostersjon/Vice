using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Venna.Data;
using Venna.Helpers;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly JollyContext _context;
    private readonly IHelpers _helpers;
    private readonly IMapper _mapper;

    public UsersController(JollyContext context,IMapper mapper, IWebHostEnvironment env)
    {
        _context = context;
        _mapper = mapper;
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      if (_context.Users == null) return NotFound();
      return await _context.Users.ToListAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<User>> GetUser()
    {
      var UserID = Request.Cookies["User"];
      var user = await _context.Users.FirstOrDefaultAsync(x => x.id == Convert.ToInt64(UserID));
      if (UserID is null||user is null) return BadRequest("something went wrong");
      var mapped = _mapper.Map<UserDTO>(user);
      return Ok(mapped);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        if (id != user.id) return BadRequest();
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id)) return NotFound();
            else throw;
        }
        return NoContent();
    }


    [HttpPut("img")]
    public async Task<IActionResult> PutImg(IFormFile img)
    {
        int id = int.Parse(Request.Cookies["User"]);
        var user = await _context.Users.FindAsync(id);
        if (user is null) return BadRequest();
        user.Image = _helpers.ImgToStr(img);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
      if (_context.Users == null) return BadRequest("Users is null");
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      return CreatedAtAction("GetUser", new { id = user.id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool UserExists(int id)
    {
        return (_context.Users?.Any(e => e.id == id)).GetValueOrDefault();
    }
}
