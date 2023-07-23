using Battleships.GameControls;
using Battleships.Generators;
using Battleships.Ships;
using Battleships.Shoots;
using FluentAssertions;
using Microsoft.VisualStudio.CodeCoverage;

namespace Battleships.Tests.Unit;

public class ReportOceanGridGeneratorTest
{
    [Fact]
    public void should_print_battle_report()
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
            }),
            Shoot.Miss(new Coordinate(0, 1)),
            Shoot.Hit(new Coordinate(0, 2)),
        };
        var ship = ShipFactory.Build(new Coordinate(0,0), new Coordinate(1,0), new Coordinate(2,0));
        ship.HitCoordinates = new List<Coordinate>()
            { new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0) };
        List<Ship> ships = new List<Ship>()
        {
            ship,
            ShipFactory.Build(new Coordinate(0,2), new Coordinate(1,2), new Coordinate(2,2)),
        };

        // Act
        var result = new ReportOceanGridGenerator(shoots, ships).GetGrid();

        // Assert
        result.Should().Be(@"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0| X | o | x |   |   |   |   |   |   |   |
   1| X |   | d |   |   |   |   |   |   |   |
   2| X |   | d |   |   |   |   |   |   |   |
   3|   |   |   |   |   |   |   |   |   |   |
   4|   |   |   |   |   |   |   |   |   |   |
   5|   |   |   |   |   |   |   |   |   |   |
   6|   |   |   |   |   |   |   |   |   |   |
   7|   |   |   |   |   |   |   |   |   |   |
   8|   |   |   |   |   |   |   |   |   |   |
   9|   |   |   |   |   |   |   |   |   |   |");

    }
}