using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager GameStateManagerSingleton { get; private set; }
    public GameStates CurrentGameState => currentGameState;

    private GameStates currentGameState;
    
    //should match scene IDs in Build Settings
    public enum GameStates
    {
        MainMenu = 0,
        LevelSelect = 1,
        ScoreBoard = 2,
        Level1 = 3,
        Level2 = 4
    }

    private void Awake()
    {
        //for the singleton
        if (GameStateManagerSingleton != null && GameStateManagerSingleton != this)
        {
            Destroy(this);
        }
        else
        {
            GameStateManagerSingleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameStates.MainMenu;
    }

    public void ChangeGameState(GameStates newGameState)
    {
        if(newGameState != currentGameState)
        {
            MoveScene((int)newGameState);
        }
    }

    private void MoveScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
    
}
