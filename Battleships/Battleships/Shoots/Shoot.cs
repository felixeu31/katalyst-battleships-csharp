using Battleships.GameControls;
using Battleships.Ships;

namespace Battleships.Shoots;

public abstract record Shoot(Coordinate Coordinate, ShootDamage ShootDamage, ShipType? ShipType = null, IReadOnlyList<Coordinate>? ShipCoordinates = null)
{
    public abstract string Announce { get; }

    public static Shoot Miss(Coordinate coordinate)
    {
        return new WaterShoot(coordinate);
    }

    public static Shoot Hit(Coordinate coordinate)
    {
        return new HitShoot(coordinate);
    }

    public static Shoot Sunk(Coordinate coordinate, ShipType shipType, IReadOnlyList<Coordinate> shipCoordinates)
    {
        return new SunkShoot(coordinate, shipType, shipCoordinates);
    }
}