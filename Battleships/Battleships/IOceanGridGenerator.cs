﻿namespace Battleships;

public interface IOceanGridGenerator
{
    string GeneratePlayersOceanGrid(List<Ship> ships);
    string GenerateTargetOceanGrid(List<Shoots> ships);
}