using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Venna.Models;

namespace Venna.Dtos;

public class SubEndDTO
{
    public int id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}
