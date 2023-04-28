using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Venna.Data;
using Venna.Helpers;
using Venna.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	// Helpers // 
	private readonly JollyContext _context;
	private readonly IMapper _mapper;
	private readonly Jwt _jwt;


	// Constructor //
	public AuthController(JollyContext context,IMapper mapper,IOptions<Jwt> jwt)
	{
		_context = context;
		_mapper = mapper;
		_jwt = jwt.Value;
	}


    // Get User //
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        if (_context.Users == null) return NotFound();
        
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception err){return NotFound(err.Message);}
    }

	[HttpGet("BeAdmin")]
	public async Task<ActionResult> BeAdmin()
	{
		var userid = Convert.ToInt32(Request.Cookies["User"]);
		var user = _context.Users.Find(userid);
		user.Roles = "Admin";
		await _context.SaveChangesAsync();
		return Ok();
	}


    // Signin //
    [HttpPost("Sign")]
	public async Task<ActionResult<User>> Signin(Regester req)
	{
		if (req is null) return BadRequest("Please enter a valid data");
		var Existed = await _context.Users.FirstOrDefaultAsync(x => x.Email == req.Email);
		if (Existed is not null) return BadRequest("Email is Already Exist");

		try
		{
			var mapped = _mapper.Map<User>(req);
			GenerateToken(mapped);
			await _context.Users.AddAsync(mapped);
			await _context.SaveChangesAsync();
			int id = mapped.id;
			var newCart = new Cart()
			{
				Userid = id,
				TotalPrice = 0
			};
			await _context.Cart.AddAsync(newCart);
			await _context.SaveChangesAsync();
			CookieYes(id);
			CookieNo();
            return Ok();
		}
		catch (Exception err){return BadRequest(err.Message);}
	}


	// Login //

	[HttpPost("Login")]
    public async Task<ActionResult<User>> Login(Login req)
    {
		if (req is null) return BadRequest("Please Enter a valid data");
		
		try
		{
			var ExiestedUser = await _context.Users.SingleOrDefaultAsync
				(x => x.Email == req.Email && x.Password == Encoding.UTF8.GetBytes(req.Password));

			if (ExiestedUser is null) return BadRequest("Email or password is incorrect");
			CookieYes(ExiestedUser.id);
			CookieNo();
			GenerateToken(ExiestedUser);
			return Ok();
		}
		catch(Exception err){return BadRequest(err.Message);}
	}

	// Log out //
	[HttpGet("Logout")]
	public async Task<ActionResult> Logout()
	{
		Response.Cookies.Delete("Active");
		Response.Cookies.Delete("Token");
		Response.Cookies.Delete("User");
		return Ok();
	}

	private string GenerateToken(User user)
	{
		var userclaims = new List<Claim>()
		{
			new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.id)),
			new Claim(ClaimTypes.Email,user.Email),
			new Claim(ClaimTypes.MobilePhone,user.Phone),
			new Claim(ClaimTypes.Role,user.Roles),
			new Claim("FirstName",user.FirstName),
			new Claim("LastName",user.LastName),
		};
		var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
		var Signingcred = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha512Signature);
		var Jwt = new JwtSecurityToken(
			issuer:_jwt.Issuer,
			audience:_jwt.Audiance,
			expires:DateTime.UtcNow.ToLocalTime().AddDays(_jwt.Expires),
			signingCredentials:Signingcred,
			claims:userclaims
			);
		var JwtToken = new JwtSecurityTokenHandler().WriteToken(Jwt);

		var CookieOptions = new CookieOptions()
		{
			Expires = DateTime.UtcNow.ToLocalTime().AddDays(_jwt.Expires),
			HttpOnly = true,
			Secure = true,
			IsEssential = true,
			SameSite = SameSiteMode.None,
		};
		HttpContext.Response.Cookies.Append("Token", JwtToken, CookieOptions);

        return JwtToken;
	}
    // HTTP True Cookie
    private void CookieYes(int id)
    {
        var CookieOptions = new CookieOptions()
        {
            Expires = DateTime.UtcNow.ToLocalTime().AddDays(_jwt.Expires),
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None,
        };
        Response.Cookies.Append("User", Convert.ToString(id), CookieOptions);
    }
    // HTTP False Cookie
    private void CookieNo()
    {
        var CookieOptionstwo = new CookieOptions()
        {
            Expires = DateTime.UtcNow.ToLocalTime().AddDays(_jwt.Expires),
            HttpOnly = false,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None,
        };
        Response.Cookies.Append("Active", "active", CookieOptionstwo);
    }

}
