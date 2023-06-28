using System.Text;
using FluentAssertions;

namespace Battleships.Tests.Unit;

public class ConsolePrinterTest
{
    [Fact]
    public void should_write_line()
    {
        // Arrange
        StringBuilder fakeoutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeoutput));
        Console.SetIn(new StringReader("a\n"));

        var printer = new ConsolePrinter();

        // Act
        printer.WriteLine("Written value");

        fakeoutput.ToString().TrimEnd().Should().Be("Written value");

    }
}