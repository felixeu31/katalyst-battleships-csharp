namespace Battleships;

public record Shoot(Coordinate Coordinate, ShootDamage ShootDamage, ShipType? ShipType = null)
{
    public string Announce => string.Empty;
}

public enum ShootDamage
{
    Water,
    Hit,
    Sunk
}

public enum ShipType
{
    Gunship,
    Destroyer,
    Carrier
}