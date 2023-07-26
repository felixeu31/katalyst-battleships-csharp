using Battleships.GameControls;
using Battleships.Generators;
using Battleships.Infrastructure;
using Battleships.Shoots;
using Moq;

namespace Battleships.Tests.Unit;

public class BattleShipGameDisplayTest
{
    [Fact]
    public void should_display_target_ocean()
    {
        // Arrange
        var displayMock = new Mock<IDisplay>();
        var oceanGridGeneratorMock = new Mock<IOceanGridGenerator>();
        var oceanGridGeneratorFactoryMock = new Mock<IOceanGridGeneratorFactory>(); 
        var battleshipGameDisplay = new BattleshipGameDisplay(displayMock.Object, oceanGridGeneratorFactoryMock.Object);
        var shoots = new List<Shoot>();
        oceanGridGeneratorMock.Setup(x => x.GetGrid()).Returns("Mocked target grid");
        oceanGridGeneratorFactoryMock.Setup(x => x.CreateTargetOceanGridGenerator(shoots, It.IsAny<int>(), It.IsAny<int>()))
            .Returns(oceanGridGeneratorMock.Object);

        // Act
        battleshipGameDisplay.DisplayTargetOcean(shoots);

        // Assert
        displayMock.Verify(x => x.WriteLine("- Target ocean grid:"), Times.Once);
        displayMock.Verify(d => d.WriteLine("Mocked target grid"), Times.Once);
        oceanGridGeneratorFactoryMock.Verify(f => f.CreateTargetOceanGridGenerator(shoots, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}