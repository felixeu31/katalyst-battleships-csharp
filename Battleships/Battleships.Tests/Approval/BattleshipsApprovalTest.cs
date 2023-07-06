using System.Text;

namespace Battleships.Tests.Approval;

[UsesVerify]
public class BattleshipsApprovalTest
{
    [Fact]
    public Task player_1_prints_board_after_initialization()
    {
        // Arrange
        StringBuilder fakeoutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeoutput));
        Console.SetIn(new StringReader("a\n"));

        // Act
        IPrinter printer = new ConsolePrinter();
        BattleshipGame game = new BattleshipGame(printer);
        game.AddPlayer(PlayerId.Player1, new List<Ship>
        {
            new Ship(new Coordinate(2, 7) ),
            new Ship(new Coordinate(4, 6) ),
            new Ship(new Coordinate(7, 1) ),
            new Ship(new Coordinate(9, 9) ),
            new Ship(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            new Ship(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            new Ship(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        });
        game.AddPlayer(PlayerId.Player2, new List<Ship>() { });
        game.StartGame(PlayerId.Player1);
        game.Print(PlayerId.Player1);
        game.EndTurn(PlayerId.Player1);

        // Assert
        var output = fakeoutput.ToString();

        return Verify(output);
    }
}