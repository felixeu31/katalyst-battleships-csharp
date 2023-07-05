namespace Battleships;

public class Player
{
    public PlayerId PlayerId { get; }
    public List<Ship> Ships { get; set; }

    public Player(PlayerId playerId, List<Ship> ships)
    {
        PlayerId = playerId;
        Ships = ships;
    }

}