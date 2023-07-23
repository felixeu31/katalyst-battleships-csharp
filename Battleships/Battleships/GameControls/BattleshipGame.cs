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
        DisplayAddedPlayer(playerId);
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

        DisplayShootResult(shoot);
    }

    public void EndTurn(PlayerId playerId)
    {
        DisplayPlayerAction(playerId, "end turn");

        if (this.IsFinished)
        {
            DisplayGameWinner(this.Winner);
            DisplayPlayerBattleReport(PlayerId.Player1, Players[PlayerId.Player1].Shoots, Players[GetOpponent(PlayerId.Player1)].Ships);
            DisplayPlayerBattleReport(PlayerId.Player2, Players[PlayerId.Player2].Shoots, Players[GetOpponent(PlayerId.Player2)].Ships);
        }
        else
        {
            DisplayPlayerEndedTurn(playerId, GetOpponent(playerId));
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

    private void DisplayAddedPlayer(PlayerId playerId)
    {
        _display.WriteLine($"{playerId.ToString()} added to the game");
    }
    private void DisplayShootResult(Shoot shoot)
    {
        _display.WriteLine(shoot.Announce);
    }

    private void DisplayPlayerEndedTurn(PlayerId playerId, PlayerId opponent)
    {
        _display.WriteLine(
            $"{playerId} finished its turn, it is turn for {opponent} to move");
    }
    public void DisplayPlayerGameGrids(PlayerId playerId)
    {
        DisplayPlayerAction(playerId, "print");

        DisplayPlayerOcean(Players[playerId].Ships);
        DisplayTargetOcean(Players[playerId].Shoots);
    }

    private void DisplayPlayerOcean(List<Ship> ships)
    {
        _display.WriteLine(@"- My ocean grid:");
        var oceanPrinted = _oceanGridGenerator.GetPlayersOceanGrid(ships);
        _display.WriteLine(oceanPrinted);
    }

    private void DisplayTargetOcean(List<Shoot> shoots)
    {
        _display.WriteLine(@"- Target ocean grid:");
        var targetGrid = _oceanGridGenerator.GetTargetOceanGrid(shoots);
        _display.WriteLine(targetGrid);
    }

    private void DisplayPlayerAction(PlayerId playerId, string action)
    {
        _display.WriteLine($"{playerId} invoked: {action}");
    }
    private void DisplayPlayerBattleReport(PlayerId playerId, List<Shoot> shoots, List<Ship> opponentShips)
    {
        var battleReport1 = _oceanGridGenerator.GetPlayerBattleReport(playerId, shoots, opponentShips);
        _display.WriteLine(battleReport1);
    }

    private void DisplayGameWinner(PlayerId? winner)
    {
        _display.WriteLine($"Game finished! {winner} won!!");
    }
}