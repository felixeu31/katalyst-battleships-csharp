using System.Text;

namespace Battleships;

public class OceanGridPrinter
{
    private readonly int _rowNumber;
    private readonly int _columnNumber;

    public OceanGridPrinter(int rowNumber = 10, int columnNumber = 10)
    {
        _rowNumber = rowNumber;
        _columnNumber = columnNumber;
    }
    public string PrintOceanGrid(List<Ship> ships)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(PrintOceanGridHeader());

        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(PrintOceanGridRow(ships, row));
        }

        var result = stringBuilder.ToString();

        return result;
    }

    private string PrintOceanGridHeader()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"    |");
        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.Append($" {row} |");
        }
        return stringBuilder.ToString();
    }

    private string PrintOceanGridRow(List<Ship> ships, int row)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"   {row}|");

        for (int col = 0; col < _columnNumber; col++)
        {
            string cellRepresentation = GetCellRepresentation(ships, new Coordinates(row, col));
            stringBuilder.Append($" {cellRepresentation} |");
        }

        return stringBuilder.ToString();
    }

    private string GetCellRepresentation(List<Ship> ships, Coordinates coordinates)
    {
        var match = ships.SingleOrDefault(x => x.CoordinatesList.Contains(coordinates));

        if (match != null)
        {
            return match.Representation;
        }

        return " ";
    }
}