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

    public Shoot GetShootAt(Coordinate coordinate)
    {
        throw new NotImplementedException();
    }

    public void AddShoot(Shoot shoot)
    {
        throw new NotImplementedException();
    }
}