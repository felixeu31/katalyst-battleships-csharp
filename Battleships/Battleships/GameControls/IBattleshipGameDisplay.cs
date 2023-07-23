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
    void DisplayPlayerOcean(List<Ship> ships);
    void DisplayTargetOcean(List<Shoot> shoots);
    void DisplayPlayerAction(PlayerId playerId, string action);
    void DisplayPlayerBattleReport(PlayerId playerId, List<Shoot> shoots, List<Ship> opponentShips);
    void DisplayGameWinner(PlayerId? winner);
}