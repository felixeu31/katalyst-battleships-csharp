using Battleships.GameControls;
using Battleships.Generators;
using Battleships.Ships;
using Battleships.Shoots;
using FluentAssertions;

namespace Battleships.Tests.Unit;

public class OceanGridGeneratorTest
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
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
        var result = oceanGridGenerator.GetPlayersOceanGrid(ships);

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
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
        var result = oceanGridGenerator.GetPlayersOceanGrid(ships);

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
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
        var result = oceanGridGenerator.GetPlayersOceanGrid(ships);

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
    
    [Fact]
    public void should_generate_target_ocean_empty()
    {
        // Arrange

        // Act
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
        var result = oceanGridGenerator.GetTargetOceanGrid(new List<Shoot>());

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

        // Act
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
        var shoots = new List<Shoot>()
        {
            Shoot.Miss(new Coordinate(0, 0))
        };
        var result = oceanGridGenerator.GetTargetOceanGrid(shoots);

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

        // Act
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
        var shoots = new List<Shoot>()
        {
            Shoot.Hit(new Coordinate(0, 0))
        };
        var result = oceanGridGenerator.GetTargetOceanGrid(shoots);

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

        // Act
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
        var shoots = new List<Shoot>()
        {
            Shoot.Sunk(new Coordinate(0, 0), ShipType.Gunship, new []{new Coordinate(0, 0)})
        };
        var result = oceanGridGenerator.GetTargetOceanGrid(shoots);

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

        // Act
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
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
        var result = oceanGridGenerator.GetTargetOceanGrid(shoots);

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

    [Fact]
    public void should_print_battle_report()
    {
        // Arrange
        OceanGridGenerator oceanGridGenerator = new OceanGridGenerator(10, 10);
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
        var result = oceanGridGenerator.GetPlayerBattleReport(PlayerId.Player1, shoots, ships);

        // Assert
        result.Should().Be(@"# Player1 battle report
Total shots: 5
Misses: 1
Hits: 4
Ships Sunk: [
	Destroyer: (0,0)
]
    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
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