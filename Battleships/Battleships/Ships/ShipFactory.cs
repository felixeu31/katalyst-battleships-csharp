using Battleships.GameControls;

namespace Battleships.Ships;

public static class ShipFactory
{
    public static Ship Build(params Coordinate[] coordinates)
    {
        switch (coordinates.Length)
        {
            case 1:
                return new Gunship(coordinates);
            case 3:
                return new Destroyer(coordinates);
            case 4:
                return new Carrier(coordinates);
            default:
                throw new Exception("Invalid set of coordinates to build ship");
        }
    }
}