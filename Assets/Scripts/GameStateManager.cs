using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager GameStateManagerSingleton { get; private set; }
    public GameStates CurrentGameState => currentGameState;

    private GameStates currentGameState;

    private List<LevelData> levelDatas;
    
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
        //game loads into Main Menu
        currentGameState = GameStates.MainMenu;

        //load up data for levels
        levelDatas = new List<LevelData>();
        LevelData levelData1 = new LevelData(5, 10, 0);
        levelDatas.Add(levelData1);

    }

    public void ChangeGameState(GameStates newGameState)
    {
        if(newGameState != currentGameState)
        {
            MoveScene((int)newGameState);

            currentGameState = newGameState;

            if((int)currentGameState > 2) //state is a game level, set up required
            {
                SetUpGameLevel();
            }
        }
    }
    
    private void MoveScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    private void SetUpGameLevel()
    {
        //add sheep to scene and set their speed values
        
    }
    ///////Level Data///////
    public class LevelData
    {
        public int noOfSheep;
        public float sheepSpeedBase;
        public float sheepSpeedRange;

        public LevelData(int noOfSheep, float sheepSpeedBase, float sheepSpeedRange)
        {
            this.noOfSheep = noOfSheep;
            this.sheepSpeedBase = sheepSpeedBase;
            this.sheepSpeedRange = sheepSpeedRange;
        }
    }
    
}
