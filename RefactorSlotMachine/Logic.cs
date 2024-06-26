﻿
namespace RefactorSlotMachine
{
 
    /// <summary>
    /// The Logic file for RefactorSlotMachine.cs
    /// </summary>
    public static class Logic
    {
        static readonly Random rng = new();

        /// <summary>
        /// Randomize the slot machine roll
        /// </summary>
        /// <returns>Randomized 3 x 3 array for slot machine roll</returns>
        public static int[,] RandomizeSlotMachineRoll()
        {
            int[,] slotMachine = new int[Constants.LINE_LENGTH, Constants.LINE_LENGTH];
            for (int i = 0; i < Constants.LINE_LENGTH; i++)
            {
                for (int j = 0; j < Constants.LINE_LENGTH; j++)
                {
                    slotMachine[i, j] = rng.Next(Constants.RAND_NUM_MAX);
                }
            }
            return slotMachine;
        }

        /// <summary>
        /// Deduct wager from playerMoney
        /// </summary>
        /// <param name="userInputWager">The amount of money the player wagers</param>
        /// <param name="playerMoney">The amount of money the player has</param>
        /// <returns>The amount of player money after paying the wager</returns>
        public static decimal CalculateRemainingPlayerMoney(decimal userInputWager, decimal playerMoney) 
        {
            return playerMoney - Math.Round(userInputWager, 2);
        }

        /// <summary>
        /// Get the number of lines won.
        /// </summary>
        /// <param name="slotMachine">3 x 3 array for slot machine roll</param>
        /// <returns>The number of lines player won</returns>
        public static int GetNumberWins(int[,] slotMachine)
        {
            int slotLineNumber = 0;
            int[] aSingleLine = [0, 0, 0];
            int wins = 0;

            while (slotLineNumber < Constants.NUM_OF_CHECKS)
            {
                bool win = false;

                if (slotLineNumber <= Constants.LINE_3)
                {
                    aSingleLine = GetHorizontalLine(slotMachine, slotLineNumber, aSingleLine);
                }

                if (slotLineNumber > Constants.LINE_3 && slotLineNumber <= Constants.LINE_6)
                {
                    aSingleLine = GetVerticalLine(slotMachine, slotLineNumber, aSingleLine);
                }

                if (slotLineNumber > Constants.LINE_6)
                {
                    aSingleLine = GetDiagonalLine(slotMachine, slotLineNumber, aSingleLine);
                }

                win = Logic.CheckLine(aSingleLine, win);

                if (win)
                {
                    wins++;
                }
                slotLineNumber++;
            }
            return wins;
        }

        /// <summary>
        /// Get a horizontal line from slot machine roll
        /// </summary>
        /// <param name="slotMachine">3 x 3 array for slot machine roll</param>
        /// <param name="slotLineNumber">Number representing a specific line in slot machine roll</param>
        /// <param name="aSingleLine">1 x 3 array for a single line in slot machine roll</param>
        /// <returns>A single horizontal line in slot machine roll</returns>
        public static int[] GetHorizontalLine(int[,] slotMachine, int slotLineNumber, int[] aSingleLine)
        {
            for (int i = 0; i < Constants.LINE_LENGTH; i++)
            {
                aSingleLine[i] = slotMachine[slotLineNumber, i];
            }
            return aSingleLine;
        }

        /// <summary>
        /// Get a vertical line from slot machine roll
        /// </summary>
        /// <param name="slotMachine">3 x 3 array for slot machine roll</param>
        /// <param name="slotLineNumber">Number representing a specific line in slot machine roll</param>
        /// <param name="aSingleLine">1 x 3 array for a single line in slot machine roll</param>
        /// <returns>A single vertical line in slot machine roll</returns>
        public static int[] GetVerticalLine(int[,] slotMachine, int slotLineNumber, int[] aSingleLine)
        {
            for (int i = 0; i < Constants.LINE_LENGTH; i++)
            {
                aSingleLine[i] = slotMachine[i, slotLineNumber % Constants.LINE_LENGTH];
            }
            return aSingleLine;
        }

        /// <summary>
        /// Get a diagonal line from slot machine roll
        /// </summary>
        /// <param name="slotMachine">3 x 3 array for slot machine roll</param>
        /// <param name="slotlineNumber">Number representing a specific line in slot machine roll</param>
        /// <param name="aSingleLine">1 x 3 array for a single line in slot machine roll</param>
        /// <returns>A single diagonal line in slot machine roll</returns>
        public static int[] GetDiagonalLine(int[,] slotMachine, int slotlineNumber, int[] aSingleLine)
        {
            int j = Constants.LINE_LENGTH;
            for (int i = 0; i < Constants.LINE_LENGTH; i++)
            {
                if (slotlineNumber == Constants.LINE_7)
                {
                    aSingleLine[i] = slotMachine[i, i];
                }

                if (slotlineNumber == Constants.LINE_8)
                {
                    j--;
                    aSingleLine[i] = slotMachine[i, j];
                }
            }
            return aSingleLine;
        }

        /// <summary>
        /// Check to see if line is a win
        /// </summary>
        /// <param name="aSingleLine">1 x 3 array for a single line in slot machine roll</param>
        /// <param name="win">Boolean to denote a line win</param>
        /// <returns>If line was a winner or not</returns>
        public static bool CheckLine(int[] aSingleLine, bool win)
        {
            int lineChecker = 0;
            for (int i = 0; i < Constants.LINE_LENGTH; i++)
            {
                if (aSingleLine[0] == aSingleLine[i])
                {
                    lineChecker++;
                }
            }
            if (lineChecker == Constants.LINE_LENGTH)
            {
                win = true;
            }
            return win;
        }

        /// <summary>
        /// Calculate the amount of money player won
        /// </summary>
        /// <param name="wins">The number of lines player won</param>
        /// <param name="userInputWager">The player's wager</param>
        /// <returns>Total amount of money player won</returns>
        public static decimal CalculateWinEarnings(int wins, decimal userInputWager)
        {
            decimal userPotentialEarnings = Constants.EARNINGS_DIFFERENTIAL * userInputWager + Constants.BONUS_ADDITION;
            return wins * userPotentialEarnings;
        }

        /// <summary>
        /// Add the total amount of money player won to the amount of money the player has
        /// </summary>
        /// <param name="playerMoney">The amount of money the player has</param>
        /// <param name="playerWinAmount">The total amount of money player won</param>
        /// <returns>The amount of money the player has</returns>
        public static decimal CalculateNewPlayerMoney(decimal playerMoney, decimal playerWinAmount)
        {
            return playerMoney + playerWinAmount;
        }
    }
}
