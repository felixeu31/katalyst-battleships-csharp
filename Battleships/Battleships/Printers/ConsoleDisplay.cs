namespace Battleships.Printers;

public class ConsoleDisplay : IDisplay
{
    public void WriteLine(string value)
    {
        Console.WriteLine(value);
    }
}