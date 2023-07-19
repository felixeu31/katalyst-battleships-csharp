using Battleships.GameControls;

namespace Battleships.Ships;

public class Destroyer : Ship
{
    public Destroyer(params Coordinate[] coordinates) : base(coordinates)
    {
        
    }

    public override ShipType ShipType => ShipType.Destroyer;
}