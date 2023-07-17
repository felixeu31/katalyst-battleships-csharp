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
        return Shoot.Water(coordinate);
    }

    public void AddShoot(Shoot shoot)
    {
        Shoots.Add(shoot);
    }
}