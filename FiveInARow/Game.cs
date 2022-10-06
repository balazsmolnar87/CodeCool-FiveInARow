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
}