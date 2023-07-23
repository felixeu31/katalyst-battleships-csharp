using Battleships.Generators;
using Battleships.Infrastructure;
using Battleships.Ships;
using Battleships.Shoots;
using System.Numerics;
using System.Text;

namespace Battleships.GameControls;

public class BattleshipGameDisplay : IBattleshipGameDisplay
{
    private readonly IDisplay _display;

    public BattleshipGameDisplay(IDisplay display)
    {
        _display = display;
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
        var oceanPrinted = new PlayerOceanGridGenerator(ships).GetGrid();
        _display.WriteLine(oceanPrinted);
    }

    public void DisplayTargetOcean(List<Shoot> shoots)
    {
        _display.WriteLine(@"- Target ocean grid:");
        var targetGrid = new TargetOceanGridGenerator(shoots).GetGrid();
        _display.WriteLine(targetGrid);
    }

    public void DisplayPlayerAction(PlayerId playerId, string action)
    {
        _display.WriteLine($"{playerId} invoked: {action}");
    }

    public void DisplayPlayerBattleReport(PlayerId playerId, List<Shoot> shoots, List<Ship> opponentShips)
    {
        _display.WriteLine($"# {playerId} battle report");
        _display.WriteLine($"Total shots: {shoots.Count}");
        _display.WriteLine($"Misses: {shoots.Count(x => x.ShootDamage == ShootDamage.Water)}");
        _display.WriteLine($"Hits: {shoots.Count(x => x.ShootDamage != ShootDamage.Water)}");

        _display.WriteLine(GetShunkshipsRepresentation(opponentShips));

        var battleReport1 = new ReportOceanGridGenerator(shoots, opponentShips).GetGrid();
        _display.WriteLine(battleReport1);
    }

    public void DisplayGameWinner(PlayerId? winner)
    {
        _display.WriteLine($"Game finished! {winner} won!!");
    }
    private string GetShunkshipsRepresentation(List<Ship> opponentShips)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine("Ships Sunk: [");

        var sunkShips = opponentShips.Where(x => x.IsSunk).ToList();
        foreach (var sunkShip in sunkShips)
        {
            stringBuilder.AppendLine(
                $"\t{sunkShip.ShipType}: ({sunkShip.Coordinates[0].XPosition},{sunkShip.Coordinates[0].YPosition}){(sunkShips.Last() == sunkShip ? "" : ",")}");

        }

        stringBuilder.Append("]");

        var result = stringBuilder.ToString();

        return result;
    }
}