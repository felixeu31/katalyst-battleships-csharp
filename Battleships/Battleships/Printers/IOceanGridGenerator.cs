using Battleships.GameControls;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Printers;

public interface IOceanGridGenerator
{
    string GeneratePlayersOceanGrid(List<Ship> ships);
    string GenerateTargetOceanGrid(List<Shoot> ships);
    string GetBattleReport(PlayerId player, List<Shoot> shoots, List<Ship> opponentShips);
}