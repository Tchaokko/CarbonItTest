using System;
using CarteAuTresor.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CarteAuTresor.Tests
{
    [TestClass()]
    public class InputHelperTests
    {
        InputHelper inputHelper;
        Mock<IFileWrapper> fileWrapperMock;

        [TestMethod]
        public void ReadFileAndPutIntoInstructionTest_ShouldReturnProperMapInstruction()
        {
            //Arrange
            string[] instructions = new string[] { "C - 3 - 4" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert
            Assert.AreEqual(3, instruction.mapSizeX);
            Assert.AreEqual(4, instruction.mapSizeY);

        }


        [TestMethod]
        public void ReadFileAndPutIntoInstructionTest_ShouldCreateMoutains()
        {
            //Arrange
            string[] instructions = new string[] { "M - 1 - 1", "M - 1 - 2" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert
            Assert.AreEqual(2, instruction.tiles.Count);
            Assert.AreEqual(TileType.MOUNTAIN, instruction.tiles[0].tileType);
            Assert.AreEqual(1, instruction.tiles[0].PosX);
            Assert.AreEqual(1, instruction.tiles[0].PosY);

            Assert.AreEqual(TileType.MOUNTAIN, instruction.tiles[1].tileType);
            Assert.AreEqual(1, instruction.tiles[1].PosX);
            Assert.AreEqual(2, instruction.tiles[1].PosY);

        }

        [TestMethod]
        public void ReadFileAndPutIntoInstructionTest_ShouldCreateTreasures()
        {
            //Arrange
            string[] instructions = new string[] { "T - 1 - 1 - 1", "T - 1 - 2 - 3" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert
            Assert.AreEqual(2, instruction.tiles.Count);
            Assert.AreEqual(TileType.TREASURE, instruction.tiles[0].tileType);
            Assert.AreEqual(1, instruction.tiles[0].PosX);
            Assert.AreEqual(1, instruction.tiles[0].PosY);
            Assert.AreEqual(1, (instruction.tiles[0] as Treasure).numberOfTreasure);

            Assert.AreEqual(TileType.TREASURE, instruction.tiles[1].tileType);
            Assert.AreEqual(1, instruction.tiles[1].PosX);
            Assert.AreEqual(2, instruction.tiles[1].PosY);
            Assert.AreEqual(3, (instruction.tiles[1] as Treasure).numberOfTreasure);
        }

        [TestMethod]
        public void ReadFileAndPutIntoInstructionTest_ShouldCreateAdventurer()
        {
            //Arrange
            string[] instructions = new string[] { "A - Indiana - 1 - 1 - S - AADADA" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert
            Assert.AreEqual(1, instruction.adventurer.Count);
            Assert.AreEqual("Indiana", instruction.adventurer[0].Name);
            Assert.AreEqual(1, instruction.adventurer[0].PosX);
            Assert.AreEqual(1, instruction.adventurer[0].PosY);
            Assert.AreEqual("AADADA", instruction.adventurer[0].MovementList);
            Assert.AreEqual("S", instruction.adventurer[0].PlayerOrientation);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadFileAndPutIntoInstructionTest_ShouldThrowIfWrongCategoryInput()
        {
            //Arrange
            string[] instructions = new string[] { "D - Indiana - 1 - 1 - S - AADADA" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadFileAndPutIntoInstructionTest_ShouldThrowIfPlayerWrongLineFormat()
        {
            //Arrange
            string[] instructions = new string[] { "A - Indiana - 1 - A - S - AADADA" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadFileAndPutIntoInstructionTest_ShouldThrowIfSimpleParsingWrongLineFormat()
        {
            //Arrange
            string[] instructions = new string[] { "C - - 3 - A" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadFileAndPutIntoInstructionTest_ShouldThrowIfTreasureWrongLineFormat()
        {
            //Arrange
            string[] instructions = new string[] { "T -  3 - 2" };
            fileWrapperMock.Setup(s => s.ReadAllLines(It.IsAny<string>())).Returns(instructions);

            inputHelper = new InputHelper(fileWrapperMock.Object);

            //Act
            var instruction = inputHelper.ReadFileAndPutIntoInstruction("");

            //Assert
        }

        [TestInitialize]
        public void InitInputHelper()
        {
            string[] instructions = new string[] { "C - 3 - 4", "M - 1 - 1", "T - 0 - 3 - 2", "A - Indiana - 1 - 1 - S - AADADA" };

            fileWrapperMock = new Mock<IFileWrapper>();
        }
    }
}