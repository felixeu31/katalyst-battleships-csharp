using Battleships.GameControls;
using Battleships.Printers;
using Battleships.Ships;
using Battleships.Shoots;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Bson;

namespace Battleships.Tests.Unit;

public class BattleshipGameTest
{
    [Fact]
    public void new_game_should_print_greeting()
    {
        // arrange
        // act
        var (gameDisplayMock, game) = NewGame();

        // assert
        gameDisplayMock.Verify(x => x.DisplayGameWelcome());
    }

    [Fact]
    public void should_add_player_with_ships()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>();
        game.AddPlayer(PlayerId.Player1, ships);

        // Assert
        game.Players.Should().HaveCount(1);
        game.Players.Should().ContainKey(PlayerId.Player1);
        game.Players[PlayerId.Player1].Should().NotBeNull();
        game.Players[PlayerId.Player1].Ships.Should().BeEquivalentTo(ships);
        gameDisplayMock.Verify(x => x.DisplayAddedPlayer(PlayerId.Player1));
    }

    [Fact]
    public void should_add_second_player()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, new List<Ship>());

        // Assert
        game.Players.Should().ContainKey(PlayerId.Player1);
        gameDisplayMock.Verify(x => x.DisplayAddedPlayer(PlayerId.Player1));
        game.Players.Should().ContainKey(PlayerId.Player2);
        gameDisplayMock.Verify(x => x.DisplayAddedPlayer(PlayerId.Player2));
    }

    [Fact]
    public void should_inform_users_when_game_starts()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, new List<Ship>());
        game.StartGame(PlayerId.Player1);

        // Arrange
        gameDisplayMock.Verify(x => x.DisplayPlayerAction(PlayerId.Player1, "start"));
        gameDisplayMock.Verify(x => x.DisplayGameStarted(PlayerId.Player1));
    }

    [Fact]
    public void should_print_player_game()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
            ShipFactory.Build(new Coordinate(4, 6)),
            ShipFactory.Build(new Coordinate(7, 1)),
            ShipFactory.Build(new Coordinate(9, 9)),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, ships);
        game.AddPlayer(PlayerId.Player2, new List<Ship>());
        game.StartGame(PlayerId.Player1);
        game.Print(PlayerId.Player1);

        // Arrange
        gameDisplayMock.Verify(x => x.DisplayPlayerAction(PlayerId.Player1, "print"));
        gameDisplayMock.Verify(x => x.DisplayPlayerOcean(ships));
        gameDisplayMock.Verify(x => x.DisplayTargetOcean(new List<Shoot>()));
    }

    [Fact]
    public void should_inform_users_when_user_ends()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>(){
            ShipFactory.Build(new Coordinate(2, 7)),});
        game.AddPlayer(PlayerId.Player2, new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
        });
        game.StartGame(PlayerId.Player1);
        game.EndTurn(PlayerId.Player1);

        // Arrange
        gameDisplayMock.Verify(x => x.DisplayPlayerAction(PlayerId.Player1, "end turn"));
        gameDisplayMock.Verify(x => x.DisplayPlayerEndedTurn(PlayerId.Player1, PlayerId.Player2));
    }

    [Fact]
    public void should_register_player_water_shoot()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
            ShipFactory.Build(new Coordinate(4, 6)),
            ShipFactory.Build(new Coordinate(7, 1)),
            ShipFactory.Build(new Coordinate(9, 9)),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(3, 0));

        // Arrange
        var shoot = game.Players[PlayerId.Player1].Shoots[0];
        shoot.Should().Be(Shoot.Miss(new Coordinate(3, 0)));
        shoot.Announce.Should().Be("Miss");
    }


    [Fact]
    public void should_register_player_hit_shoot()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
            ShipFactory.Build(new Coordinate(4, 6)),
            ShipFactory.Build(new Coordinate(7, 1)),
            ShipFactory.Build(new Coordinate(9, 9)),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(3, 2));

        // Arrange
        var shoot = game.Players[PlayerId.Player1].Shoots[0];
        shoot.Should().BeEquivalentTo(Shoot.Hit(new Coordinate(3, 2)));
        shoot.Announce.Should().Be("Hit");
    }

    [Fact]
    public void should_register_player_gunship_sunk_shoot()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
            ShipFactory.Build(new Coordinate(4, 6)),
            ShipFactory.Build(new Coordinate(7, 1)),
            ShipFactory.Build(new Coordinate(9, 9)),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(2, 7));

        // Arrange
        var shoot = game.Players[PlayerId.Player1].Shoots[0];
        shoot.Should().BeEquivalentTo(Shoot.Sunk(new Coordinate(2, 7), ShipType.Gunship, new []{ new Coordinate(2, 7) }));
        shoot.Announce.Should().Be("Gunship sunk!");
    }
    [Fact]
    public void should_register_player_destroyer_sunk_shoot()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
            ShipFactory.Build(new Coordinate(4, 6)),
            ShipFactory.Build(new Coordinate(7, 1)),
            ShipFactory.Build(new Coordinate(9, 9)),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(3, 2));
        game.Fire(PlayerId.Player1, new Coordinate(3, 3));
        game.Fire(PlayerId.Player1, new Coordinate(3, 4));

        // Arrange
        var shoot = game.Players[PlayerId.Player1].Shoots[2];
        shoot.Coordinate.Should().Be(new Coordinate(3, 4));
        shoot.ShipType.Should().Be(ShipType.Destroyer);
        shoot.ShootDamage.Should().Be(ShootDamage.Sunk);
        shoot.ShipCoordinates.Should().BeEquivalentTo(new[] { new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4) });
        shoot.Announce.Should().Be("Destroyer sunk!");
    }

    [Fact]
    public void should_print_target_ocean_with_shoots()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
            ShipFactory.Build(new Coordinate(4, 6)),
            ShipFactory.Build(new Coordinate(7, 1)),
            ShipFactory.Build(new Coordinate(9, 9)),
            ShipFactory.Build(new Coordinate(3, 2), new Coordinate(3, 3), new Coordinate(3, 4)),
            ShipFactory.Build(new Coordinate(7, 5), new Coordinate(8, 5), new Coordinate(9, 5)),
            ShipFactory.Build(new Coordinate(4, 8), new Coordinate(5, 8), new Coordinate(6, 8), new Coordinate(7, 8)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>());
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(3, 2));
        game.Fire(PlayerId.Player1, new Coordinate(3, 3));
        game.Fire(PlayerId.Player1, new Coordinate(3, 4));
        game.Print(PlayerId.Player1);

        // Arrange
        gameDisplayMock.Verify(x => x.DisplayTargetOcean(It.Is<List<Shoot>>(list => list.Count == 3)), Times.Once);
    }

    [Fact]
    public void should_announce_winner_when_player_sunk_all_opponent_ships()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        var ships = new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
        };
        game.AddPlayer(PlayerId.Player1, new List<Ship>()
        {

            ShipFactory.Build(new Coordinate(2, 7)),
        });
        game.AddPlayer(PlayerId.Player2, ships);
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(2, 7));
        game.EndTurn(PlayerId.Player1);

        // Arrange
        gameDisplayMock.Verify(x => x.DisplayGameWinner(PlayerId.Player1), Times.Once);
    }

    [Fact]
    public void should_print_battle_reports_when_game_is_finished()
    {
        // arrange
        var (gameDisplayMock, game) = NewGame();

        // act
        game.AddPlayer(PlayerId.Player1, new List<Ship>()
        {

            ShipFactory.Build(new Coordinate(2, 7)),
            ShipFactory.Build(new Coordinate(2, 7)),
        });
        game.AddPlayer(PlayerId.Player2, new List<Ship>()
        {
            ShipFactory.Build(new Coordinate(2, 7)),
        });
        game.StartGame(PlayerId.Player1);
        game.Fire(PlayerId.Player1, new Coordinate(2, 7));
        game.EndTurn(PlayerId.Player1);

        // Arrange
        gameDisplayMock.Verify(x => x.DisplayPlayerBattleReport(PlayerId.Player1,
            It.Is<List<Shoot>>(match => match.Count == 1),
            It.Is<List<Ship>>(match => match.Count == 1)), Times.Once);
        gameDisplayMock.Verify(x => x.DisplayPlayerBattleReport(PlayerId.Player2,
            It.Is<List<Shoot>>(match => match.Count == 0),
            It.Is<List<Ship>>(match => match.Count == 2)), Times.Once);
    }

    private static (Mock<IBattleshipGameDisplay> gameDisplayMock, BattleshipGame game) NewGame()
    {
        Mock<IBattleshipGameDisplay> gameDisplayMock = new Mock<IBattleshipGameDisplay>();
        BattleshipGame game = new BattleshipGame(gameDisplayMock.Object);
        return (gameDisplayMock, game);
    }

}