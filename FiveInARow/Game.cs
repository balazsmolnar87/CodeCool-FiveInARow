namespace FiveInARow;

public class Game
{
    private int[,] Board { get; }
    private int _rows;
    private int _cols;
    private const string AbcAscii = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string RowLetters = "ABCDE";

    public Game(int nRows, int nCols)
    {
        _rows = nRows;
        _cols = nCols;
        Board = new int[nRows, nCols];

        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < nCols; j++)
            {
                Board[i, j] = 0;
            }
        }
    }
    
    public void Play(int marksToWin)
    {
        int player = -1;
        bool hasWon = false;
        bool isFull = false;

        while (hasWon == false && isFull == false)
        {
            player = GetNextPlayer(player);

            PrintTopMessage();
            PrintBoard();

            var move = GetMove(player);

            int row = move.Item1;
            int col = move.Item2;

            Console.Clear();

            Mark(player, row, col);

            hasWon = HasWon(player, marksToWin);
            isFull = IsFull();

            if (hasWon)
            {
                PrintBoard();
                PrintResult(player);
            }

            else if (isFull)
            {
                PrintBoard();
                PrintResult();
            }
        }
    }

    private static int GetNextPlayer(int player)
    {
        return player.Equals(1) ? 2 : 1;
    }

    private static void PrintTopMessage()
    {
        Console.WriteLine("Welcome to five in a row!");
        Console.WriteLine("Player 1: X");
        Console.WriteLine("Player 2: O");
        Console.WriteLine();
    }

    private void PrintBoard()
    {
        Console.WriteLine("  1 2 3 4 5");

        for (var i = 0; i < Board.GetLength(0); i++)
        {
            var rowString = AbcAscii[i] + " ";
            for (var j = 0; j < Board.GetLength(1); j++)
            {
                rowString += Board[i, j] switch
                {
                    0 => "+ ",
                    1 => "X ",
                    _ => "O "
                };
            }
            Console.WriteLine(rowString);
        }
        Console.WriteLine();
    }

    private (int, int) GetMove(int player)
    {
        bool isMoveValid = false;

        int row = 0;
        int col = 0;
        
        const int minColValue = 1;
        const int minRowValue = 5;

        while (isMoveValid is false)
        {
            Console.WriteLine("It is player " + player + "'s turn.");
            Console.WriteLine("Select a coordinate to place your mark.");

            string move = Console.ReadLine().ToUpper();

            if (move.Length == 0 || move.Length == 1)
            {
                Console.WriteLine("A coordinate must be a row letter and a column number!");
                continue;
            }

            var rowChar = move[0];
            col = int.Parse(move.Substring(1));

            // Lets the user quit the game
            if (move == "QUIT")
            {
                Console.Clear();
                Console.WriteLine("Goodbye!");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
            
            // Check if move is valid
            else if (move.Length != 2)
            {
                Console.WriteLine("A coordinate must be a row letter and a column number!");
                continue;
            }

            else if (RowLetters.Contains(rowChar) is false || col is < minColValue or > minRowValue)
            {
                Console.WriteLine("Please enter coordinate in a correct format.");
                continue;
            }

            row = AbcAscii.IndexOf(move[0]);

            if (Board[row, col-1] == 1 || Board[row, col-1] == 2)
            {
                Console.WriteLine("That tile is taken, choose another one!");
                continue;
            }

            isMoveValid = true;
        }

        return (row, col-1);
    }

    private void Mark(int player, int row, int col)
    {
        if (player == 1)
        {
            Board[row, col] = 1;
        }
        else
        {
            Board[row, col] = 2;
        }
    }

    private bool HasWon(int player, int howMany)
    {
        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
            {
                if (Board[i, j] != player) continue;
                bool hasRight = CheckRight(i, j, howMany, player);
                if (hasRight)
                {
                    return true;
                }
                bool hasDown = CheckDown(i, j, howMany, player);
                if (hasDown)
                {
                    return true;
                }
                bool hasDownLeft = CheckDownLeft(i, j, howMany, player);
                if (hasDownLeft)
                {
                    return true;
                }
                bool hasDownRight = CheckDownRight(i, j, howMany, player);
                if (hasDownRight)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckRight(int i, int j, int howMany, int player)
    {
        int counter = 0;

        try
        {
            for (int k = 0; k < howMany; k++)
            {
                if (Board[i, j + k] == player)
                {
                    counter++;
                }
            }

            return counter == howMany;
        }
        catch (IndexOutOfRangeException e)
        {
            return false;
        }
    }

    private bool CheckDown(int i, int j, int howMany, int player)
    {
        int counter = 0;

        try
        {
            for (int k = 0; k < howMany; k++)
            {
                if (Board[i + k, j] == player)
                {
                    counter++;
                }
            }

            return counter == howMany;
        }
        catch (IndexOutOfRangeException e)
        {
            return false;
        }
    }

    private bool CheckDownLeft(int i, int j, int howMany, int player)
    {
        int counter = 0;

        try
        {
            for (int k = 0; k < howMany; k++)
            {
                if (Board[i + k, j - k] == player)
                {
                    counter++;
                }
            }

            return counter == howMany;
        }
        catch (IndexOutOfRangeException e)
        {
            return false;
        }
    }

    private bool CheckDownRight(int i, int j, int howMany, int player)
    {
        int counter = 0;

        try
        {
            for (int k = 0; k < howMany; k++)
            {
                if (Board[i + k, j + k] == player)
                {
                    counter++;
                }
            }

            return counter == howMany;
        }
        catch (IndexOutOfRangeException e)
        {
            return false;
        }
    }

    private bool IsFull()
    {
        for (int i = 0; i < Board.GetLength(0); i++)
        {
            for (int j = 0; j < Board.GetLength(1); j++)
            {
                if (Board[i, j] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }
}