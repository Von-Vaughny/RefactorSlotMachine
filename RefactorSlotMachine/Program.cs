using RefactorSlotMachine;

decimal playerMoney = Constants.INITIAL_PLAYER_MONEY;

UIMethods.DisplayWelcomeMessage();

while(true)
{
    decimal userInputWager = Logic.ValidateUserInputWager(playerMoney);
    int[,] slotMachine = Logic.RandomizeSlotMachineRoll();
    playerMoney = Logic.CalculateRemainingPlayerMoney(userInputWager, playerMoney);
    decimal userPotentialEarnings = Logic.CalculatePotentialEarnings(userInputWager);
    UIMethods.DisplaySlotMachine(slotMachine);
    int wins = Logic.GetNumberWins(slotMachine);
    decimal playerWinAmount = Logic.CalculateWinEarnings(wins, userPotentialEarnings);
    playerMoney = Logic.CalculateNewPlayerMoney(playerMoney, playerWinAmount);
    UIMethods.DisplayGameResults(playerWinAmount);
    UIMethods.ResetPlayerMoney(playerMoney);
    playerMoney = Logic.ResetPlayerMoney(playerMoney);
}
