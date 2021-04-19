using CarteAuTresor.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CarteAuTresor.Tests
{
    [TestClass]
    public class AdventurerTests
    {
        Mock<IMap> mapMock;
        Adventurer adventurer;

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldMoveSouthIfAdventurerOrientationIsSouth()
        {
            //Arrange
            adventurer.PlayerOrientation = "S";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 1);
            Assert.AreEqual(adventurer.PosY, 2);
            Assert.AreEqual(false, tempTileMap[1, 1].GotAdventurer);
            Assert.AreEqual(true, tempTileMap[2, 1].GotAdventurer);


        }


        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldMoveNorthIfAdventurerOrientationIsNorth()
        {
            //Arrange
            adventurer.PlayerOrientation = "N";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 1);
            Assert.AreEqual(adventurer.PosY, 0);
            Assert.AreEqual(false, tempTileMap[1, 1].GotAdventurer);
            Assert.AreEqual(true, tempTileMap[0, 1].GotAdventurer);
        }


        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldMoveWestIfAdventurerOrientationIsWest()
        {
            //Arrange
            adventurer.PlayerOrientation = "O";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 0);
            Assert.AreEqual(adventurer.PosY, 1);
            Assert.AreEqual(false, tempTileMap[1, 1].GotAdventurer);
            Assert.AreEqual(true, tempTileMap[1, 0].GotAdventurer);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldMoveEastIfAdventurerOrientationIsEast()
        {
            //Arrange
            adventurer.PlayerOrientation = "E";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 2);
            Assert.AreEqual(adventurer.PosY, 1);
            Assert.AreEqual(false, tempTileMap[1, 1].GotAdventurer);
            Assert.AreEqual(true, tempTileMap[1, 2].GotAdventurer);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldNotMoveIfTryingToMoveOutOfRange()
        {
            //Arrange
            adventurer.PlayerOrientation = "E";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 1);
            Assert.AreEqual(adventurer.PosY, 1);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldNotMoveIfTryingToMoveOnMountainTile()
        {
            //Arrange
            adventurer.PlayerOrientation = "E";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            tempTileMap[1, 2] = new Mountain(2, 1);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 1);
            Assert.AreEqual(adventurer.PosY, 1);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldNotMoveIfTryingToMoveOnTileWithAnotherPlayer()
        {
            //Arrange
            adventurer.PlayerOrientation = "E";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            tempTileMap[1, 2].GotAdventurer = true;
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 1);
            Assert.AreEqual(adventurer.PosY, 1);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldGetOnlyOneTreasureIfTreasureMap()
        {
            //Arrange
            adventurer.PlayerOrientation = "E";
            adventurer.PosX = 1;
            adventurer.PosY = 1;
            var tempTileMap = initMap(5, 5);
            tempTileMap[1, 2] = new Treasure(3, 2, 1);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.PosX, 2);
            Assert.AreEqual(adventurer.PosY, 1);
            Assert.AreEqual(adventurer.Treasures, 1);
            Assert.AreEqual((tempTileMap[1, 2] as Treasure).numberOfTreasure, 2);
        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerWestIfOrientationIsSouthAndLookingRight()
        {
            //Arrange
            adventurer.PlayerOrientation = "S";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "O");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerEastIfOrientationIsSouthAndLookingLeft()
        {
            //Arrange
            adventurer.PlayerOrientation = "S";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "E");

        }


        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerEastIfOrientationIsNorthAndLookingRight()
        {
            //Arrange
            adventurer.PlayerOrientation = "N";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "E");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerEastIfOrientationIsNorthAndLookingLeft()
        {
            //Arrange
            adventurer.PlayerOrientation = "N";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "O");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerNorthIfOrientationIsEasthAndLookingLeft()
        {
            //Arrange
            adventurer.PlayerOrientation = "E";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "N");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerSouthIfOrientationIsEastAndLookingRight()
        {
            //Arrange
            adventurer.PlayerOrientation = "E";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "S");

        }


        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerNorthIfOrientationIsWestAndLookingRight()
        {
            //Arrange
            adventurer.PlayerOrientation = "O";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "N");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerSouthIfOrientationIsWestAndLookingLeft()
        {
            //Arrange
            adventurer.PlayerOrientation = "O";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.PlayerOrientation, "S");

        }

        private ITile[,] initMap(int sizeX, int sizeY)
        {
            var TileMap = new ITile[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    TileMap[x, y] = new Plain(x, y);
                }
            }
            return TileMap;
        }

        [TestInitialize]
        public void InitPlayer()
        {
            mapMock = new Mock<IMap>();

            adventurer = new Adventurer();
        }
    }
}