using System.Text;

namespace Battleships;

public class OceanGridPrinter
{
    public string PrintOceanGrid(List<Ship> ships)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.AppendLine();
        stringBuilder.Append("    | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 |");

        for (int row = 0; row < 10; row++)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append($"   {row}|");

            for (int col = 0; col < 10; col++)
            {
                string cellRepresentation = GetCellRepresentation(ships, row, col);
                stringBuilder.Append($" {cellRepresentation} |");
            }
        }

        var result = stringBuilder.ToString();

        return result;
    }

    private string GetCellRepresentation(List<Ship> ships, int row, int col)
    {
        var match = ships.SingleOrDefault(x => x.CoordinatesList.Any(y => y.XPosition == row && y.YPosition == col));

        if (match != null)
        {
            return match.Representation;
        }

        return " ";
    }
}