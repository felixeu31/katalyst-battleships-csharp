using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.GameControls;

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

    public Shoot ShootAt(Coordinate coordinate)
    {
        var ship = Ships.FirstOrDefault(x => x.Coordinates.Contains(coordinate));

        if (ship is not null)
        {
            return ship.ShootAt(coordinate);
        }

        return Shoot.Miss(coordinate);
    }

    public void AddShoot(Shoot shoot)
    {
        Shoots.Add(shoot);
    }
}