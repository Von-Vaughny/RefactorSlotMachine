
namespace RefactorSlotMachine
{
    public static class Logic
    {
        const int LINE_3 = 2;
        const int LINE_LENGTH = 3;
        const int RAND_NUM_MAX = 3;
        const int LINE_6 = 5;
        const int LINE_7 = 6;
        const int LINE_8 = 7;
        const int NUM_OF_CHECKS = 8;

        const decimal A_CENT = 0.01M;
        const decimal HALF_BONUS = 0.50M;
        const decimal A_DOLLAR = 1.00M;
        const decimal PLAYER_MONEY_RESET = 20.00M;

        static Random rng = new();


        public static decimal ValidateUserInputWager(decimal playerMoney, decimal userInputWager)
        {
            while (userInputWager < A_CENT || userInputWager > playerMoney)
            {
                UIMethods.DisplayWagerQuestion(playerMoney);
                userInputWager = UIMethods.ReturnUserInput();
            }
            return userInputWager;
        }

        public static decimal CalculateRemainingPlayerMoney(decimal userInputWager, decimal playerMoney)  // -------------
        {
            return playerMoney - userInputWager;
        }

        public static decimal CalculatePotentialEarnings(decimal userInputWager)
        {
            return HALF_BONUS * userInputWager + A_DOLLAR;
        }

        public static int[,] RandomizeSlotMachineRoll()
        {
            int[,] slotMachine = new int[LINE_LENGTH, LINE_LENGTH];
            for (int i = 0; i < LINE_LENGTH; i++)
            {
                for (int j = 0; j < LINE_LENGTH; j++)
                {
                    slotMachine[i, j] = rng.Next(RAND_NUM_MAX);
                }
            }
            return slotMachine;
        }

        public static int GetNumberWins(int[,] slotMachine)
        {
            int slotLineNumber = 0;
            int[] aSingleLine = [0, 0, 0];
            int wins = 0;

            while (slotLineNumber < NUM_OF_CHECKS)
            {
                bool win = false;

                if (slotLineNumber <= LINE_3)
                {
                    aSingleLine = GetHorizontalLine(slotMachine, slotLineNumber, aSingleLine);
                }

                if (slotLineNumber > LINE_3 && slotLineNumber <= LINE_6)
                {
                    aSingleLine = GetVerticalLine(slotMachine, slotLineNumber, aSingleLine);
                }

                if (slotLineNumber > LINE_6)
                {
                    aSingleLine = GetDiagonalLine(slotMachine, slotLineNumber, aSingleLine);
                }

                win = Logic.CheckLine(aSingleLine, win);

                if (win)
                {
                    wins++;
                }
                slotLineNumber++;

                // DELETE IN FINAL CODE
                UIMethods.DisplayIndividualLine(aSingleLine, slotLineNumber, LINE_LENGTH, win);
            }
            return wins;
        }

        public static int[] GetHorizontalLine(int[,] slotMachine, int slotLineNumber, int[] aSingleLine)
        {
            for (int i = 0; i < LINE_LENGTH; i++)
            {
                aSingleLine[i] = slotMachine[slotLineNumber, i];
            }
            return aSingleLine;
        }

        public static int[] GetVerticalLine(int[,] slotMachine, int slotLineNumber, int[] aSingleLine)
        {
            for (int i = 0; i < LINE_LENGTH; i++)
            {
                aSingleLine[i] = slotMachine[i, slotLineNumber % LINE_LENGTH];
            }
            return aSingleLine;
        }

        public static int[] GetDiagonalLine(int[,] slotMachine, int slotlineNumber, int[] aSingleLine)
        {
            int j = LINE_LENGTH;
            for (int i = 0; i < LINE_LENGTH; i++)
            {
                if (slotlineNumber == LINE_7)
                {
                    aSingleLine[i] = slotMachine[i, i];
                }

                if (slotlineNumber == LINE_8)
                {
                    j--;
                    aSingleLine[i] = slotMachine[i, j];
                }
            }
            return aSingleLine;
        }

        public static bool CheckLine(int[] aSingleLine, bool win)
        {
            int lineChecker = 0;
            for (int i = 0; i < LINE_LENGTH; i++)
            {
                if (aSingleLine[0] == aSingleLine[i])
                {
                    lineChecker++;
                }
            }
            if (lineChecker == LINE_LENGTH)
            {
                win = true;
            }
            return win;
        }

        public static decimal CalculateWinEarnings(int wins, decimal userPotentialEarnings)
        {
            return wins * userPotentialEarnings;
        }

        public static decimal CalculateNewPlayerMoney(decimal playerMoney, decimal playerWinAmount)
        {
            return playerMoney + playerWinAmount;
        }

        public static decimal ResetPlayerMoney(decimal playerMoney) 
        {  
            if (playerMoney == 0)
            {
                playerMoney = PLAYER_MONEY_RESET;
            }
            return playerMoney;
        }
    }
}
