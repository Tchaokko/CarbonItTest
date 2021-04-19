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
            adventurer.playerOrientation = "S";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 1);
            Assert.AreEqual(adventurer.posY, 2);
            Assert.AreEqual(false, tempTileMap[1, 1].gotAdventurer);
            Assert.AreEqual(true, tempTileMap[2, 1].gotAdventurer);


        }


        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldMoveNorthIfAdventurerOrientationIsNorth()
        {
            //Arrange
            adventurer.playerOrientation = "N";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 1);
            Assert.AreEqual(adventurer.posY, 0);
            Assert.AreEqual(false, tempTileMap[1, 1].gotAdventurer);
            Assert.AreEqual(true, tempTileMap[0, 1].gotAdventurer);
        }


        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldMoveWestIfAdventurerOrientationIsWest()
        {
            //Arrange
            adventurer.playerOrientation = "O";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 0);
            Assert.AreEqual(adventurer.posY, 1);
            Assert.AreEqual(false, tempTileMap[1, 1].gotAdventurer);
            Assert.AreEqual(true, tempTileMap[1, 0].gotAdventurer);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldMoveEastIfAdventurerOrientationIsEast()
        {
            //Arrange
            adventurer.playerOrientation = "E";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 2);
            Assert.AreEqual(adventurer.posY, 1);
            Assert.AreEqual(false, tempTileMap[1, 1].gotAdventurer);
            Assert.AreEqual(true, tempTileMap[1, 2].gotAdventurer);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldNotMoveIfTryingToMoveOutOfRange()
        {
            //Arrange
            adventurer.playerOrientation = "E";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 1);
            Assert.AreEqual(adventurer.posY, 1);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldNotMoveIfTryingToMoveOnMountainTile()
        {
            //Arrange
            adventurer.playerOrientation = "E";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            tempTileMap[1, 2] = new Mountain(2, 1);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 1);
            Assert.AreEqual(adventurer.posY, 1);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldNotMoveIfTryingToMoveOnTileWithAnotherPlayer()
        {
            //Arrange
            adventurer.playerOrientation = "E";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            tempTileMap[1, 2].gotAdventurer = true;
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 1);
            Assert.AreEqual(adventurer.posY, 1);
        }

        [TestMethod]
        public void movePlayerInFrontOfHim_ShouldGetOnlyOneTreasureIfTreasureMap()
        {
            //Arrange
            adventurer.playerOrientation = "E";
            adventurer.posX = 1;
            adventurer.posY = 1;
            var tempTileMap = initMap(5, 5);
            tempTileMap[1, 2] = new Treasure(3, 2, 1);
            mapMock.Setup(s => s.CheckIffOutOfRange(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            mapMock.SetupGet(s => s.TileMap).Returns(tempTileMap);


            //Act
            adventurer.movePlayerInFrontOfHim(mapMock.Object);

            //Assert
            Assert.AreEqual(adventurer.posX, 2);
            Assert.AreEqual(adventurer.posY, 1);
            Assert.AreEqual(adventurer.treasures, 1);
            Assert.AreEqual((tempTileMap[1, 2] as Treasure).numberOfTreasure, 2);
        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerWestIfOrientationIsSouthAndLookingRight()
        {
            //Arrange
            adventurer.playerOrientation = "S";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "O");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerEastIfOrientationIsSouthAndLookingLeft()
        {
            //Arrange
            adventurer.playerOrientation = "S";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "E");

        }


        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerEastIfOrientationIsNorthAndLookingRight()
        {
            //Arrange
            adventurer.playerOrientation = "N";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "E");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerEastIfOrientationIsNorthAndLookingLeft()
        {
            //Arrange
            adventurer.playerOrientation = "N";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "O");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerNorthIfOrientationIsEasthAndLookingLeft()
        {
            //Arrange
            adventurer.playerOrientation = "E";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "N");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerSouthIfOrientationIsEastAndLookingRight()
        {
            //Arrange
            adventurer.playerOrientation = "E";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "S");

        }


        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerNorthIfOrientationIsWestAndLookingRight()
        {
            //Arrange
            adventurer.playerOrientation = "O";


            //Act
            adventurer.ChangeOrientation(MovementDirection.RIGHT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "N");

        }

        [TestMethod]
        public void ChangeOrientation_ShouldOrientPlayerSouthIfOrientationIsWestAndLookingLeft()
        {
            //Arrange
            adventurer.playerOrientation = "O";


            //Act
            adventurer.ChangeOrientation(MovementDirection.LEFT);

            //Assert
            Assert.AreEqual(adventurer.playerOrientation, "S");

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