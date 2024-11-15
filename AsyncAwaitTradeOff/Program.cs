namespace AsyncAwaitTradeOff;

class Program
{
    static async void Main(string[] args)
    {
        Console.WriteLine("Main thread...");

        // Without ConfigureAwait(false)
        //await LongRunningTask();
        // Console.WriteLine("Main thread continued immediately after LongRunningTask"); // This might not print immediately

        // With ConfigureAwait(false)
        await BachGroundTask().ConfigureAwait(false);
        Console.WriteLine("Main thread continued immediately after LongRunningTask"); // This will print immediately

        Console.WriteLine("Main thread finished execution...");
    }
    private static async Task BachGroundTask()
    {
        await Task.Delay(5000);
        Console.WriteLine("Background task finished.");
    }
}
