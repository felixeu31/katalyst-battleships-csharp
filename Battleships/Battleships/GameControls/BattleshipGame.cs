using System;
using Battleships.Printers;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.GameControls;

public class BattleshipGame
{
    private readonly IPrinter _printer;
    private readonly IOceanGridGenerator _oceanGridGenerator;

    public BattleshipGame(IPrinter printer, IOceanGridGenerator oceanGridGenerator)
    {
        _printer = printer;
        _oceanGridGenerator = oceanGridGenerator;
        Players = new Dictionary<PlayerId, Player>();
        _printer.WriteLine("Welcome to Battleship game!");
    }

    public Dictionary<PlayerId, Player> Players { get; set; }


    public void AddPlayer(PlayerId playerId, List<Ship> ships)
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
        PrintPlayerAction(playerId, "end turn");

        if (this.IsFinished)
        {
            _printer.WriteLine($"Game finished! {this.Winner} won!!");
            var battleReport1 = _oceanGridGenerator.GetBattleReport(PlayerId.Player1, Players[PlayerId.Player1].Shoots,
                Players[GetOpponent(PlayerId.Player1)].Ships);
            var battleReport2 = _oceanGridGenerator.GetBattleReport(PlayerId.Player2, Players[PlayerId.Player2].Shoots,
                Players[GetOpponent(PlayerId.Player2)].Ships);
            _printer.WriteLine(battleReport1);
            _printer.WriteLine(battleReport2);
        }
        else
        {
            _printer.WriteLine($"{playerId.ToString()} finished its turn, it is turn for {GetOpponent(playerId).ToString()} to move");
        }
    }

    public bool IsFinished => Players.Any(x => x.Value.Ships.All(s => s.IsSunk));
    public PlayerId? Winner => IsFinished ? GetOpponent(Players.Single(x => x.Value.Ships.All(s => s.IsSunk)).Key) : null;

    private PlayerId GetOpponent(PlayerId playerId)
    {
        if (playerId.Equals(PlayerId.Player1))
            return PlayerId.Player2;

        return PlayerId.Player1;
    }

    public void PrintPlayerGameGrids(PlayerId playerId)
    {
        PrintPlayerAction(playerId, "print");

        PrintPlayerOcean(playerId);
        PrintTargetOcean(playerId);
    }

    private void PrintPlayerOcean(PlayerId playerId)
    {
        _printer.WriteLine(@"- My ocean grid:");
        var playerShipsPositions = Players[playerId].Ships;
        var oceanPrinted = _oceanGridGenerator.GeneratePlayersOceanGrid(playerShipsPositions);
        _printer.WriteLine(oceanPrinted);
    }

    private void PrintTargetOcean(PlayerId playerId)
    {
        _printer.WriteLine(@"- Target ocean grid:");
        var targetGrid = _oceanGridGenerator.GenerateTargetOceanGrid(Players[playerId].Shoots);
        _printer.WriteLine(targetGrid);
    }

    private void PrintPlayerAction(PlayerId playerId, string action)
    {
        _printer.WriteLine($"{playerId.ToString()} invoked: {action}");
    }

    public void Fire(PlayerId playerId, Coordinate coordinate)
    {
        PrintPlayerAction(playerId, "fire");

        var shoot = Players[GetOpponent(playerId)].ShootAt(coordinate);

        Players[playerId].AddShoot(shoot);

        _printer.WriteLine(shoot.Announce);
    }
}