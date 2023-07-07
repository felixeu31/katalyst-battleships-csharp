using System.Text;

namespace Battleships;

public class OceanGridGenerator : IOceanGridGenerator
{
    private readonly int _rowNumber;
    private readonly int _columnNumber;

    public OceanGridGenerator(int rowNumber = 10, int columnNumber = 10)
    {
        _rowNumber = rowNumber;
        _columnNumber = columnNumber;
    }
    public string GeneratePlayersOceanGrid(List<Ship> ships)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(GenerateceanGridHeader());

        for (int row = 0; row < _rowNumber; row++)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append(GenerateOceanGridRow(ships, row));
        }

        var result = stringBuilder.ToString();

        return result;
    }

    public string GenerateTargetOceanGrid(List<Shoots> ships)
    {
        return @"    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |
   0|   |   |   |   |   |   |   |   |   |   |
   1|   |   |   |   |   |   |   |   |   |   |
   2|   |   |   |   |   |   |   |   |   |   |
   3|   |   |   |   |   |   |   |   |   |   |
   4|   |   |   |   |   |   |   |   |   |   |
   5|   |   |   |   |   |   |   |   |   |   |
   6|   |   |   |   |   |   |   |   |   |   |
   7|   |   |   |   |   |   |   |   |   |   |
   8|   |   |   |   |   |   |   |   |   |   |
   9|   |   |   |   |   |   |   |   |   |   |";
    }

    private string GenerateceanGridHeader()
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

    private string GetCellRepresentation(List<Ship> ships, Coordinate coordinate)
    {
        var match = ships.SingleOrDefault(x => x.Coordinates.Contains(coordinate));

        if (match != null)
        {
            return match.Representation;
        }

        return " ";
    }
}