using FluentAssertions;
using Moq;

namespace Battleships.Tests.Unit;

public class BattleshipGameTest
{
    [Fact]
    public void new_game_should_print_greeting()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();

        // act
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // assert
        printerMock.Verify(x => x.WriteLine("Welcome to Battleship game!"));
    }

    [Fact]
    public void should_add_player_with_ships()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // act
        var ships = new List<Ship>();
        game.AddPlayer(PlayerId.Player1, ships);

        // Assert
        game.Players.Should().HaveCount(1);
        game.Players.Should().ContainKey(PlayerId.Player1);
        game.Players[PlayerId.Player1].Should().NotBeNull();
        game.Players[PlayerId.Player1].Ships.Should().BeEquivalentTo(ships);
        printerMock.Verify(x => x.WriteLine("Player1 added to the game"));
    }

    [Fact]
    public void should_add_second_player()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, new List<Ship>());

        // Assert
        game.Players.Should().ContainKey(PlayerId.Player1);
        printerMock.Verify(x => x.WriteLine("Player2 added to the game"));
        game.Players.Should().ContainKey(PlayerId.Player1);
        printerMock.Verify(x => x.WriteLine("Player2 added to the game"));
    }

    [Fact]
    public void should_inform_users_when_game_starts()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, new List<Ship>());
        game.StartGame(PlayerId.Player1);

        // Arrange
        printerMock.Verify(x => x.WriteLine("Player1 invoked: start"));
        printerMock.Verify(x => x.WriteLine("Game started! Player1 starts moving"));
    }

    [Fact]
    public void should_print_player_game()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // act
        var ships = new List<Ship>()
        {
            new Ship(new Coordinate(2, 7)),
            new Ship(new Coordinate(4, 6)),
            new Ship(new Coordinate(7, 1)),
            new Ship(new Coordinate(9, 9)),
            new Ship(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            new Ship(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            new Ship(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, ships);
        game.AddPlayer(PlayerId.Player2, new List<Ship>());
        game.StartGame(PlayerId.Player1);
        game.PrintPlayerGameGrids(PlayerId.Player1);

        // Arrange
        printerMock.Verify(x => x.WriteLine("Player1 invoked: print"));
        printerMock.Verify(x => x.WriteLine(@"- My ocean grid:"));
        oceanPrinterMock.Verify(x => x.GeneratePlayersOceanGrid(ships));

        printerMock.Verify(x => x.WriteLine(@"- Target ocean grid:"));
        oceanPrinterMock.Verify(x => x.GenerateTargetOceanGrid(new List<Shoot>()));
    }

    [Fact]
    public void should_inform_users_when_user_ends()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, new List<Ship>());
        game.StartGame(PlayerId.Player1);
        game.EndTurn(PlayerId.Player1);

        // Arrange
        printerMock.Verify(x => x.WriteLine("Player1 invoked: end turn"));
        printerMock.Verify(x => x.WriteLine("Player1 finished its turn, it is turn for Player2 to move"));
    }

    [Fact]
    public void should_register_player_water_shoot()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // act
        var ships = new List<Ship>()
        {
            new Ship(new Coordinate(2, 7)),
            new Ship(new Coordinate(4, 6)),
            new Ship(new Coordinate(7, 1)),
            new Ship(new Coordinate(9, 9)),
            new Ship(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            new Ship(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            new Ship(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(3, 0));

        // Arrange
        var shoot = game.Players[PlayerId.Player1].Shoots[0];
        shoot.Should().Be(new Shoot(new Coordinate(3, 0), ShootDamage.Water));
        shoot.Announce.Should().Be("Miss");
    }


    [Fact]
    public void should_register_player_hit_shoot()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        Mock<IOceanGridGenerator> oceanPrinterMock = new Mock<IOceanGridGenerator>();
        BattleshipGame game = new BattleshipGame(printerMock.Object, oceanPrinterMock.Object);

        // act
        var ships = new List<Ship>()
        {
            new Ship(new Coordinate(2, 7)),
            new Ship(new Coordinate(4, 6)),
            new Ship(new Coordinate(7, 1)),
            new Ship(new Coordinate(9, 9)),
            new Ship(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            new Ship(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            new Ship(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(3, 2));

        // Arrange
        var shoot = game.Players[PlayerId.Player1].Shoots[0];
        shoot.Should().Be(new Shoot(new Coordinate(3, 2), ShootDamage.Hit));
        shoot.Announce.Should().Be("Hit");
    }
}