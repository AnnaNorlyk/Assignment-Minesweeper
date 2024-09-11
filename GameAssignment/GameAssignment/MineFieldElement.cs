using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssignment
{
    public class MineFieldElement : IMineFieldElement
    {
        public bool IsRevealed { get; set; }
        public int AdjacentMines { get; set; }
        public bool HasMine { get; set; }  
        public int Row { get; set; }       
        public int Col { get; set; }       

        public MineFieldElement(bool hasMine)
        {
            HasMine = hasMine;
            IsRevealed = false;
            AdjacentMines = 0;
        }

        public void Reveal()
        {
            IsRevealed = true;
        }
    }
}

