using Battleships.GameControls;
using Battleships.Ships;

namespace Battleships.Shoots;

public record SunkShoot(Coordinate Coordinate, ShipType? ShipType) : Shoot(Coordinate, ShootDamage.Sunk, ShipType)
{
    public override string Announce => "Gun Ship sunk!"; // TODO: several types of ships
}