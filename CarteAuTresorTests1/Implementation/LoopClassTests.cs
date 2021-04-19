using System;
using System.Collections.Generic;
using CarteAuTresor.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarteAuTresor.Tests
{
    [TestClass()]
    public class LoopClassTests
    {
        LoopClass loopClassTest;
        InstructionDto instruct;
        Map map;

        [TestMethod]
        public void InitializeGame_ShouldProperlyInitializeMap()
        {

            //Arrange

            //Act
            loopClassTest.InitializeGame(instruct);


            //Assert
            Assert.AreEqual(true, map.TileMap[0, 0].gotAdventurer);
            Assert.AreEqual(true, map.TileMap[3, 3].gotAdventurer);
            Assert.AreEqual(TileType.TREASURE, map.TileMap[2, 2].tileType);
            Assert.AreEqual(TileType.MOUNTAIN, map.TileMap[1, 1].tileType);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void InitializeGame_ShouldThrowIfPlayerAlreadyOnTile()
        {

            instruct.adventurer[1].posX = 0;
            instruct.adventurer[1].posY = 0;

            //Act
            loopClassTest.InitializeGame(instruct);

        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void InitializeGame_ShouldThrowIMountainAlreadyOnTile()
        {

            instruct.adventurer[1].posX = 1;
            instruct.adventurer[1].posY = 1;

            //Act
            loopClassTest.InitializeGame(instruct);

        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void InitializeGame_ShouldThrowIfPlayerIsOutOfBound()
        {

            instruct.adventurer[1].posX = 15;
            instruct.adventurer[1].posY = 15;

            //Act
            loopClassTest.InitializeGame(instruct);

        }

        [TestMethod]
        public void Loop_PlayerShouldProperlyMove()
        {
            //Arrange
            loopClassTest.InitializeGame(instruct);

            //Act
            var result = loopClassTest.Loop();

            Assert.AreEqual(2, loopClassTest.AdventurerList[1].posX);
            Assert.AreEqual(2, loopClassTest.AdventurerList[1].posY);
            Assert.AreEqual(true, result.TileMap[2, 2].gotAdventurer);

            Assert.AreEqual(0, loopClassTest.AdventurerList[0].posX);
            Assert.AreEqual(1, loopClassTest.AdventurerList[0].posY);
            Assert.AreEqual(true, result.TileMap[1, 0].gotAdventurer);
        }

        [TestMethod]
        public void Loop_ComplexMoving()
        {
            //Arrange
            var advent = new Adventurer { posX = 1, posY = 1, movementList = "AADADAGGA", name = "Shia", finishMoving = false, playerOrientation = "S", treasures = 0 };
            instruct.adventurer = new List<IAdventurer> { advent };
            instruct.mapSizeX = 5;
            instruct.mapSizeY = 5;
            var mountain = new Mountain(1, 0) { gotAdventurer = false, tileType = TileType.MOUNTAIN };
            var mountain2 = new Mountain(2, 1) { gotAdventurer = false, tileType = TileType.MOUNTAIN };

            var treasure = new Treasure(2, 0, 3) { gotAdventurer = false, tileType = TileType.TREASURE };
            var treasure2 = new Treasure(3, 1, 3) { gotAdventurer = false, tileType = TileType.TREASURE };
            instruct.tiles = instruct.tiles = new List<ITile> { mountain, treasure, mountain2, treasure2 };

            map = new Map(3, 4);
            loopClassTest.InitializeGame(instruct);

            loopClassTest.Loop();

            Assert.AreEqual(0, loopClassTest.AdventurerList[0].posX);
            Assert.AreEqual(3, loopClassTest.AdventurerList[0].posY);
            Assert.AreEqual("S", loopClassTest.AdventurerList[0].playerOrientation);
            Assert.AreEqual(3, loopClassTest.AdventurerList[0].treasures);
        }

        [TestMethod]
        public void Loop_PlayerShouldGetTreasure()
        {
            //Arrange
            loopClassTest.InitializeGame(instruct);

            //Act
            var result = loopClassTest.Loop();

            Assert.AreEqual(2, loopClassTest.AdventurerList[1].posX);
            Assert.AreEqual(2, loopClassTest.AdventurerList[1].posY);
            Assert.AreEqual(1, loopClassTest.AdventurerList[1].treasures);
        }


        [TestInitialize]
        public void initializeTest()
        {
            instruct = new InstructionDto();

            var advent1 = new Adventurer { posX = 0, posY = 0, movementList = "A", name = "Indiana", finishMoving = false, playerOrientation = "S", treasures = 0 };
            var advent2 = new Adventurer { posX = 3, posY = 3, movementList = "ADDGA", name = "Shia", finishMoving = false, playerOrientation = "O", treasures = 0 };

            instruct.adventurer = new List<IAdventurer> { advent1, advent2 };
            instruct.mapSizeX = 5;
            instruct.mapSizeY = 5;
            var mountain = new Mountain(1, 1) { gotAdventurer = false, tileType = TileType.MOUNTAIN };
            var treasure = new Treasure(2, 2, 2) { gotAdventurer = false, tileType = TileType.TREASURE };

            instruct.tiles = new List<ITile> { mountain, treasure };
            map = new Map(5, 5);
            loopClassTest = new LoopClass(map);
            //Assert



        }
    }
}