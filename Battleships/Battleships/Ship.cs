namespace Battleships;

public class Ship
{
    private readonly Coordinates[] _coordinatesList;

    public Ship(params Coordinates[] coordinatesList)
    {
        _coordinatesList = coordinatesList;
    }

    public Coordinates[] CoordinatesList => _coordinatesList;

    public string Representation
    {
        get
        {
            if (_coordinatesList.Length == 1)
            {
                return "g";
            }

            return string.Empty;
        }
    }
}