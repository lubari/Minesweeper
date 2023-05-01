using Minesweeper.Core;
using Minesweeper.Core.Enums;

namespace MinesweeperTests
{
    [TestFixture]
    public class PositiveTests
    { 
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void CreateGameProcessorTest()
        {   //Arrange
            bool[,] boolField = { };
            var gameProcessor = new GameProcessor(boolField);
            //Act
            //Assert
            Assert.That(gameProcessor.GameState, Is.EqualTo(GameState.Active));
        }

        [TestCase(0,0)]
        [TestCase(0,0)]
        public void WhenOpenNotMine_OpenTest_GameStateShouldBeActive(int x, int y)
        {
            //Arrange
            bool[,] boolField = new bool[2, 2] { { false, true }, { true, false } };
            var gameProcessor = new GameProcessor(boolField);
            //Act
            gameProcessor.Open(x, y);
            //Equals
            Assert.That(gameProcessor.GameState, Is.EqualTo(GameState.Active));
        }

        [TestCase(1, 1)]
        [TestCase(0, 2)]
        public void WhenOpenMine_OpenTest_GameStateShouldBeLose(int x, int y)
        {
            //Arrange
            bool[,] boolField = new bool[3, 3]
            {
                { false, false, false },
                { false, true, false },
                { true, false, false }
            };
            var gameProcessor = new GameProcessor(boolField);
            //Act
            gameProcessor.Open(x, y);
            //Equals
            Assert.That(gameProcessor.GameState, Is.EqualTo(GameState.Lose));
        }

        [Test]
        public void WhenOpenMine_OpenTest_GameStateShouldBeWin()
        {   
            //Arrange
            bool[,] boolField = new bool[3, 3]
                {
                { false, true, false },
                { true, false, true },
                { false, true, false }
                };
            var gameProcessor = new GameProcessor(boolField);
            // Act
            gameProcessor.Open(0, 0);
            gameProcessor.Open(2, 0);
            gameProcessor.Open(0, 2);
            gameProcessor.Open(2, 2);
            gameProcessor.Open(1, 1);
            //Assert
            Assert.That(gameProcessor.GameState, Is.EqualTo(GameState.Win));
        }

        [Test]
        public void GetCurrentFieldTest()
        {
            //Arrange
            bool[,] boolField =
            {
            {false, false, false},
            {false, true, false},
            {true, false, false}
            };
            var gameProcessor = new GameProcessor(boolField);
            //Act
            gameProcessor.Open(1, 1);
            PointState[,] currentField = gameProcessor.GetCurrentField();
            // Assert
            Assert.AreEqual(PointState.Close, currentField[0, 0]);
            Assert.AreEqual(PointState.Close, currentField[0, 1]);
            Assert.AreEqual(PointState.Close, currentField[0, 2]);
            Assert.AreEqual(PointState.Close, currentField[1, 0]);
            Assert.AreEqual(PointState.Mine, currentField[1, 1]);
            Assert.AreEqual(PointState.Close, currentField[1, 2]);
            Assert.AreEqual(PointState.Close, currentField[2, 0]);
            Assert.AreEqual(PointState.Close, currentField[2, 1]);
            Assert.AreEqual(PointState.Close, currentField[2, 2]);
        }

    }
}