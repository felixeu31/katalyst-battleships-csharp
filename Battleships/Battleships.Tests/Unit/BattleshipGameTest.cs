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
        game.Players.Should().ContainKey(PlayerId.Player1);
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
}