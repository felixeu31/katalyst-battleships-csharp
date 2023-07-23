using Battleships.GameControls;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Printers;

public interface IOceanGridGenerator
{
    string GetPlayersOceanGrid(List<Ship> ships);
    string GetTargetOceanGrid(List<Shoot> ships);
    string GetPlayerBattleReport(PlayerId player, List<Shoot> shoots, List<Ship> opponentShips);
}