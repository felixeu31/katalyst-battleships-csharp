namespace Battleships;

public class ConsolePrinter : IPrinter
{
    public void WriteLine(string value)
    {
        Console.WriteLine(value);
    }
}