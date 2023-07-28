using Battleships.GameControls;
using Battleships.Ships;

namespace Battleships.Generators;

public class PlayerOceanGridGenerator : OceanGridGenerator
{
    private readonly IReadOnlyList<Ship> _ships;

    public PlayerOceanGridGenerator(IReadOnlyList<Ship> ships, int rowNumber = 10, int columnNumber = 10) : base(rowNumber, columnNumber)
    {
        _ships = ships;
    }
    protected override string GetCoordinateRepresentation(Coordinate coordinate)
    {
        var match = _ships.SingleOrDefault(x => x.Coordinates.Contains(coordinate));

        if (match != null)
        {
            return ShipRepresentationMap[match.ShipType];
        }

        return " ";
    }
}