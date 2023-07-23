using Battleships.Generators;
using Battleships.Infrastructure;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.GameControls;

public class BattleshipGameDisplay : IBattleshipGameDisplay
{
    private readonly IDisplay _display;
    private readonly IOceanGridGenerator _oceanGridGenerator;

    public BattleshipGameDisplay(IDisplay display, IOceanGridGenerator oceanGridGenerator)
    {
        _display = display;
        _oceanGridGenerator = oceanGridGenerator;
    }

    public void DisplayGameWelcome()
    {
        _display.WriteLine("Welcome to Battleship game!");
    }

    public void DisplayGameStarted(PlayerId playerId)
    {
        _display.WriteLine($"Game started! {playerId.ToString()} starts moving");
    }

    public void DisplayAddedPlayer(PlayerId playerId)
    {
        _display.WriteLine($"{playerId.ToString()} added to the game");
    }

    public void DisplayShootResult(Shoot shoot)
    {
        _display.WriteLine(shoot.Announce);
    }

    public void DisplayPlayerEndedTurn(PlayerId playerId, PlayerId opponent)
    {
        _display.WriteLine(
            $"{playerId} finished its turn, it is turn for {opponent} to move");
    }

    public void DisplayPlayerOcean(List<Ship> ships)
    {
        _display.WriteLine(@"- My ocean grid:");
        var oceanPrinted = _oceanGridGenerator.GetPlayersOceanGrid(ships);
        _display.WriteLine(oceanPrinted);
    }

    public void DisplayTargetOcean(List<Shoot> shoots)
    {
        _display.WriteLine(@"- Target ocean grid:");
        var targetGrid = _oceanGridGenerator.GetTargetOceanGrid(shoots);
        _display.WriteLine(targetGrid);
    }

    public void DisplayPlayerAction(PlayerId playerId, string action)
    {
        _display.WriteLine($"{playerId} invoked: {action}");
    }

    public void DisplayPlayerBattleReport(PlayerId playerId, List<Shoot> shoots, List<Ship> opponentShips)
    {
        var battleReport1 = _oceanGridGenerator.GetPlayerBattleReport(playerId, shoots, opponentShips);
        _display.WriteLine(battleReport1);
    }

    public void DisplayGameWinner(PlayerId? winner)
    {
        _display.WriteLine($"Game finished! {winner} won!!");
    }
}