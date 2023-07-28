using Battleships.GameControls;
using Battleships.Ships;

namespace Battleships.Shoots;

public record SunkShoot(Coordinate Coordinate, ShipType? ShipType, IReadOnlyList<Coordinate> ShipCoordinates) : Shoot(Coordinate, ShootDamage.Sunk, ShipType, ShipCoordinates)
{
    public override string Announce => $"{ShipType} sunk!";
}