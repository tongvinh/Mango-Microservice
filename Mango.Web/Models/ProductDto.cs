using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    [Range(1, 100, ErrorMessage = "Count must be between 1 and 100")]
    public string? ImageLocalPath { get; set; }
    public int Count { get; set; } = 1;
    public IFormFile? Image { get; set; }
}