namespace AsyncAwait;

class Program
{
    static async Task Main(string[] args)
    {
        Task<string> dataTask = GetDataFromApiAsync();

        // Continue with other work while waiting for the data
        Console.WriteLine("Doing other work...");

        // Wait for the data to become available
        string data = await dataTask;

        // Process the data
        Console.WriteLine($"Received data: {data}");
    }
    static async Task<string> GetDataFromApiAsync()
    {
        using (var client = new HttpClient())
        {
            // Make the API request
            HttpResponseMessage response = await client.GetAsync("http://localhost:5096//products");

            //Added Delayes In Ececution  
            await Task.Delay(1000);
            // Check the response status code
            response.EnsureSuccessStatusCode();

            // Read the response content
            return await response.Content.ReadAsStringAsync();
        }
    }
}
