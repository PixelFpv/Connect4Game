namespace GameForm
{
    public partial class Form1 : Form
    {

        private const int Rows = 6;
        private const int Columns = 7;
        private int[,] board = new int[Rows, Columns]; // 0 for empty, 1 for player 1, 2 for player 2
        private int currentPlayer = 1; // Player 1 starts

        public Form1()
        {
            InitializeComponent();
            InitializeGameBoard();

        }

        private void InitializeGameBoard()
        {
            // Add buttons to the grid
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Button button = new Button
                    {
                        Width = 50,
                        Height = 50,
                        Top = row * 50,
                        Left = col * 50,
                        Tag = col // Store column index in Tag property
                    };
                    button.Click += DropPiece;
                    Controls.Add(button);
                }
            }

            // Add reset button
            Button resetButton = new Button
            {
                Text = "Reset Game",
                Top = Rows * 50 + 10,
                Left = 10
            };
            resetButton.Click += ResetGame;
            Controls.Add(resetButton);

            // Add player turn label
            Label turnLabel = new Label
            {
                Text = "Player 1's Turn",
                Top = Rows * 50 + 10,
                Left = resetButton.Right + 10,
                Name = "turnLabel"
            };
            Controls.Add(turnLabel);
        }



        private void DropPiece(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //Button newButton = (Button)sender;
            int col = (int)button.Tag;

            // Find the first available row in the selected column
            int row = GetAvailableRow(col);

            if (row != -1)
            {


                //if (currentPlayer == 1)
                //    button.BackColor = System.Drawing.Color.Red;
                //else if (currentPlayer == 2)
                //    button.BackColor = System.Drawing.Color.Yellow;


                int newTop = (Rows - 1 - row) * button.Height;

                //// Create a new button with the same properties and place it at the final position
                //Button newButton = new Button
                //{
                //    Width = button.Width,
                //    Height = button.Height,
                //    Top = newTop,
                //    Left = button.Left,
                //    Tag = col,
                //    //BackColor = button.BackColor // Preserve the original color
                //};

                //// Attach the event handler to the new button
                //newButton.Click += DropPiece;

                //// Add the new button to the form
                //Controls.Add(newButton);
                Button newButton = new Button
                {
                    Width = 50,
                    Height = 50,
                    Top = newTop,
                    Left = col * 50,
                    Tag = col, // Store column index in Tag property
                    BackColor = Color.Transparent
                };





                if (currentPlayer == 1)
                {
                    button.Top = row * button.Height;

                    button.BackColor = System.Drawing.Color.Red;
                }
                else if (currentPlayer == 2)
                {
                    button.Top = row * button.Height;

                    button.BackColor = System.Drawing.Color.Yellow;
                }
                board[row, col] = currentPlayer;

                newButton.Click += DropPiece;
                Controls.Add(newButton);

                if (CheckForWinner(row, col))
                {
                    MessageBox.Show($"Player {currentPlayer} wins!");
                    ResetGame(null, null);
                }
                else
                {
                    // Switch player turn
                    currentPlayer = (currentPlayer == 1) ? 2 : 1;
                    ((Label)Controls.Find("turnLabel", true)[0]).Text = $"Player {currentPlayer}'s Turn";
                }
            }
        }



        


        private int GetAvailableRow(int col)
        {
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (board[row, col] == 0)
                {
                    return row;
                }
            }
            return -1; // Column is full
        }

        private bool CheckForWinner(int row, int col)
        {
            // Check horizontally
            for (int c = Math.Max(0, col - 3); c <= Math.Min(Columns - 4, col); c++)
            {
                if (board[row, c] == currentPlayer &&
                    board[row, c + 1] == currentPlayer &&
                    board[row, c + 2] == currentPlayer &&
                    board[row, c + 3] == currentPlayer)
                {
                    return true;
                }
            }

            // Check vertically
            for (int r = Math.Max(0, row - 3); r <= Math.Min(Rows - 4, row); r++)
            {
                if (board[r, col] == currentPlayer &&
                    board[r + 1, col] == currentPlayer &&
                    board[r + 2, col] == currentPlayer &&
                    board[r + 3, col] == currentPlayer)
                {
                    return true;
                }
            }

            // Check diagonally (positive slope)
            for (int r = Math.Max(0, row - 3); r <= Math.Min(Rows - 4, row); r++)
            {
                for (int c = Math.Max(0, col - 3); c <= Math.Min(Columns - 4, col); c++)
                {
                    if (board[r, c] == currentPlayer &&
                        board[r + 1, c + 1] == currentPlayer &&
                        board[r + 2, c + 2] == currentPlayer &&
                        board[r + 3, c + 3] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            // Check diagonally (negative slope)
            for (int r = Math.Max(0, row - 3); r <= Math.Min(Rows - 4, row); r++)
            {
                for (int c = Math.Max(0, col - 3); c <= Math.Min(Columns - 4, col); c++)
                {
                    if (board[r, c + 3] == currentPlayer &&
                        board[r + 1, c + 2] == currentPlayer &&
                        board[r + 2, c + 1] == currentPlayer &&
                        board[r + 3, c] == currentPlayer)
                    {
                        return true;
                    }
                }
            }

            return false;
        }



        private void ResetGame(object sender, EventArgs e)
        {
            //InitializeGameBoard();

            // Clear the board
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    board[row, col] = 0;
                    Controls[row * Columns + col].BackColor = default;
                }
            }

            currentPlayer = 1;
            ((Label)Controls.Find("turnLabel", true)[0]).Text = "Player 1's Turn";
        }



        private void GameForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }



    }
}