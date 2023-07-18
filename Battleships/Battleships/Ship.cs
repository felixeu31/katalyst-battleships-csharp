namespace Battleships;

public class Ship
{
    private readonly Coordinate[] _coordinates;

    public Ship(params Coordinate[] coordinates)
    {
        _coordinates = coordinates;
    }

    public Coordinate[] Coordinates => _coordinates;

    public string Representation
    {
        get
        {
            if (_coordinates.Length == 1)
            {
                return "g";
            }
            if (_coordinates.Length == 3)
            {
                return "d";
            }
            if (_coordinates.Length == 4)
            {
                return "c";
            }

            return " ";
        }
    }

    public List<Coordinate> HitCoordinates { get; set; } = new List<Coordinate>();
    public bool IsSunk => _coordinates.All(x => HitCoordinates.Contains(x));

    public void Hit(Coordinate coordinate)
    {
        HitCoordinates.Add(coordinate);
    }
}