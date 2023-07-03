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

        // act
        BattleshipGame game = new BattleshipGame(printerMock.Object);

        // assert
        printerMock.Verify(x => x.WriteLine("Welcome to Battleship game!"));
    }

    [Fact]
    public void should_add_player_with_ships()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        BattleshipGame game = new BattleshipGame(printerMock.Object);

        // act
        var ships = new List<List<Coordinates>>();
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
        BattleshipGame game = new BattleshipGame(printerMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<List<Coordinates>>());
        game.AddPlayer(PlayerId.Player2, new List<List<Coordinates>>());

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
        BattleshipGame game = new BattleshipGame(printerMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<List<Coordinates>>());
        game.AddPlayer(PlayerId.Player2, new List<List<Coordinates>>());
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
        BattleshipGame game = new BattleshipGame(printerMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<List<Coordinates>>()
        {
            new() { new() { XPosition = 7, YPosition = 3 } },
            new() { new() { XPosition = 6, YPosition = 4 } },
            new() { new() { XPosition = 1, YPosition = 7 } },
            new() { new() { XPosition = 9, YPosition = 9 } },
            new() { new() { XPosition = 2, YPosition = 4 }, new() { XPosition = 3, YPosition = 4 }, new() { XPosition = 4, YPosition = 4 } },
            new() { new() { XPosition = 5, YPosition = 7 }, new() { XPosition = 5, YPosition = 8 }, new() { XPosition = 5, YPosition = 9 } },
            new() { new() { XPosition = 8, YPosition = 4 }, new() { XPosition = 8, YPosition = 5 }, new() { XPosition = 8, YPosition = 6 }, new() { XPosition = 8, YPosition = 7 } },
        });
        game.AddPlayer(PlayerId.Player2, new List<List<Coordinates>>());
        game.StartGame(PlayerId.Player1);
        game.Print(PlayerId.Player1);

        // Arrange
        printerMock.Verify(x => x.WriteLine("Player1 invoked: print"));
        printerMock.Verify(x => x.WriteLine(@"- My ocean grid:
    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0|   |   |   |   |   |   |   |   |   |   |
   1|   |   |   |   |   |   |   |   |   |   |
   2|   |   |   |   |   |   |   | g |   |   |
   3|   |   | d | d | d |   |   |   |   |   |
   4|   |   |   |   |   |   | g |   | c |   |
   5|   |   |   |   |   |   |   |   | c |   |
   6|   |   |   |   |   |   |   |   | c |   |
   7|   | g |   |   |   | d |   |   | c |   |
   8|   |   |   |   |   | d |   |   |   |   |
   9|   |   |   |   |   | d |   |   |   | g | "));
        printerMock.Verify(x => x.WriteLine(@"- Target ocean grid:
    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0|   |   |   |   |   |   |   |   |   |   |
   1|   |   |   |   |   |   |   |   |   |   |
   2|   |   |   |   |   |   |   |   |   |   |
   3|   |   |   |   |   |   |   |   |   |   |
   4|   |   |   |   |   |   |   |   |   |   |
   5|   |   |   |   |   |   |   |   |   |   |
   6|   |   |   |   |   |   |   |   |   |   |
   7|   |   |   |   |   |   |   |   |   |   |
   8|   |   |   |   |   |   |   |   |   |   |
   9|   |   |   |   |   |   |   |   |   |   | "));
    }


    [Fact]
    public void should_inform_users_when_user_ends()
    {
        // arrange
        Mock<IPrinter> printerMock = new Mock<IPrinter>();
        BattleshipGame game = new BattleshipGame(printerMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<List<Coordinates>>());
        game.AddPlayer(PlayerId.Player2, new List<List<Coordinates>>());
        game.StartGame(PlayerId.Player1);

        // Arrange
        printerMock.Verify(x => x.WriteLine("Player1 invoked: end turn"));
        printerMock.Verify(x => x.WriteLine("Player1 finished its turn, it is turn for Player2 to move"));
    }
}