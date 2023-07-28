using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Generators;

public interface IOceanGridGeneratorFactory
{
    IOceanGridGenerator CreatePlayerOceanGridGenerator(IReadOnlyList<Ship> ships, int rowNumber = 10,
        int columnNumber = 10);
    IOceanGridGenerator CreateTargetOceanGridGenerator(IReadOnlyList<Shoot> shoots, int rowNumber = 10,
        int columnNumber = 10);
    IOceanGridGenerator CreateReportOceanGridGenerator(IReadOnlyList<Shoot> shoots, IReadOnlyList<Ship> opponentShips,
        int rowNumber = 10, int columnNumber = 10);
}