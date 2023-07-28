using Battleships.GameControls;
using Battleships.Shoots;

namespace Battleships.Ships;

public abstract class Ship
{
    private readonly IReadOnlyList<Coordinate> _coordinates;
    private readonly List<Coordinate> _hitCoordinates;

    public abstract ShipType ShipType { get; }

    public Ship(params Coordinate[] coordinates)
    {
        _coordinates = coordinates;
        _hitCoordinates = new List<Coordinate>();
    }
    public IReadOnlyList<Coordinate> Coordinates => _coordinates;
    
    public bool IsSunk => _coordinates.All(x => _hitCoordinates.Contains(x));

    public Shoot ShootAt(Coordinate coordinate)
    {
        _hitCoordinates.Add(coordinate);

        if (IsSunk)
        {
            return Shoot.Sunk(coordinate, ShipType, _coordinates);
        }

        return Shoot.Hit(coordinate);
    }
}