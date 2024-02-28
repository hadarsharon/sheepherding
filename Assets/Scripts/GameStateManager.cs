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
        ScoreBoard = 1,
        Level1 = 2,
        Level2 = 3
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
        LevelData levelData1 = new LevelData(10, 3, 10, 3, 7, 3, 7, 10, 5);
        levelDatas.Add(levelData1);

    }

    public void ChangeGameState(GameStates newGameState)
    {
        if(newGameState != currentGameState)
        {
            StartCoroutine(MoveScene(newGameState));
        }
    }
    
    IEnumerator MoveScene(GameStates newGameState)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync((int)newGameState);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        currentGameState = newGameState;

        if ((int)currentGameState > 1) //state is a game level, set up required
        {
            //need to wait frame for objects to be loaded before game can be set up
            yield return new WaitForEndOfFrame();
            SetUpGameLevel();
        }
    }

    private void SetUpGameLevel()
    {
        
        //add sheep to scene and set their speed values
        if (currentGameState == GameStates.Level2)
        {
            GameObject startZoneObj = GameObject.Find("StartArea");
            if (startZoneObj is null)
            {
                Debug.Log("GameStateManager:SetUpGameLevel - Error: No StartArea object in scene");
                return;
            }
            StartAreaScript startZone = startZoneObj.GetComponent<StartAreaScript>();
            if(startZone is null)
            {
                Debug.Log("GameStateManager:SetUpGameLevel - Error: No StartAreaScript attached to StartAreaObject");
                return;
            }
            else
            {
                startZone.SpawnSheep(levelDatas[0]);
            }
        }
    }
    ///////Level Data///////
    public class LevelData
    {
        public int noOfSheep;
        public float sheepSpeedMin;
        public float sheepSpeedMax;
        public float grazeTimerMin;
        public float grazeTimerMax;
        public float obedienceTimerMin;
        public float obedienceTimerMax;
        public float barkDistance;
        public float minFenceDistance;

        public LevelData(int noOfSheep, float sheepSpeedMin, float sheepSpeedMax, float grazeTimerMin, float grazeTimerMax, 
            float obedienceTimerMin, float obedienceTimerMax, float barkDistance, float minFenceDistance)
        {
            this.noOfSheep = noOfSheep;
            this.sheepSpeedMin = sheepSpeedMin;
            this.sheepSpeedMax = sheepSpeedMax;
            this.grazeTimerMin = grazeTimerMin;
            this.grazeTimerMax = grazeTimerMax;
            this.obedienceTimerMin = obedienceTimerMin;
            this.obedienceTimerMax = obedienceTimerMax;
            this.barkDistance = barkDistance;
            this.minFenceDistance = minFenceDistance;
        }
    }
}
