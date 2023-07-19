using Battleships.GameControls;

namespace Battleships.Ships;

public class Carrier : Ship
{
    public Carrier(params Coordinate[] coordinates) : base(coordinates)
    {
        
    }

    public override ShipType ShipType => ShipType.Carrier;
}