namespace Battleships.Infrastructure;

public class ConsoleDisplay : IDisplay
{
    public void WriteLine(string value)
    {
        Console.WriteLine(value);
    }
}