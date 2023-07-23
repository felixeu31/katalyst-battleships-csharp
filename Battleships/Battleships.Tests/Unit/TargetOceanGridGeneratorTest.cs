using Battleships.GameControls;
using Battleships.Generators;
using Battleships.Ships;
using Battleships.Shoots;
using FluentAssertions;
using Microsoft.VisualStudio.CodeCoverage;

namespace Battleships.Tests.Unit;

public class TargetOceanGridGeneratorTest
{
    [Fact]
    public void should_generate_target_ocean_empty()
    {
        // Arrange
        // Act
        var result = new TargetOceanGridGenerator(new List<Shoot>()).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0|   |   |   |   |   |   |   |   |   |   |
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
    public void should_generate_target_ocean_with_miss_shoot()
    {
        // Arrange
        var shoots = new List<Shoot>()
        {
            Shoot.Miss(new Coordinate(0, 0))
        };

        // Act
        var result = new TargetOceanGridGenerator(shoots).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| o |   |   |   |   |   |   |   |   |   |
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
    public void should_generate_target_ocean_with_hit_shoot()
    {
        // Arrange
        var shoots = new List<Shoot>()
        {
            Shoot.Hit(new Coordinate(0, 0))
        };

        // Act
        var result = new TargetOceanGridGenerator(shoots).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| x |   |   |   |   |   |   |   |   |   |
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
    public void should_generate_target_ocean_with_simple_sunk_shoot()
    {
        // Arrange
        var shoots = new List<Shoot>()
        {
            Shoot.Sunk(new Coordinate(0, 0), ShipType.Gunship, new []{new Coordinate(0, 0)})
        };

        // Act
        var result = new TargetOceanGridGenerator(shoots).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| X |   |   |   |   |   |   |   |   |   |
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
    public void should_generate_target_ocean_with_complex_sunk_shoot()
    {
        // Arrange
        var shoots = new List<Shoot>()
        {
            Shoot.Hit(new Coordinate(0, 0)),
            Shoot.Hit(new Coordinate(1, 0)),
            Shoot.Sunk(new Coordinate(2, 0), ShipType.Gunship, new []
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(2, 0),
            })
        };

        // Act
        var result = new TargetOceanGridGenerator(shoots).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| X |   |   |   |   |   |   |   |   |   |
   1| X |   |   |   |   |   |   |   |   |   |
   2| X |   |   |   |   |   |   |   |   |   |
   3|   |   |   |   |   |   |   |   |   |   |
   4|   |   |   |   |   |   |   |   |   |   |
   5|   |   |   |   |   |   |   |   |   |   |
   6|   |   |   |   |   |   |   |   |   |   |
   7|   |   |   |   |   |   |   |   |   |   |
   8|   |   |   |   |   |   |   |   |   |   |
   9|   |   |   |   |   |   |   |   |   |   |");
    }

}