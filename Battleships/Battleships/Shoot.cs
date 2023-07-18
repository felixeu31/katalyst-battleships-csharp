namespace Battleships;

public record Shoot(Coordinate Coordinate, ShootDamage ShootDamage, ShipType? ShipType = null)
{
    public string Announce
    {
        get
        {
            if (ShootDamage == ShootDamage.Hit)
            {
                return "Hit";
            }
            if (ShootDamage == ShootDamage.Sunk)
            {
                return "Gun Ship sunk!";
            }

            return "Miss";
        }
    }

    public static Shoot Water(Coordinate coordinate)
    {
        return new Shoot(coordinate, ShootDamage.Water);
    }

    public static Shoot Hit(Coordinate coordinate)
    {
        return new Shoot(coordinate, ShootDamage.Hit);
    }

    public static Shoot Sunk(Coordinate coordinate)
    {
        return new Shoot(coordinate, ShootDamage.Sunk);
    }
}