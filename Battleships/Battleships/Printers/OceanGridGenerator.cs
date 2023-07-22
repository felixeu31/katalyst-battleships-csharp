using System.Text;
using Battleships.GameControls;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Printers;

public class OceanGridGenerator : IOceanGridGenerator //todo ¿? extract base class or separate in two classes
{
    private readonly int _rowNumber;
    private readonly int _columnNumber;
    private readonly Dictionary<ShootDamage, string> _shootDamageRepresentationMap = new()
    {
        { ShootDamage.Water , "o"},
        { ShootDamage.Hit , "x"},
        { ShootDamage.Sunk , "X"}
    };
    private readonly Dictionary<ShipType, string> _shipRepresentationMap = new()
    {
        { ShipType.Gunship , "g"},
        { ShipType.Destroyer , "d"},
        { ShipType.Carrier , "c"}
    };

    public OceanGridGenerator(int rowNumber = 10, int columnNumber = 10)
    {
        _rowNumber = rowNumber;
        _columnNumber = columnNumber;
    }
    public string GeneratePlayersOceanGrid(List<Ship> ships)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(GenerateOceanGridHeader());

        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(GenerateOceanGridRow(ships, row));
        }

        var result = stringBuilder.ToString();

        return result;
    }

    public string GenerateTargetOceanGrid(List<Shoot> ships)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(GenerateOceanGridHeader());

        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(GenerateOceanGridRow(ships, row));
        }

        var result = stringBuilder.ToString();

        return result;
    }

    public string GetBattleReport(PlayerId player, List<Shoot> shoots, List<Ship> opponentShips)
    {
        throw new NotImplementedException();
    }

    private string GenerateOceanGridHeader()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"    |");
        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.Append($" {row} |");
        }
        return stringBuilder.ToString();
    }

    private string GenerateOceanGridRow(List<Ship> ships, int row)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"   {row}|");

        for (int col = 0; col < _columnNumber; col++)
        {
            string cellRepresentation = GetCellRepresentation(ships, new Coordinate(row, col));
            stringBuilder.Append($" {cellRepresentation} |");
        }

        return stringBuilder.ToString();
    }
    private string GenerateOceanGridRow(List<Shoot> shoots, int row)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"   {row}|");

        for (int col = 0; col < _columnNumber; col++)
        {
            string cellRepresentation = GetCellRepresentation(shoots, new Coordinate(row, col));
            stringBuilder.Append($" {cellRepresentation} |");
        }

        return stringBuilder.ToString();
    }

    private string GetCellRepresentation(List<Ship> ships, Coordinate coordinate)
    {
        var match = ships.SingleOrDefault(x => x.Coordinates.Contains(coordinate));

        if (match != null)
        {
            return _shipRepresentationMap[match.ShipType];
        }

        return " ";
    }
    private string GetCellRepresentation(List<Shoot> shoots, Coordinate coordinate)
    {
        var match = shoots.SingleOrDefault(x => x.Coordinate.Equals(coordinate));

        if (match != null)
        {
            if (match.ShootDamage.Equals(ShootDamage.Sunk) || BelongsToSunkShip(match, shoots, coordinate))
            {
                return _shootDamageRepresentationMap[ShootDamage.Sunk];
            }

            return _shootDamageRepresentationMap[match.ShootDamage];
        }

        return " ";
    }

    private static bool BelongsToSunkShip(Shoot shoot, List<Shoot> shoots, Coordinate coordinate)
    {
        return shoots.Any(x =>
            x.ShootDamage == ShootDamage.Sunk 
            && x.ShipCoordinates != null &&
            x.ShipCoordinates.Contains(shoot.Coordinate));
    }
}