using Microsoft.VisualBasic;
using RefactorSlotMachine;

const string LOSS = "LOST", WIN = "WON";
const decimal A_CENT = 0.01M, HALF_BONUS = 0.50M, A_DOLLAR = 1.00M, PLAYER_MONEY_RESET = 20.00M;
const int LINE_3 = 2, LINE_LENGTH = 3, RAND_NUM_MAX = 3, LINE_6 = 5, LINE_7 = 6, LINE_8 = 7, NUM_OF_CHECKS = 8;
decimal playerMoney = PLAYER_MONEY_RESET;
Random rng = new ();
// --------------------------------------------------------------------

UIMethods.DisplayWelcomeMessage();

while(true)
{
    decimal userInputWager = ValidateUserInputWager(playerMoney, 0);
    int[,] slotMachine = RandomizeSlotMachineRoll(LINE_LENGTH, RAND_NUM_MAX);
    playerMoney = CalculateRemainingPlayerMoney(playerMoney, userInputWager);
    decimal userPotentialEarnings = CalculatePotentialEarnings(userInputWager);
    UIMethods.DisplaySlotMachine(slotMachine, LINE_LENGTH);
    int wins = GetNumberWins(slotMachine);
    decimal playerWinAmount = CalculateWinEarnings(wins, userPotentialEarnings);
    playerMoney = CalculateNewPlayerMoney(playerMoney, playerWinAmount);
    string playerResult = PlayerResult(wins);
    UIMethods.DisplayGameResults(playerResult, playerWinAmount);
    playerMoney = UIMethods.ResetPlayerMoney(playerMoney, PLAYER_MONEY_RESET);
}


//--------------------------------------------------------------------------
int[,] RandomizeSlotMachineRoll(int lineLength, int randomNumberMax) 
{
    int [,] slotMachine = new int[lineLength, lineLength];
    for (int i = 0; i < lineLength; i++)
    {
        for (int j = 0; j < lineLength; j++)
        {
            slotMachine[i, j] = rng.Next(randomNumberMax); 
        }
    }
    return slotMachine;
}  

decimal ValidateUserInputWager(decimal playerMoney, decimal userInputWager)
{
    while (userInputWager < A_CENT || userInputWager > playerMoney)
    {
        UIMethods.DisplayWagerQuestion(playerMoney);
        userInputWager = UIMethods.ReturnUserInput();
    }
    return userInputWager;
}

decimal CalculateRemainingPlayerMoney(decimal playerMoney, decimal userInputWager)  // -------------
{
    return playerMoney - userInputWager;
}

decimal CalculatePotentialEarnings(decimal userInputWager)
{
    return HALF_BONUS * userInputWager + A_DOLLAR;
}

decimal CalculateWinEarnings(int wins, decimal userPotentialEarnings) 
{ 
    return wins * userPotentialEarnings;
}

decimal CalculateNewPlayerMoney(decimal playerMoney, decimal playerWinAmount) 
{
    return playerMoney + playerWinAmount;
}

int GetNumberWins(int[,] slotMachine) 
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

        win = CheckLine(aSingleLine, win);

        if (win) 
        {
            wins++; 
        }
        slotLineNumber++;

        UIMethods.DisplayIndividualLine(aSingleLine, slotLineNumber, LINE_LENGTH, win);
    }
    return wins;
}

int[] GetHorizontalLine(int[,] slotMachine, int slotLineNumber, int[] aSingleLine)
{
    for (int i = 0; i < LINE_LENGTH; i++)
    {
        aSingleLine[i] = slotMachine[slotLineNumber, i];
    }
    return aSingleLine;
}

int[] GetVerticalLine(int[,] slotMachine, int slotLineNumber, int[] aSingleLine) 
{
    for (int i = 0; i < LINE_LENGTH; i++) 
    {
        aSingleLine[i] = slotMachine[i, slotLineNumber % 3];
    }
    return aSingleLine;
}

int[] GetDiagonalLine(int[,] slotMachine, int slotlineNumber, int[] aSingleLine) 
{   
    int j = LINE_LENGTH;
    for (int i = 0; i < LINE_LENGTH - 1; i++) 
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

bool CheckLine(int[] aSingleLine, bool win)
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

string PlayerResult(int wins) 
{
    string playerResult = LOSS;
    if (wins > 0)
    {
        playerResult = WIN;
    }
    return playerResult;
}

