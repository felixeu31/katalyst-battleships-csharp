using Battleships.Ships;
using Battleships.Shoots;

namespace Battleships.GameControls;

public interface IBattleshipGameDisplay
{
    void DisplayGameWelcome();
    void DisplayGameStarted(PlayerId playerId);
    void DisplayAddedPlayer(PlayerId playerId);
    void DisplayShootResult(Shoot shoot);
    void DisplayPlayerEndedTurn(PlayerId playerId, PlayerId opponent);
    void DisplayPlayerOcean(IReadOnlyList<Ship> ships);
    void DisplayTargetOcean(IReadOnlyList<Shoot> shoots);
    void DisplayPlayerAction(PlayerId playerId, string action);
    void DisplayPlayerBattleReport(PlayerId playerId, IReadOnlyList<Shoot> shoots, IReadOnlyList<Ship> opponentShips);
    void DisplayGameWinner(PlayerId? winner);
}