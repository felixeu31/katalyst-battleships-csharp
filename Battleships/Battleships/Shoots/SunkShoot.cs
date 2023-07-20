using Battleships.GameControls;
using Battleships.Ships;

namespace Battleships.Shoots;

public record SunkShoot(Coordinate Coordinate, ShipType? ShipType, Coordinate[] ShipCoordinates) : Shoot(Coordinate, ShootDamage.Sunk, ShipType, ShipCoordinates)
{
    public override string Announce => $"{ShipType} sunk!";
}