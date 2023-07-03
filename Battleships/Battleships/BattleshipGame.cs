using System;

namespace Battleships;

public class BattleshipGame
{
    private readonly IPrinter _printer;

    public BattleshipGame(IPrinter printer)
    {
        _printer = printer;
        Players = new Dictionary<PlayerId, Player>();
        _printer.WriteLine("Welcome to Battleship game!");
    }

    public Dictionary<PlayerId, Player> Players { get; set; }


    public void AddPlayer(PlayerId playerId, List<List<Coordinates>> ships)
    {
        Players.Add(playerId, new Player(playerId, ships));
        _printer.WriteLine($"{playerId.ToString()} added to the game");
    }

    public void StartGame(PlayerId playerId)
    {
        PrintPlayerAction(playerId, "start");
        _printer.WriteLine($"Game started! {playerId.ToString()} starts moving");
    }

    public void EndTurn(PlayerId playerId)
    {
        throw new NotImplementedException();
    }

    public void Print(PlayerId playerId)
    {
        PrintPlayerAction(playerId, "print");

        PrintPlayerOcean(playerId);
        PrintTargetOcean(playerId);
    }

    private void PrintPlayerOcean(PlayerId playerId)
    {
        _printer.WriteLine(@"- My ocean grid:
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
   9|   |   |   |   |   | d |   |   |   | g | ");
    }

    private void PrintTargetOcean(PlayerId playerId)
    {
        _printer.WriteLine(@"- Target ocean grid:
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
   9|   |   |   |   |   |   |   |   |   |   | ");
    }

    private void PrintPlayerAction(PlayerId playerId, string action)
    {
        _printer.WriteLine($"{playerId.ToString()} invoked: {action}");
    }
}