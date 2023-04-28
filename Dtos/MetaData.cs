using Venna.Models;

namespace Venna.Dtos;

public class MetaData
{
	public dynamic MyProperty { get; set; } 
	public int CurrentPage { get; set; } = 1;
	public int TotalPages { get; set; }
	public List<ProductEndDTO> Products { get; set; }
}
