using Venna.Models;

namespace Venna.Dtos;

public class HomeProduct
{
    public List<ProductEndDTO> NewProducts { get; set; }
    public List<ProductEndDTO> BudgetProducts { get; set; }
    public List<ProductEndDTO> TopProducts { get; set; }
    public List<ProductEndDTO> HighEndProduct { get; set; }
}
