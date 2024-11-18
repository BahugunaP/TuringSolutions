using Microsoft.AspNetCore.Mvc;

namespace ApiVersioning.ProductServices.V1;
[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    // Sample in-memory data store
    private static readonly List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Product A", Price = 100 },
        new Product { Id = 2, Name = "Product B", Price = 200 }
    };

    // GET: api/v1.0/products
    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(Products);
    }

    // GET: api/v1.0/products/{id}
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound(new { Message = "Product not found" });

        return Ok(product);
    }

    // POST: api/v1.0/products
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product newProduct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        newProduct.Id = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
        Products.Add(newProduct);

        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }

    // PUT: api/v1.0/products/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound(new { Message = "Product not found" });

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;

        return NoContent();
    }

    // DELETE: api/v1.0/products/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound(new { Message = "Product not found" });

        Products.Remove(product);
        return NoContent();
    }
}

// Sample Product model
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}