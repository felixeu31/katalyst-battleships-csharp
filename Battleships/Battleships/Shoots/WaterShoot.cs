using Battleships.GameControls;

namespace Battleships.Shoots;

public record  WaterShoot(Coordinate Coordinate) : Shoot(Coordinate, ShootDamage.Water)
{
    public override string Announce => "Miss";
}