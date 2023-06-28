namespace Battleships;

public class Player
{
    public PlayerId PlayerId { get; }
    public List<List<Coordinates>> Ships { get; set; }

    public Player(PlayerId playerId, List<List<Coordinates>> ships)
    {
        PlayerId = playerId;
        Ships = ships;
    }

}