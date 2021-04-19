using System.Collections.Generic;
using CarteAuTresor.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarteAuTresor.Implementation.Tests
{
    [TestClass()]
    public class FileWrapperTests
    {
        Map map;
        List<IAdventurer> adventurers;

        [TestMethod()]
        public void WriteMapToStringArray_ShouldProperlyFillStringArray()
        {

            FileWrapper fileWrapper = new FileWrapper();
            var result = fileWrapper.WriteMapToStringArray(map, adventurers);
            Assert.AreEqual("C - 5 - 5", result[0]);
            Assert.AreEqual("M - 1 - 1", result[1]);
            Assert.AreEqual("# {T comme Trésor} - {Axe horizontal} - {Axe vertical} - {Nb. de trésors restants}", result[2]);
            Assert.AreEqual("T - 2 - 2 - 2", result[3]);
            Assert.AreEqual("# {A comme Aventurier} - {Nom de l’aventurier} - {Axe horizontal} - {Axevertical} - {Orientation} - {Nb.trésors ramassés}", result[4]);
            Assert.AreEqual("A - Indiana - 0 - 0 - S - 0", result[5]);
            Assert.AreEqual("A - Shia - 1 - 3 - O - 0", result[6]);
        }

        [TestInitialize]
        public void initializeTest()
        {
            var advent1 = new Adventurer { posX = 0, posY = 0, movementList = "A", name = "Indiana", finishMoving = false, playerOrientation = "S", treasures = 0 };
            var advent2 = new Adventurer { posX = 1, posY = 3, movementList = "ADDGA", name = "Shia", finishMoving = false, playerOrientation = "O", treasures = 0 };
            adventurers = new List<IAdventurer> { advent1, advent2 };
            var mountain = new Mountain(1, 1) { gotAdventurer = false, tileType = TileType.MOUNTAIN };
            var treasure = new Treasure(2, 2, 2) { gotAdventurer = false, tileType = TileType.TREASURE };

            map = new Map(5, 5);
            map.AddMountainToMap(mountain);
            map.AddTreasureToMap(treasure);
            map.TileMap[0, 0].gotAdventurer = true;
            map.TileMap[1, 3].gotAdventurer = true;

        }
    }
}