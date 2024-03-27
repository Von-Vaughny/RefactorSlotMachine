
namespace RefactorSlotMachine
{
    /// <summary>
    /// The UIMethods.cs for RefactorSlotMachine.cs
    /// </summary>
    public static class UIMethods
    {

        /// <summary>
        /// Display the welcome message
        /// </summary>
        public static void DisplayWelcomeMessage() 
        {
            Console.WriteLine("****************************");
            Console.WriteLine("* Welcome to Slot Machine! *");
            Console.WriteLine("****************************");
        }

        /// <summary>
        /// Validate user inputted wager.
        /// </summary>
        /// <param name="playerMoney">The amount of money the player has</param>
        /// <returns>User inputted wager</returns>
        public static decimal ValidateUserInput(decimal playerMoney)
        {
            decimal userInputWager = 0.00M;
            while (userInputWager < Constants.MIN_WAGER || userInputWager > playerMoney)
            {
                Console.WriteLine($"\nPlayer money: ${playerMoney:F2}");
                Console.Write($"How much is your wager (cannot exceed amount of player money): $");

                string userInput = Console.ReadLine();
                if (!decimal.TryParse(userInput, out userInputWager))
                {
                    Console.WriteLine($"Error: '{userInput}' is not a numeric amount between ${Constants.MIN_WAGER} and ${playerMoney}.");
                }                
                
                if (decimal.TryParse(userInput, out decimal userAmount))
                {
                    if (userAmount < Constants.MIN_WAGER || userAmount > playerMoney)
                    {
                        Console.WriteLine($"Error: ${userAmount} is not between ${Constants.MIN_WAGER} and ${playerMoney}.");
                    }

                    if (userAmount > Constants.MIN_WAGER && userAmount <= playerMoney) 
                    {
                        userInputWager = userAmount;
                    }
                }
            }
            return userInputWager;
        }

        /// <summary>
        /// Display the slot machine roll
        /// </summary>
        /// <param name="slotMachine">3 x 3 array for slot machine roll</param>
        public static void DisplaySlotMachine(int[,] slotMachine)  
        {
            Console.WriteLine("\nSlot Machine Roll");
            for (int i = 0; i < Constants.LINE_LENGTH; i++)
            {
                for (int j = 0; j < Constants.LINE_LENGTH; j++)
                {
                    Console.Write($"{slotMachine[i, j]}  ");
                    if (j == Constants.LINE_LENGTH - 1)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        /// <summary>
        /// Display each line on the slot machine roll
        /// </summary>
        /// <param name="aSingleLine">1 x 3 array for a single line in slot machine roll</param>
        /// <param name="lineNumber">number to keep track of lines in slot machine array</param>
        /// <param name="win">boolean to denote a line win</param>
        public static void DisplayIndividualLine(int[] aSingleLine, int lineNumber, bool win) 
        {
            if (lineNumber == 1) 
            {
                Console.Write("\n");
                Console.WriteLine("Individual Line Results");
            }
            Console.Write($"Line {lineNumber}: ");
            for (int i = 0; i < Constants.LINE_LENGTH; i++)
            { 
                Console.Write($"{aSingleLine[i]} ");
            }
            Console.Write($"{win}\n");
        }

        /// <summary>
        /// Display the results of the game
        /// </summary>
        /// <param name="playerWinAmount">The total amount of money player won</param>
        public static void DisplayGameResults(decimal playerWinAmount)
        {
            string game_results = (playerWinAmount > 0) ? "WON" : "LOST";
            Console.WriteLine($"\nPlayer {game_results}");
            Console.WriteLine($"Won: ${playerWinAmount:F2}");
        }

        /// <summary>
        /// Display screen when player has run out of money
        /// </summary>
        /// <param name="playerMoney">The amount of money the player has</param>
        /// <returns></returns>
        public static void DisplayGameResetMessage() 
        { 
            Console.WriteLine($"Player has lost all money. Resetting game...\n");
        }
    }
}
