using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text scoreTable;

    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("ScoreBoard: get scores...");
       // Debug.Log("number of scores stored: " + GameStateManager.GameStateManagerSingleton.scores.Count);
        LoadScoresIntoTable();
    }

    void LoadScoresIntoTable()
    {
        // Dictionary<int, string> playerNames = new Dictionary<int, string>();
        // Dictionary<int, float> playerScores = new Dictionary<int, float>();

        string scoreTableText = "Scores:";

        List<string> playerNames = new List<string>();
        List<float> scoreValues = new List<float>();

        //load scores from combined string into separated lists of player names and score values
        for(int i = 0; i < GameStateManager.GameStateManagerSingleton.scores.Count; i++)
        {
            string scoreLine = GameStateManager.GameStateManagerSingleton.scores[i];
            string[] scorePair = scoreLine.Split(',');
            
            if(!float.TryParse(scorePair[1], out float scoreAsFloat))
            {
                //score is invalid float
                continue;
            }
            playerNames.Add(scorePair[0]);
            scoreValues.Add(scoreAsFloat);
        }

        //bubble sort scores & related names
        for(int i = 0; i < playerNames.Count - 1; i++)
        {
            bool madeSwitch = false;
            for (int j = 0; j < playerNames.Count - 1; j++)
            {
                if(scoreValues[j] > scoreValues[j+1])
                {
                    //switch scores
                    float tempScore = scoreValues[j];
                    scoreValues[j] = scoreValues[j + 1];
                    scoreValues[j + 1] = tempScore;
                    //switch names
                    string tempName = playerNames[j];
                    playerNames[j] = playerNames[j + 1];
                    playerNames[j + 1] = tempName;

                    madeSwitch = true;
                }
            }
            //if list was checked and no switches made, sorted
            if (!madeSwitch)
            {
                break;
            }
        }

        //store scores into string for display
        for(int i = 0; i < playerNames.Count; i++)
        {
            scoreTableText += "\n" + playerNames[i] + " - " + scoreValues[i].ToString();
        }

        scoreTable.text = scoreTableText;
    }
}
