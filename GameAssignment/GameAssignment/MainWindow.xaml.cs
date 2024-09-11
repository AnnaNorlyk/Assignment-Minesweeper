using System;
using System.Windows;
using System.Windows.Controls;

namespace GameAssignment
{
    public partial class MainWindow : Window
    {
        private GameController gameController;
        private Button[,] buttons;
        private readonly int rows = 10;
        private readonly int columns = 10;
        private TimeManager timeManager;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        // Initialize the game state and the timer
        private void InitializeGame()
        {
            gameController = new GameController(rows, columns);
            timeManager = new TimeManager();
            timeManager.TimeUpdated += TimeManager_TimeUpdated;
            InitializeGameBoard();
            timeManager.StartTimer();
        }

        // Event handler for timer updates, ensuring the UI is updated from a non-UI thread
        private void TimeManager_TimeUpdated(object sender, TimeSpan elapsedTime)
        {
            Dispatcher.Invoke(() =>
            {
                int roundedSeconds = (int)Math.Round(elapsedTime.TotalSeconds);
                TimerTextBlock.Text = $"Time: {roundedSeconds} seconds";
            });
        }

        // Initialize the game board with buttons
        private void InitializeGameBoard()
        {
            ResetGameGrid();
            buttons = new Button[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    CreateButton(i, j);
                }
            }
        }

        // Reset the grid for the game board
        private void ResetGameGrid()
        {
            gameGrid.Children.Clear();
        }

        // Create a button for each grid cell
        private void CreateButton(int row, int col)
        {
            var mineFieldElement = gameController.GetMineFieldElement(row, col);
            var button = new Button
            {
                Tag = mineFieldElement,
                Margin = new Thickness(5), 
                MinWidth = 40,  
                MinHeight = 40  
            };
            button.Click += ButtonClick;
            buttons[row, col] = button;
            gameGrid.Children.Add(button);
        }



        // Button click event handler
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var element = button.Tag as MineFieldElement;
                if (element != null && !element.IsRevealed)
                {
                    HandleButtonClick(button, element);
                }
            }
        }

        // Handle the game logic after a button click
        private void HandleButtonClick(Button button, MineFieldElement element)
        {
            element.Reveal();

            if (element.HasMine)
            {
                EndGame("Game Over!", element, button);
            }
            else
            {
                DisplayElementInfo(button, element);
                if (gameController.CheckWinCondition())
                {
                    EndGame("You Win!", element, button);
                }
            }
        }

        // Display the adjacent mine count or an empty string if none
        private void DisplayElementInfo(Button button, MineFieldElement element)
        {
            button.Content = element.AdjacentMines > 0 ? element.AdjacentMines.ToString() : "";
            button.IsEnabled = false;
        }

        // End the game, show the message, and stop the timer
        private void EndGame(string message, MineFieldElement element, Button button)
        {
            if (element.HasMine)
            {
                button.Content = "💣";  // Display bomb symbol
            }
            timeManager.StopTimer();
            int roundedSeconds = (int)Math.Round(timeManager.ElapsedTime.TotalSeconds);
            MessageBox.Show($"{message} Your score: {roundedSeconds}");

        }



        // Reset button click event handler to restart the game
        private void ResetButtonClick(object sender, RoutedEventArgs e)
        {
            
            gameController = new GameController(rows, columns);
            InitializeGameBoard();

      
            timeManager.ResetTimer();
            timeManager.StartTimer();

            TimerTextBlock.Text = "Time: 0 seconds";
        }

    }
}
