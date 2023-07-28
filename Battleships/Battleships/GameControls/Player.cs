using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.GameControls;

public class Player
{
    public readonly PlayerId PlayerId;
    public readonly IReadOnlyList<Ship> Ships;
    public readonly List<Shoot> _shoots;

    public IReadOnlyList<Shoot> Shoots => _shoots;

    public Player(PlayerId playerId, List<Ship> ships)
    {
        PlayerId = playerId;
        Ships = ships;
        _shoots = new List<Shoot>();
    }

    public Shoot ShootAt(Coordinate coordinate)
    {
        var ship = Ships.FirstOrDefault(x => x.Coordinates.Contains(coordinate));

        Shoot newShoot;

        if (ship is not null)
        {
            newShoot = ship.ShootAt(coordinate);
        }
        else
        {
            newShoot = Shoot.Miss(coordinate);
        }

        return newShoot;
    }

    public void AddShoot(Shoot shoot)
    {
        _shoots.Add(shoot);
    }
}