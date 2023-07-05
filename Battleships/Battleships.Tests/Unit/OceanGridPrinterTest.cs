﻿using FluentAssertions;

namespace Battleships.Tests.Unit;

public class OceanGridPrinterTest
{
    [Fact]
    public void should_print_gun_ship()
    {
        // Arrange
        var ships = new List<Ship>
        {
            new Ship(new Coordinates()
            {
                XPosition = 0,
                YPosition = 0
            })
        };

        // Act
        OceanGridPrinter oceanGridPrinter = new OceanGridPrinter();
        var result = oceanGridPrinter.Print(ships);

        // Assert
        result.Should().Be(@"
    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| g |   |   |   |   |   |   |   |   |   |
   1|   |   |   |   |   |   |   |   |   |   |
   2|   |   |   |   |   |   |   |   |   |   |
   3|   |   |   |   |   |   |   |   |   |   |
   4|   |   |   |   |   |   |   |   |   |   |
   5|   |   |   |   |   |   |   |   |   |   |
   6|   |   |   |   |   |   |   |   |   |   |
   7|   |   |   |   |   |   |   |   |   |   |
   8|   |   |   |   |   |   |   |   |   |   |
   9|   |   |   |   |   |   |   |   |   |   |");
    }
}