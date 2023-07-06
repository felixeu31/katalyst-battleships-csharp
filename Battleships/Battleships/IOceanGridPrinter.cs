namespace Battleships;

public interface IOceanGridPrinter
{
    string PrintPlayersOceanGrid(List<Ship> ships);
    string PrintTargetOceanGrid(List<Shoots> ships);
}