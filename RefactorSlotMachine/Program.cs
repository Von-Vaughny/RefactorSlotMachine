using RefactorSlotMachine;


while(true)
{
    decimal playerMoney = Constants.INITIAL_PLAYER_MONEY;
    UIMethods.DisplayWelcomeMessage();

    while (true)
    {

        decimal userInputWager = UIMethods.ValidateUserInput(playerMoney);
        int[,] slotMachine = Logic.RandomizeSlotMachineRoll();
        playerMoney = Logic.CalculateRemainingPlayerMoney(userInputWager, playerMoney);
        UIMethods.DisplaySlotMachine(slotMachine);
        int wins = Logic.GetNumberWins(slotMachine);
        decimal playerWinAmount = Logic.CalculateWinEarnings(wins, userInputWager);
        playerMoney = Logic.CalculateNewPlayerMoney(playerMoney, playerWinAmount);
        UIMethods.DisplayGameResults(playerWinAmount);
        if (playerMoney < Constants.MIN_WAGER) 
        {
            UIMethods.DisplayGameResetMessage();
            break;
        }
    }
}
