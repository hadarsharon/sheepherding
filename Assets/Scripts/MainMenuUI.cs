using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void GoToTutorial()
    {
        GameStateManager.GameStateManagerSingleton.ChangeGameState(GameStateManager.GameStates.Level1);
    }
    public void GoToGameLevel()
    {
        GameStateManager.GameStateManagerSingleton.ChangeGameState(GameStateManager.GameStates.Level2);
    }
    public void GoToScoreboard()
    {
        GameStateManager.GameStateManagerSingleton.ChangeGameState(GameStateManager.GameStates.ScoreBoard);
    }
    public void GoToMainMenu()
    {
        GameStateManager.GameStateManagerSingleton.ChangeGameState(GameStateManager.GameStates.MainMenu);
    }
}
