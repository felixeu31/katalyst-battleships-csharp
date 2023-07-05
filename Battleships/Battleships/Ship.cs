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
            if (_coordinatesList.Length == 3)
            {
                return "d";
            }
            if (_coordinatesList.Length == 4)
            {
                return "c";
            }

            return " ";
        }
    }
}