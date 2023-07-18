namespace Battleships.Shoots;

public abstract record Shoot(Coordinate Coordinate, ShootDamage ShootDamage, ShipType? ShipType = null)
{
    public abstract string Announce { get; }

    public static Shoot Water(Coordinate coordinate)
    {
        return new WaterShoot(coordinate);
    }

    public static Shoot Hit(Coordinate coordinate)
    {
        return new HitShoot(coordinate);
    }

    public static Shoot Sunk(Coordinate coordinate, ShipType shipType)
    {
        return new SunkShoot(coordinate, shipType);
    }
}