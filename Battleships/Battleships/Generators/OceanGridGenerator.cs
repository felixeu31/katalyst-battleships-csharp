using Battleships.GameControls;
using Battleships.Ships;
using Battleships.Shoots;
using System.Text;

namespace Battleships.Generators;

public abstract class OceanGridGenerator : IOceanGridGenerator
{
    private readonly int _rowNumber;
    private readonly int _columnNumber;

    protected readonly Dictionary<ShootDamage, string> ShootDamageRepresentationMap = new()
    {
        { ShootDamage.Water , "o"},
        { ShootDamage.Hit , "x"},
        { ShootDamage.Sunk , "X"}
    };
    protected readonly Dictionary<ShipType, string> ShipRepresentationMap = new()
    {
        { ShipType.Gunship , "g"},
        { ShipType.Destroyer , "d"},
        { ShipType.Carrier , "c"}
    };

    protected OceanGridGenerator(int rowNumber = 10, int columnNumber = 10)
    {
        _rowNumber = rowNumber;
        _columnNumber = columnNumber;
    }


    public string GetGrid()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(GetHeader());

        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(GetRow(row));
        }

        var result = stringBuilder.ToString();

        return result;
    }

    private string GetHeader()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"    |");
        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.Append($" {row} |");
        }
        return stringBuilder.ToString();
    }

    private string GetRow(int row)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"   {row}|");

        for (int col = 0; col < _columnNumber; col++)
        {
            string cellRepresentation = GetCoordinateRepresentation(new Coordinate(row, col));
            stringBuilder.Append($" {cellRepresentation} |");
        }

        return stringBuilder.ToString();
    }

    protected abstract string GetCoordinateRepresentation(Coordinate coordinate);
}