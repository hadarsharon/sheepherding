using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveScoreUI : MonoBehaviour
{
    [SerializeField]
    private GameObject SaveScoreMenu;
    [SerializeField]
    private TMPro.TMP_InputField playerName;

    private float playersScore;

    public void Display(float score)
    {
        SaveScoreMenu.SetActive(true);
        playersScore = score;
    }
    public void SaveScore()
    {
        GameStateManager.GameStateManagerSingleton.SaveScore(playerName.text, playersScore);
    }
    public void GoToMain()
    {
        GameStateManager.GameStateManagerSingleton.ChangeGameState(GameStateManager.GameStates.MainMenu);
    }

    
}
