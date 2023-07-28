using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Generators;

public class OceanGridGeneratorFactory : IOceanGridGeneratorFactory
{
    public IOceanGridGenerator CreatePlayerOceanGridGenerator(IReadOnlyList<Ship> ships, int rowNumber = 10,
        int columnNumber = 10)
    {
        return new PlayerOceanGridGenerator(ships, rowNumber, columnNumber);
    }

    public IOceanGridGenerator CreateTargetOceanGridGenerator(IReadOnlyList<Shoot> shoots, int rowNumber = 10,
        int columnNumber = 10)
    {
        return new TargetOceanGridGenerator(shoots, rowNumber, columnNumber);
    }
    public IOceanGridGenerator CreateReportOceanGridGenerator(IReadOnlyList<Shoot> shoots,
        IReadOnlyList<Ship> opponentShips, int rowNumber = 10, int columnNumber = 10)
    {
        return new ReportOceanGridGenerator(shoots, opponentShips, rowNumber, columnNumber);
    }
}