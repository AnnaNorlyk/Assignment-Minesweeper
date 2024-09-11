using System;
using System.Linq;

namespace GameAssignment
{
    public class GameController : IGameController
    {
        private MineFieldElement[,] field;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public event EventHandler GameOver;
        public event EventHandler GameWon;

        public GameController(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            field = new MineFieldElement[rows, columns];
            SetupField();
        }

        // Initialize the minefield and place mines
        public void SetupField()
        {
            InitializeEmptyField();
            PlaceMines(); 
            CalculateAdjacentMines();
        }

        // Initialize the minefield with empty elements
        private void InitializeEmptyField()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    field[i, j] = new MineFieldElement(false); // Initialize without mines
                }
            }
        }


        private void PlaceMines()
        {
            int[,] minePositions = new int[,]
            {
                { 0, 1 },
                { 2, 3 },
                { 4, 5 },
                { 6, 7 },
                { 1, 8 },
                { 3, 9 },
                { 5, 0 },
                { 7, 2 },
                { 8, 6 },
                { 9, 4 }
            };

            // Loops through the mine positions and place the mines
            for (int k = 0; k < minePositions.GetLength(0); k++)
            {
                int i = minePositions[k, 0];
                int j = minePositions[k, 1];
                field[i, j].HasMine = true;
            }
        }


        // Calculate the number of adjacent mines for each cell
        private void CalculateAdjacentMines()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (!field[i, j].HasMine)
                    {
                        field[i, j].AdjacentMines = CountAdjacentMines(i, j);
                    }
                }
            }
        }

        // Count the number of mines surrounding a given cell
        private int CountAdjacentMines(int row, int col)
        {
            int mineCount = 0;
            for (int di = -1; di <= 1; di++)
            {
                for (int dj = -1; dj <= 1; dj++)
                {
                    if (IsWithinBounds(row + di, col + dj) && field[row + di, col + dj].HasMine)
                    {
                        mineCount++;
                    }
                }
            }
            return mineCount;
        }

        // Check if the given coordinates are within the minefield boundaries
        private bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < Rows && col >= 0 && col < Columns;
        }

        public void StartGame()
        {
            SetupField(); // Reset the field and start a new game
        }

        // Reveal a specific element and trigger game over if a mine is hit
        public void RevealElement(MineFieldElement element)
        {
            if (!element.IsRevealed)
            {
                element.Reveal();
                if (element.HasMine)
                {
                    GameOver?.Invoke(this, EventArgs.Empty);
                }
                else if (CheckWinCondition())
                {
                    GameWon?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        // Check if the game has been won by revealing all non-mine elements
        public bool CheckWinCondition()
        {
            return !field.Cast<MineFieldElement>().Any(e => !e.HasMine && !e.IsRevealed);
        }

        // Get a specific MineFieldElement based on its row and column
        public MineFieldElement GetMineFieldElement(int row, int col)
        {
            return IsWithinBounds(row, col) ? field[row, col] : null;
        }
    }
}
