using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Generators;

public class OceanGridGeneratorFactory : IOceanGridGeneratorFactory
{
    public IOceanGridGenerator CreatePlayerOceanGridGenerator(List<Ship> ships, int rowNumber = 10, int columnNumber = 10)
    {
        return new PlayerOceanGridGenerator(ships, rowNumber, columnNumber);
    }

    public IOceanGridGenerator CreateTargetOceanGridGenerator(List<Shoot> shoots, int rowNumber = 10, int columnNumber = 10)
    {
        return new TargetOceanGridGenerator(shoots, rowNumber, columnNumber);
    }
    public IOceanGridGenerator CreateTargetOceanGridGenerator(List<Shoot> shoots, List<Ship> opponentShips, int rowNumber = 10, int columnNumber = 10)
    {
        return new ReportOceanGridGenerator(shoots, opponentShips, rowNumber, columnNumber);
    }
}