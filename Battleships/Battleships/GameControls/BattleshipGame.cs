using System;
using Battleships.Printers;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.GameControls;

public class BattleshipGame
{
    private readonly IDisplay _display;
    private readonly IOceanGridGenerator _oceanGridGenerator;
    public Dictionary<PlayerId, Player> Players { get; set; }

    public BattleshipGame(IDisplay display, IOceanGridGenerator oceanGridGenerator)
    {
        _display = display;
        _oceanGridGenerator = oceanGridGenerator;
        Players = new Dictionary<PlayerId, Player>();
        _display.WriteLine("Welcome to Battleship game!");
    }
    
    public void AddPlayer(PlayerId playerId, List<Ship> ships)
    {
        Players.Add(playerId, new Player(playerId, ships));
        _display.WriteLine($"{playerId.ToString()} added to the game");
    }

    public void StartGame(PlayerId playerId)
    {
        DisplayPlayerAction(playerId, "start");
        _display.WriteLine($"Game started! {playerId.ToString()} starts moving");
    }
    public void Fire(PlayerId playerId, Coordinate coordinate)
    {
        DisplayPlayerAction(playerId, "fire");

        var shoot = Players[GetOpponent(playerId)].ShootAt(coordinate);

        Players[playerId].AddShoot(shoot);

        _display.WriteLine(shoot.Announce);
    }

    public void EndTurn(PlayerId playerId)
    {
        DisplayPlayerAction(playerId, "end turn");

        if (this.IsFinished)
        {
            DisplayGameFinishAndBattleReports();
        }
        else
        {
            _display.WriteLine($"{playerId.ToString()} finished its turn, it is turn for {GetOpponent(playerId).ToString()} to move");
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

    public void DisplayPlayerGameGrids(PlayerId playerId)
    {
        DisplayPlayerAction(playerId, "print");

        DisplayPlayerOcean(playerId);
        DisplayTargetOcean(playerId);
    }

    private void DisplayPlayerOcean(PlayerId playerId)
    {
        _display.WriteLine(@"- My ocean grid:");
        var playerShipsPositions = Players[playerId].Ships;
        var oceanPrinted = _oceanGridGenerator.GetPlayersOceanGrid(playerShipsPositions);
        _display.WriteLine(oceanPrinted);
    }

    private void DisplayTargetOcean(PlayerId playerId)
    {
        _display.WriteLine(@"- Target ocean grid:");
        var targetGrid = _oceanGridGenerator.GetTargetOceanGrid(Players[playerId].Shoots);
        _display.WriteLine(targetGrid);
    }

    private void DisplayPlayerAction(PlayerId playerId, string action)
    {
        _display.WriteLine($"{playerId.ToString()} invoked: {action}");
    }
    
    private void DisplayGameFinishAndBattleReports()
    {
        _display.WriteLine($"Game finished! {this.Winner} won!!");
        var battleReport1 = _oceanGridGenerator.GetPlayerBattleReport(PlayerId.Player1, Players[PlayerId.Player1].Shoots,
            Players[GetOpponent(PlayerId.Player1)].Ships);
        var battleReport2 = _oceanGridGenerator.GetPlayerBattleReport(PlayerId.Player2, Players[PlayerId.Player2].Shoots,
            Players[GetOpponent(PlayerId.Player2)].Ships);
        _display.WriteLine(battleReport1);
        _display.WriteLine(battleReport2);
    }
}