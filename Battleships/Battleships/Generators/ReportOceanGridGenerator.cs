using Battleships.GameControls;
using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.Generators;

public class ReportOceanGridGenerator : OceanGridGenerator
{
    private readonly IReadOnlyList<Shoot> _shoots;
    private readonly IReadOnlyList<Ship> _opponentShips;

    public ReportOceanGridGenerator(IReadOnlyList<Shoot> shoots, IReadOnlyList<Ship> opponentShips, int rowNumber = 10,
        int columnNumber = 10) : base(rowNumber, columnNumber)
    {
        _shoots = shoots;
        _opponentShips = opponentShips;
    }
    protected override string GetCoordinateRepresentation(Coordinate coordinate)
    {
        var shootMatch = _shoots.SingleOrDefault(x => x.Coordinate.Equals(coordinate));

        if (shootMatch != null)
        {
            if (shootMatch.ShootDamage.Equals(ShootDamage.Sunk) || BelongsToSunkShip(shootMatch, _shoots))
            {
                return ShootDamageRepresentationMap[ShootDamage.Sunk];
            }

            return ShootDamageRepresentationMap[shootMatch.ShootDamage];
        }

        var shipMatch = _opponentShips.SingleOrDefault(x => x.Coordinates.Contains(coordinate));

        if (shipMatch != null)
        {
            return ShipRepresentationMap[shipMatch.ShipType];
        }

        return " ";
    }
    private static bool BelongsToSunkShip(Shoot shoot, IReadOnlyList<Shoot> shoots)
    {
        return shoots.Any(x =>
            x.ShootDamage == ShootDamage.Sunk
            && x.ShipCoordinates != null &&
            x.ShipCoordinates.Contains(shoot.Coordinate));
    }
}