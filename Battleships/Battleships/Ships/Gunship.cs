﻿using Battleships.GameControls;

namespace Battleships.Ships;

public class Gunship : Ship
{
    public Gunship(params Coordinate[] coordinates) : base(coordinates)
    {
        
    }
    public override string Representation => "g";
    public override ShipType ShipType => ShipType.Gunship;
}