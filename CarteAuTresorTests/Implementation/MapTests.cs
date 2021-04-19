using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarteAuTresor.Tests
{
    [TestClass]
    public class MapTests
    {
        private Map TestMap { get; set; }


        [TestMethod]
        public void Map_ShouldProperlyInitialize()
        {
            Assert.AreEqual(10, TestMap.SizeX);
            Assert.AreEqual(10, TestMap.SizeY);
            foreach (var tile in TestMap.TileMap)
            {
                Assert.AreEqual(TileType.PLAIN, tile.tileType);
            }

        }

        [TestMethod]
        public void AddMountainToMap_ShouldAddMountainTileToMap()
        {
            Mountain mountain = new Mountain(5, 5);
            TestMap.AddMountainToMap(mountain);

            Assert.AreEqual(TileType.MOUNTAIN, TestMap.TileMap[5, 5].tileType);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddMountainToMap_ShouldThrowIfMoutainOutOfRange()
        {
            Mountain mountain = new Mountain(11, 11);
            TestMap.AddMountainToMap(mountain);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddMountainToMap_ShouldThrowIfMoutainAlreadyOnTile()
        {
            Mountain mountain = new Mountain(5, 5);
            TestMap.AddMountainToMap(mountain);
            TestMap.AddMountainToMap(mountain);
        }

        [TestMethod]
        public void AddTreasureToMap_ShouldAddTreasureTileToMap()
        {
            Treasure treasure = new Treasure(1, 5, 5);
            TestMap.AddTreasureToMap(treasure);
            Assert.AreEqual(TileType.TREASURE, TestMap.TileMap[5, 5].tileType);
        }

        [TestMethod]
        public void AddTreasureToMap_ShouldAddTreasureNumberToTileIfTreasureIsAlreadyPresent()
        {
            Treasure treasure = new Treasure(1, 5, 5);
            TestMap.AddTreasureToMap(treasure);
            TestMap.AddTreasureToMap(treasure);
            var result = (Treasure)TestMap.TileMap[5, 5];
            Assert.AreEqual(TileType.TREASURE, result.tileType);
            Assert.AreEqual(2, result.numberOfTreasure);
        }



        [TestMethod]
        public void CheckIffOutOfRange_ShouldReturnFalseIfInRange()
        {
            var result = TestMap.CheckIffOutOfRange(5, 5);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CheckIffOutOfRange_ShouldReturnTrueInNotRange()
        {
            var result = TestMap.CheckIffOutOfRange(11, 11);
            Assert.IsTrue(result);
        }

        [TestInitialize]
        public void InitializeMap()
        {
            TestMap = new Map(10, 10);
        }
    }
}