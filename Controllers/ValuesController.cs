using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Venna.Data;
using Venna.Dtos;

namespace Venna.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
	private readonly JollyContext _context;
	private readonly IMapper _mapper;
	public ValuesController(JollyContext context,IMapper mapper)
	{
		_context= context;
		_mapper= mapper;
	}

	[HttpGet]
	public IActionResult GetAll()
	{
		var Categories =_context.Category.Take(9).ToList();
		var ValueObject = new List<object>();

		foreach (var Category in Categories)
		{
			var Value = new List<object>();
            Value.Add(Category.Name);
			var products = _context.Product.Where(x => x.categoryid == Category.Id).Take(18).ToList();
			var mapped = _mapper.Map<List<ProductEndDTO>>(products);
            var SubCategory =( _context.SubCategorys.Where(x => x.Categoryid == Category.Id).Take(7).ToList());
			var SubMapped = _mapper.Map<List<SubEndDTO>>(SubCategory);
            Value.Add(SubMapped);
            Value.Add(mapped);
            ValueObject.Add(Value);
		}
		return Ok(ValueObject);
	}
}
