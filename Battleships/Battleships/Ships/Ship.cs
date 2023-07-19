using Battleships.GameControls;
using Battleships.Shoots;

namespace Battleships.Ships;

public abstract class Ship
{
    private readonly Coordinate[] _coordinates;

    public Coordinate[] Coordinates => _coordinates;
    public List<Coordinate> HitCoordinates { get; set; } = new List<Coordinate>();
    public abstract ShipType ShipType { get; }

    public Ship(params Coordinate[] coordinates)
    {
        _coordinates = coordinates;
    }
    
    public bool IsSunk => _coordinates.All(x => HitCoordinates.Contains(x));

    public Shoot ShootAt(Coordinate coordinate)
    {
        HitCoordinates.Add(coordinate);

        if (IsSunk)
        {
            return Shoot.Sunk(coordinate, ShipType);
        }

        return Shoot.Hit(coordinate);
    }
}