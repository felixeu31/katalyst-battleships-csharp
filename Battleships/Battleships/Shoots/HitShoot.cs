using Battleships.GameControls;

namespace Battleships.Shoots;

public record  HitShoot(Coordinate Coordinate) : Shoot(Coordinate, ShootDamage.Hit)
{
    public override string Announce => "Hit";
}