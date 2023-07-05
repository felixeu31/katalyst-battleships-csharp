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
        BattleshipGame game = new BattleshipGame(printerMock.Object);

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
        BattleshipGame game = new BattleshipGame(printerMock.Object);

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
        BattleshipGame game = new BattleshipGame(printerMock.Object);

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>()
        {
            new Ship(new Coordinates(7, 3) ),
            new Ship(new Coordinates(6, 4) ),
            new Ship(new Coordinates(1, 7) ),
            new Ship(new Coordinates(9, 9) ),
            new Ship(new Coordinates(2, 4), new Coordinates(3, 4), new Coordinates(4, 4)),
            new Ship(new Coordinates(5, 7), new Coordinates(5, 8), new Coordinates(5, 9)),
            new Ship(new Coordinates(8, 4), new Coordinates(8, 5), new Coordinates(8, 6), new Coordinates(8, 7)),
        });
        game.AddPlayer(PlayerId.Player2, new List<Ship>());
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
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, new List<Ship>());
        game.StartGame(PlayerId.Player1);
        game.EndTurn(PlayerId.Player1);

        // Arrange
        printerMock.Verify(x => x.WriteLine("Player1 invoked: end turn"));
        printerMock.Verify(x => x.WriteLine("Player1 finished its turn, it is turn for Player2 to move"));
    }
}