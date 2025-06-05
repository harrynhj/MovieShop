using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class MovieController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = new { Id = id, Name = $"Product {id}", Price = 99.99 };
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct(int id)
    {

        return Ok("Product created");
    }
}