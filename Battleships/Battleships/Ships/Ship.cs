using Battleships.GameControls;
using Battleships.Shoots;

namespace Battleships.Ships;

public abstract class Ship
{
    private readonly Coordinate[] _coordinates;
    private List<Coordinate> _hitCoordinates;

    public abstract ShipType ShipType { get; }

    public Ship(params Coordinate[] coordinates)
    {
        _coordinates = coordinates;
        _hitCoordinates = new List<Coordinate>();
    }
    public Coordinate[] Coordinates => _coordinates;
    public IReadOnlyList<Coordinate> HitCoordinates => _hitCoordinates;
    
    public bool IsSunk => _coordinates.All(x => HitCoordinates.Contains(x));

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