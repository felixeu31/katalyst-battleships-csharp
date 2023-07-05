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
        game.AddPlayer(PlayerId.Player1, new List<List<Coordinates>>()
        {
            new() { new(7, 3) },
            new() { new(6, 4) },
            new() { new(1, 7) },
            new() { new(9, 9) },
            new() { new(2, 4), new(3, 4), new(4, 4) },
            new() { new(5, 7), new(5, 8), new(5, 9) },
            new() { new(8, 4), new(8, 5), new(8, 6), new(8, 7) },
        });
        game.AddPlayer(PlayerId.Player2, new List<List<Coordinates>>() { });
        game.StartGame(PlayerId.Player1);
        game.Print(PlayerId.Player1);
        game.EndTurn(PlayerId.Player1);

        // Assert
        var output = fakeoutput.ToString();

        return Verify(output);
    }
}