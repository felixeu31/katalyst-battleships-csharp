namespace Battleships;

public class BattleshipGame
{
    private readonly IPrinter _printer;

    public BattleshipGame(IPrinter printer)
    {
        _printer = printer;
        _printer.WriteLine("Welcome to Battleship game!");
    }

    public void AddPlayer(List<List<Coordinates>> ships)
    {
        throw new NotImplementedException();
    }

    public void StartGame(Player player)
    {
        throw new NotImplementedException();
    }

    public void EndTurn(Player player)
    {
        throw new NotImplementedException();
    }

    public void Print(Player player)
    {
        throw new NotImplementedException();
    }
}