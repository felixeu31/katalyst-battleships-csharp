using Battleships.GameControls;
using Battleships.Generators;
using Battleships.Ships;
using Battleships.Shoots;
using FluentAssertions;
using Microsoft.VisualStudio.CodeCoverage;

namespace Battleships.Tests.Unit;

public class PlayerOceanGridGeneratorTest
{
    [Fact]
    public void should_generate_ocean_grid_with_gun_ship()
    {
        // Arrange
        var ships = new List<Ship>
        {
            ShipFactory.Build(new Coordinate(0, 0))
        };

        // Act
        var result = new PlayerOceanGridGenerator(ships).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
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

    [Fact]
    public void should_generate_ocean_grid_with_destroyer()
    {
        // Arrange
        var ships = new List<Ship>
        {
            ShipFactory.Build(new Coordinate(0, 0), new Coordinate(0,1), new Coordinate(0,2))
        };

        // Act
        var result = new PlayerOceanGridGenerator(ships).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| d | d | d |   |   |   |   |   |   |   |
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


    [Fact]
    public void should_generate_ocean_grid_with_carrier()
    {
        // Arrange
        var ships = new List<Ship>
        {
            ShipFactory.Build(new Coordinate(0, 0), new Coordinate(0,1), new Coordinate(0,2), new Coordinate(0,3))
        };

        // Act
        var result = new PlayerOceanGridGenerator(ships).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| c | c | c | c |   |   |   |   |   |   |
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