using RefactorSlotMachine;

decimal playerMoney = Constants.PLAYER_MONEY_RESET;

UIMethods.DisplayWelcomeMessage();

while(true)
{
    decimal userInputWager = Logic.ValidateUserInputWager(playerMoney, 0);
    int[,] slotMachine = Logic.RandomizeSlotMachineRoll();
    playerMoney = Logic.CalculateRemainingPlayerMoney(userInputWager, playerMoney);
    decimal userPotentialEarnings = Logic.CalculatePotentialEarnings(userInputWager);
    UIMethods.DisplaySlotMachine(slotMachine);
    int wins = Logic.GetNumberWins(slotMachine);
    decimal playerWinAmount = Logic.CalculateWinEarnings(wins, userPotentialEarnings);
    playerMoney = Logic.CalculateNewPlayerMoney(playerMoney, playerWinAmount);
    UIMethods.DisplayGameResults(playerWinAmount);
    playerMoney = UIMethods.ResetPlayerMoney(playerMoney);
}
