using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Generators;

public interface IOceanGridGeneratorFactory
{
    IOceanGridGenerator CreatePlayerOceanGridGenerator(List<Ship> ships, int rowNumber = 10, int columnNumber = 10);
    IOceanGridGenerator CreateTargetOceanGridGenerator(List<Shoot> shoots, int rowNumber = 10, int columnNumber = 10);
    IOceanGridGenerator CreateTargetOceanGridGenerator(List<Shoot> shoots, List<Ship> opponentShips, int rowNumber = 10, int columnNumber = 10);
}