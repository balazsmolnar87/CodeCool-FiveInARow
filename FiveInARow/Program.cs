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

            //Game class will be created in the next step
            Game game = new Game(5, 5);
            
            Console.Clear();
            
            game.Play(5);
        }
    }
}