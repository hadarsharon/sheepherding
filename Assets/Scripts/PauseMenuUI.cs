using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject pauseButton;

    public void PauseGame() 
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }
    public void BackToMainMenu()
    {
        GameStateManager.GameStateManagerSingleton.ChangeGameState(GameStateManager.GameStates.MainMenu);
    }
}
