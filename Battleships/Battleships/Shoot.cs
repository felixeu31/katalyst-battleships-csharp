namespace Battleships;

public record Shoot(Coordinate Coordinate, ShootDamage ShootDamage, ShipType? ShipType = null)
{
    public string Announce => "Miss";

    public static Shoot Water(Coordinate coordinate)
    {
        return new Shoot(coordinate, ShootDamage.Water);
    }
}