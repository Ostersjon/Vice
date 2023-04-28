using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Venna.Data;
using Venna.Models;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly JollyContext _context;
    public ReviewsController(JollyContext context)
    {
        _context= context;
    }
    [HttpGet]
    public async Task<IActionResult> Getall()
    {
        var Reviews = await _context.Review.ToListAsync();
        return Ok(Reviews);
    }
    [HttpGet("Allegable")]
    public  dynamic Allegable(int id)
    {
        var userid = Convert.ToInt32(Request.Cookies["User"]);
        var orderd = _context.Order.Where(x => x.Userid == userid).ToList();
        var objectorders = new List<object>();
        foreach (var order in orderd)
        {
        var orderitemss = _context.Orderitems.Where(x => x.Orderid == order.Id &&x.Productid == id);
            objectorders.AddRange(orderitemss);
        }
        return objectorders.Count>0;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Getall(int id)
    {
        var Review = await _context.Review.Where(x => x.Productid == id).Include(x => x.User).ToListAsync();
        if (Review is null) return BadRequest();
        return Ok(Review);
    }
    [HttpPost("Post")]
    public async Task<IActionResult> PostReview(ReviewDTO req)
    {
        var UserID = Convert.ToInt32(Request.Cookies["User"]);
        var product = await _context.Product.FindAsync(req.Productid);
        if (await _context.Users.FindAsync(UserID) is null
        || product is null
        || await _context.Review.FindAsync(UserID, req.Productid) is not null)
        return BadRequest();
        product.rate += req.Rate;
        product.rateNO += 1;
        var review = new Review()
        {
            Productid = req.Productid,
            Userid = UserID,
            Rate = req.Rate,
            RateDesc= req.RateDesc
        };
       await _context.Review.AddAsync(review);
       await _context.SaveChangesAsync();
       return Ok(req);
    }
}