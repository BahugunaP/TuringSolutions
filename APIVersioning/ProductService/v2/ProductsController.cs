using Microsoft.AspNetCore.Mvc;
namespace ApiVersioning.ProductServices.V2;
[ApiVersion("2.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    // Sample in-memory data store (shared with v1 for simplicity)
    private static readonly List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Product A", Price = 100 },
        new Product { Id = 2, Name = "Product B", Price = 200 }
    };

    // GET: api/v2.0/products
    [HttpGet]
    public IActionResult GetProducts()
    {
        // Example: Version 2 introduces additional metadata
        return Ok(new
        {
            Metadata = "This is version 2",
            Products
        });
    }

    // GET: api/v2.0/products/{id}
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound(new { Message = "Product not found" });

        return Ok(product);
    }

    // POST: api/v2.0/products
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product newProduct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        newProduct.Id = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
        Products.Add(newProduct);

        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }

    // Additional version 2 functionality can go here
    [HttpGet("metadata")]
    public IActionResult GetMetadata()
    {
        return Ok(new { Version = "2.0", Description = "New features introduced in version 2" });
    }
}

// Reuse the same Product model
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}