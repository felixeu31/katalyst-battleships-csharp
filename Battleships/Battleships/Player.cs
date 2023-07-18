using Battleships.Shoots;

namespace Battleships;

public class Player
{
    public PlayerId PlayerId { get; }
    public List<Ship> Ships { get; set; }
    public List<Shoot> Shoots { get; set; }

    public Player(PlayerId playerId, List<Ship> ships)
    {
        PlayerId = playerId;
        Ships = ships;
        Shoots = new List<Shoot>();
    }

    public Shoot ShootedAt(Coordinate coordinate)
    {
        var ship = Ships.FirstOrDefault(x => x.Coordinates.Contains(coordinate));

        if (ship is not null)
        {
            ship.Hit(coordinate);

            if (ship.IsSunk)
            {
                return Shoot.Sunk(coordinate, ship.ShipType);
            }

            return Shoot.Hit(coordinate);
        }

        return Shoot.Water(coordinate);
    }

    public void AddShoot(Shoot shoot)
    {
        Shoots.Add(shoot);
    }
}