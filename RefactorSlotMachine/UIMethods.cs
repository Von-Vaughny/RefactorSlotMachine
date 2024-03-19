﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace RefactorSlotMachine
{
    public static class UIMethods
    {

        public static void DisplayWelcomeMessage() 
        {
            Console.WriteLine("****************************");
            Console.WriteLine("* Welcome to Slot Machine! *");
            Console.WriteLine("****************************");
        }

        public static void DisplayWagerQuestion(decimal playerMoney) 
        {
            Console.WriteLine($"\nPlayer money: ${playerMoney:F2}");
            Console.Write($"How much is your wager (cannot exceed amount of player money): ");
        }

        public static void DisplaySlotMachine(int[,] array, int lineLength) 
        {
            Console.WriteLine("\nSlot Machine Roll");
            for (int i = 0; i < lineLength; i++)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    Console.Write($"{array[i, j]}  ");
                    if (j == lineLength - 1)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }

        public static decimal ReturnUserInput() 
        {
            decimal userInputWager = 0.00M;

            if (decimal.TryParse(Console.ReadLine(), out decimal userAmount))
            {
                userInputWager = userAmount;
            }
            return userInputWager;
        }

        public static void DisplayIndividualLine(int[] aSingleLine, int lineNumber, int lineLength, bool win) 
        {
            if (lineNumber == 1) 
            {
                Console.Write("\n");
                Console.WriteLine("Individual Line Results");
            }
            Console.Write($"Line {lineNumber}: ");
            for (int i = 0; i < lineLength; i++)
            { 
                Console.Write($"{aSingleLine[i]} ");
            }
            Console.Write($"{win}\n");
        }

        public static void DisplayGameResults(string game_results, decimal playerWinAmount)
        {
            Console.WriteLine($"\nPlayer {game_results}");
            Console.WriteLine($"Won: ${playerWinAmount:F2}");
        }

        public static decimal ResetPlayerMoney(decimal playerMoney, decimal PLAYER_RESET_MONEY) 
        { 
            if (playerMoney == 0)
            {
                playerMoney = PLAYER_RESET_MONEY;
                Console.WriteLine($"Player has lost all money. Resetting game...");
            }
            return playerMoney;
        }
    }
}
