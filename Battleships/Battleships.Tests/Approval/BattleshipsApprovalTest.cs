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
        BattleshipGame game = new BattleshipGame();
        game.AddPlayer(new List<List<Coordinates>>()
        {
            new() { new() { XPosition = 7, YPosition = 3 } },
            new() { new() { XPosition = 6, YPosition = 4 } },
            new() { new() { XPosition = 1, YPosition = 7 } },
            new() { new() { XPosition = 9, YPosition = 9 } },
            new() { new() { XPosition = 2, YPosition = 4 }, new() { XPosition = 3, YPosition = 4 }, new() { XPosition = 4, YPosition = 4 } },
            new() { new() { XPosition = 5, YPosition = 7 }, new() { XPosition = 5, YPosition = 8 }, new() { XPosition = 5, YPosition = 9 } },
            new() { new() { XPosition = 8, YPosition = 4 }, new() { XPosition = 8, YPosition = 5 }, new() { XPosition = 8, YPosition = 6 }, new() { XPosition = 8, YPosition = 7 } },
        });
        game.AddPlayer(new List<List<Coordinates>>() { });
        game.StartGame(Player.Player1);
        game.Print(Player.Player1);
        game.EndTurn(Player.Player1);

        // Assert
        var output = fakeoutput.ToString();

        return Verify(output);
    }
}