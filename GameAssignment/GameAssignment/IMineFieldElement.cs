using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssignment
{
    public interface IMineFieldElement
    {

    //Should probably have been made an enum
        bool IsRevealed { get; set; }
        int AdjacentMines { get; set; }
        bool HasMine { get; set; }

        int Row { get; set; }
        int Col { get; set; }

        void Reveal();
    }
}
