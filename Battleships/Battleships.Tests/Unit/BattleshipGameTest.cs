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
}