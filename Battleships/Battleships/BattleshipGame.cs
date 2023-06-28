namespace Battleships;

public class BattleshipGame
{
    private readonly IPrinter _printer;

    public BattleshipGame(IPrinter printer)
    {
        _printer = printer;
        Players = new Dictionary<PlayerId, Player>();
        _printer.WriteLine("Welcome to Battleship game!");
    }

    public Dictionary<PlayerId, Player> Players { get; set; }


    public void AddPlayer(PlayerId playerId, List<List<Coordinates>> ships)
    {
        Players.Add(playerId, new Player(playerId, ships));
    }

    public void StartGame(PlayerId playerId)
    {
        throw new NotImplementedException();
    }

    public void EndTurn(PlayerId playerId)
    {
        throw new NotImplementedException();
    }

    public void Print(PlayerId playerId)
    {
        throw new NotImplementedException();
    }
}