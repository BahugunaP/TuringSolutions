using System.Net.Http;

namespace Client.APICalling
{
    class APICalling
    {
        static void Main()
        {
            ProductClient productClient = new ProductClient(new HttpClient());
            string response = productClient.GetProductsAsync().Result;
        }
    }
}
public class ProductClient
{
    private readonly HttpClient _httpClient;

    public ProductClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public
 async Task<string> GetProductsAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5096//products");
        return response.ToString();
    }
}
