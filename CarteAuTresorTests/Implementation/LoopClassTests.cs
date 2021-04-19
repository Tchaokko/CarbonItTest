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
            Assert.AreEqual(true, map.TileMap[0, 0].GotAdventurer);
            Assert.AreEqual(true, map.TileMap[3, 3].GotAdventurer);
            Assert.AreEqual(TileType.TREASURE, map.TileMap[2, 2].tileType);
            Assert.AreEqual(TileType.MOUNTAIN, map.TileMap[1, 1].tileType);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void InitializeGame_ShouldThrowIfPlayerAlreadyOnTile()
        {

            instruct.adventurer[1].PosX = 0;
            instruct.adventurer[1].PosY = 0;

            //Act
            loopClassTest.InitializeGame(instruct);

        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void InitializeGame_ShouldThrowIMountainAlreadyOnTile()
        {

            instruct.adventurer[1].PosX = 1;
            instruct.adventurer[1].PosY = 1;

            //Act
            loopClassTest.InitializeGame(instruct);

        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void InitializeGame_ShouldThrowIfPlayerIsOutOfBound()
        {

            instruct.adventurer[1].PosX = 15;
            instruct.adventurer[1].PosY = 15;

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

            Assert.AreEqual(2, loopClassTest.AdventurerList[1].PosX);
            Assert.AreEqual(2, loopClassTest.AdventurerList[1].PosY);
            Assert.AreEqual(true, result.TileMap[2, 2].GotAdventurer);

            Assert.AreEqual(0, loopClassTest.AdventurerList[0].PosX);
            Assert.AreEqual(1, loopClassTest.AdventurerList[0].PosY);
            Assert.AreEqual(true, result.TileMap[1, 0].GotAdventurer);
        }

        [TestMethod]
        public void Loop_ComplexMoving()
        {
            //Arrange
            var advent = new Adventurer { PosX = 1, PosY = 1, MovementList = "AADADAGGA", Name = "Shia", FinishMoving = false, PlayerOrientation = "S", Treasures = 0 };
            instruct.adventurer = new List<IAdventurer> { advent };
            instruct.mapSizeX = 5;
            instruct.mapSizeY = 5;
            var mountain = new Mountain(1, 0) { GotAdventurer = false, tileType = TileType.MOUNTAIN };
            var mountain2 = new Mountain(2, 1) { GotAdventurer = false, tileType = TileType.MOUNTAIN };

            var treasure = new Treasure(2, 0, 3) { GotAdventurer = false, tileType = TileType.TREASURE };
            var treasure2 = new Treasure(3, 1, 3) { GotAdventurer = false, tileType = TileType.TREASURE };
            instruct.tiles = instruct.tiles = new List<ITile> { mountain, treasure, mountain2, treasure2 };

            map = new Map(3, 4);
            loopClassTest.InitializeGame(instruct);

            loopClassTest.Loop();

            Assert.AreEqual(0, loopClassTest.AdventurerList[0].PosX);
            Assert.AreEqual(3, loopClassTest.AdventurerList[0].PosY);
            Assert.AreEqual("S", loopClassTest.AdventurerList[0].PlayerOrientation);
            Assert.AreEqual(3, loopClassTest.AdventurerList[0].Treasures);
        }

        [TestMethod]
        public void Loop_PlayerShouldGetTreasure()
        {
            //Arrange
            loopClassTest.InitializeGame(instruct);

            //Act
            var result = loopClassTest.Loop();

            Assert.AreEqual(2, loopClassTest.AdventurerList[1].PosX);
            Assert.AreEqual(2, loopClassTest.AdventurerList[1].PosY);
            Assert.AreEqual(1, loopClassTest.AdventurerList[1].Treasures);
        }


        [TestInitialize]
        public void initializeTest()
        {
            instruct = new InstructionDto();

            var advent1 = new Adventurer { PosX = 0, PosY = 0, MovementList = "A", Name = "Indiana", FinishMoving = false, PlayerOrientation = "S", Treasures = 0 };
            var advent2 = new Adventurer { PosX = 3, PosY = 3, MovementList = "ADDGA", Name = "Shia", FinishMoving = false, PlayerOrientation = "O", Treasures = 0 };

            instruct.adventurer = new List<IAdventurer> { advent1, advent2 };
            instruct.mapSizeX = 5;
            instruct.mapSizeY = 5;
            var mountain = new Mountain(1, 1) { GotAdventurer = false, tileType = TileType.MOUNTAIN };
            var treasure = new Treasure(2, 2, 2) { GotAdventurer = false, tileType = TileType.TREASURE };

            instruct.tiles = new List<ITile> { mountain, treasure };
            map = new Map(5, 5);
            loopClassTest = new LoopClass(map);
            //Assert



        }
    }
}