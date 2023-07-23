using Battleships.GameControls;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Generators;

public class TargetOceanGridGenerator : OceanGridGenerator
{
    private readonly List<Shoot> _shoots;

    public TargetOceanGridGenerator(List<Shoot> shoots, int rowNumber = 10, int columnNumber = 10) : base(rowNumber, columnNumber)
    {
        _shoots = shoots;
    }
    protected override string GetCoordinateRepresentation(Coordinate coordinate)
    {
        var match = _shoots.SingleOrDefault(x => x.Coordinate.Equals(coordinate));

        if (match != null)
        {
            if (match.ShootDamage.Equals(ShootDamage.Sunk) || BelongsToSunkShip(match, _shoots))
            {
                return ShootDamageRepresentationMap[ShootDamage.Sunk];
            }

            return ShootDamageRepresentationMap[match.ShootDamage];
        }

        return " ";
    }
    private static bool BelongsToSunkShip(Shoot shoot, List<Shoot> shoots)
    {
        return shoots.Any(x =>
            x.ShootDamage == ShootDamage.Sunk
            && x.ShipCoordinates != null &&
            x.ShipCoordinates.Contains(shoot.Coordinate));
    }
}