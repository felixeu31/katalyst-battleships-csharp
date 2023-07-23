using System;
using Battleships.Printers;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.GameControls;

public class BattleshipGame
{
    private readonly IBattleshipGameDisplay _gameDisplay;
    public Dictionary<PlayerId, Player> Players { get; set; }

    public BattleshipGame(IBattleshipGameDisplay gameDisplay)
    {
        _gameDisplay = gameDisplay;
        Players = new Dictionary<PlayerId, Player>();
        _gameDisplay.DisplayGameWelcome();
    }

    public void AddPlayer(PlayerId playerId, List<Ship> ships)
    {
        Players.Add(playerId, new Player(playerId, ships));
        _gameDisplay.DisplayAddedPlayer(playerId);
    }

    public void StartGame(PlayerId playerId)
    {
        _gameDisplay.DisplayPlayerAction(playerId, "start");
        _gameDisplay.DisplayGameStarted(playerId);
    }


    public void Fire(PlayerId playerId, Coordinate coordinate)
    {
        _gameDisplay.DisplayPlayerAction(playerId, "fire");

        var shoot = Players[GetOpponent(playerId)].ShootAt(coordinate);

        Players[playerId].AddShoot(shoot);

        _gameDisplay.DisplayShootResult(shoot);
    }

    public void EndTurn(PlayerId playerId)
    {
        _gameDisplay.DisplayPlayerAction(playerId, "end turn");

        if (this.IsFinished)
        {
            _gameDisplay.DisplayGameWinner(this.Winner);
            _gameDisplay.DisplayPlayerBattleReport(PlayerId.Player1, Players[PlayerId.Player1].Shoots, Players[GetOpponent(PlayerId.Player1)].Ships);
            _gameDisplay.DisplayPlayerBattleReport(PlayerId.Player2, Players[PlayerId.Player2].Shoots, Players[GetOpponent(PlayerId.Player2)].Ships);
        }
        else
        {
            _gameDisplay.DisplayPlayerEndedTurn(playerId, GetOpponent(playerId));
        }
    }
    public void Print(PlayerId playerId)
    {
        _gameDisplay.DisplayPlayerAction(playerId, "print");

        _gameDisplay.DisplayPlayerOcean(Players[playerId].Ships);
        _gameDisplay.DisplayTargetOcean(Players[playerId].Shoots);
    }

    public bool IsFinished => Players.Any(x => x.Value.Ships.All(s => s.IsSunk));
    public PlayerId? Winner => IsFinished ? GetOpponent(Players.Single(x => x.Value.Ships.All(s => s.IsSunk)).Key) : null;

    private PlayerId GetOpponent(PlayerId playerId)
    {
        if (playerId.Equals(PlayerId.Player1))
            return PlayerId.Player2;

        return PlayerId.Player1;
    }

}