using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssignment
{
    public interface IGameController
    {
        void SetupField();
        void StartGame();

        void RevealElement(MineFieldElement element);
        bool CheckWinCondition();
        MineFieldElement GetMineFieldElement(int row, int col);

        event EventHandler GameOver;
        event EventHandler GameWon;

        int Rows { get; }
        int Columns { get; }
    }

}
