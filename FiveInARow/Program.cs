namespace FiveInARow
{
    public static class FiveInARow
    {
        public static void Main(string[] args)
        {
            //Currently the game works with only one option: player vs player,
            //but can be extended with other game modes later
            
            Console.WriteLine("Welcome to five in a row!");
            Console.WriteLine();
            Console.WriteLine("Press enter to continue.");

            //We are currently not using the gameMode variable 
            var gameMode = Console.ReadLine();

            //Since we are playing 'five in a row' table is set to a 5 by 5 size
            const int tableRows = 5;
            const int tableColumns = 5;
            
            //Game class will be created in the next step
            Game game = new Game(tableRows, tableColumns);
            
            Console.Clear();
            
            //The argument for the Play method sets the winning condition for how many marks
            //we need to win the game in a row or column or diagonally
            const int marksToWin = 5;
            
            game.Play(marksToWin);
        }
    }
}