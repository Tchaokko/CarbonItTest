using System.Collections.Generic;
using CarteAuTresor.Interface;

namespace CarteAuTresor
{
    public class InstructionDto
    {
        public int mapSizeX { get; set; }
        public int mapSizeY { get; set; }
        public List<ITile> tiles { get; set; }
        public List<IAdventurer> adventurer { get; set; }

        public InstructionDto()
        {
            tiles = new List<ITile>();
            adventurer = new List<IAdventurer>();
        }
    }
}
