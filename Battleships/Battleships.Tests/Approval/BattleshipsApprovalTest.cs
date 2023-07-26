using System.Text;
using Battleships.GameControls;
using Battleships.Generators;
using Battleships.Infrastructure;
using Battleships.Ships;

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
        BattleshipGame game = NewGame();
        game.AddPlayer(PlayerId.Player1, new List<Ship>
        {
            ShipFactory.Build(new Coordinate(2, 7) ),
            ShipFactory.Build(new Coordinate(4, 6) ),
            ShipFactory.Build(new Coordinate(7, 1) ),
            ShipFactory.Build(new Coordinate(9, 9) ),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        });
        game.AddPlayer(PlayerId.Player2, new List<Ship>() {
            ShipFactory.Build(new Coordinate(2, 7) ),
            ShipFactory.Build(new Coordinate(4, 6) ),});
        game.StartGame(PlayerId.Player1);
        game.Print(PlayerId.Player1);
        game.EndTurn(PlayerId.Player1);

        // Assert
        var output = fakeoutput.ToString();

        return Verify(output);
    }

    private static BattleshipGame NewGame()
    {
        IDisplay display = new ConsoleDisplay();
        IOceanGridGeneratorFactory factory = new OceanGridGeneratorFactory();
        IBattleshipGameDisplay gameDisplay = new BattleshipGameDisplay(display, factory);
        BattleshipGame game = new BattleshipGame(gameDisplay);
        return game;
    }


    [Fact]
    public Task player_1_destroy_some_ships_and_prints_boards()
    {
        // Arrange
        StringBuilder fakeoutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeoutput));
        Console.SetIn(new StringReader("a\n"));

        // Act
        BattleshipGame game = NewGame();
        game.AddPlayer(PlayerId.Player1, new List<Ship>
        {
            ShipFactory.Build(new Coordinate(2, 7) ),
            ShipFactory.Build(new Coordinate(4, 6) ),
            ShipFactory.Build(new Coordinate(7, 1) ),
            ShipFactory.Build(new Coordinate(9, 9) ),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        });
        game.AddPlayer(PlayerId.Player2, new List<Ship>
        {
            ShipFactory.Build(new Coordinate(2, 7) ),
            ShipFactory.Build(new Coordinate(4, 6) ),
            ShipFactory.Build(new Coordinate(7, 1) ),
            ShipFactory.Build(new Coordinate(9, 9) ),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        });
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(3, 0));
        game.Fire(PlayerId.Player1, new Coordinate(3, 2));
        game.Fire(PlayerId.Player1, new Coordinate(3, 3));
        game.Fire(PlayerId.Player1, new Coordinate(3, 4));
        game.Fire(PlayerId.Player1, new Coordinate(9, 9));
        game.Fire(PlayerId.Player1, new Coordinate(4, 8));
        game.Print(PlayerId.Player1);
        game.EndTurn(PlayerId.Player1);

        // Assert
        var output = fakeoutput.ToString();

        return Verify(output);
    }

    [Fact]
    public Task game_finished_when_player_sunk_all_opponent_ships()
    {
        // Arrange
        StringBuilder fakeoutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeoutput));
        Console.SetIn(new StringReader("a\n"));

        // Act
        BattleshipGame game = NewGame();
        game.AddPlayer(PlayerId.Player1, new List<Ship>
        {
            ShipFactory.Build(new Coordinate(2, 7) ),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
        });
        game.AddPlayer(PlayerId.Player2, new List<Ship>
        {
            ShipFactory.Build(new Coordinate(2, 7) ),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
        });
        game.StartGame(PlayerId.Player1);

        game.Fire(PlayerId.Player1, new Coordinate(2, 6)); //Miss
        game.EndTurn(PlayerId.Player1);
        game.Fire(PlayerId.Player2, new Coordinate(2, 6)); //Miss
        game.EndTurn(PlayerId.Player2);
        game.Fire(PlayerId.Player1, new Coordinate(3, 0)); //Miss
        game.EndTurn(PlayerId.Player1);
        game.Fire(PlayerId.Player2, new Coordinate(3, 0)); //Miss
        game.EndTurn(PlayerId.Player2);

        game.Fire(PlayerId.Player1, new Coordinate(2, 7)); //Hit
        game.EndTurn(PlayerId.Player1);
        game.Fire(PlayerId.Player2, new Coordinate(2, 8)); //Hit
        game.EndTurn(PlayerId.Player2);

        game.Fire(PlayerId.Player1, new Coordinate(3, 2)); //Hit
        game.EndTurn(PlayerId.Player1);
        game.Fire(PlayerId.Player2, new Coordinate(3, 2)); //Hit
        game.EndTurn(PlayerId.Player2);
        game.Fire(PlayerId.Player1, new Coordinate(3, 3)); //Hit
        game.EndTurn(PlayerId.Player1);
        game.Fire(PlayerId.Player2, new Coordinate(3, 3)); //Hit
        game.EndTurn(PlayerId.Player2);
        game.Fire(PlayerId.Player1, new Coordinate(3, 4)); //Hit
        game.EndTurn(PlayerId.Player1);

        // Assert
        var output = fakeoutput.ToString();

        return Verify(output);
    }
}