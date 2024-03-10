using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//area sheep need to be herded into to complete game
public class SheepZone : MonoBehaviour
{
    private List<GameObject> sheepInPen;

    private GameObject[] sheepArray;

    private int sheepTotal => GameObject.FindGameObjectsWithTag("Sheep").Length + GameObject.FindGameObjectsWithTag("SheepInPen").Length;

    [SerializeField]
    private TMPro.TMP_Text sheepCounterUI;

    [SerializeField]
    private TMPro.TMP_Text sheepRemaining;

    [SerializeField]
    private GameObject SaveScoreMenu;

    [SerializeField]
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        sheepInPen = new List<GameObject>();

        //sheepArray = GameObject.FindGameObjectsWithTag("Sheep");

        //sheepTotal = Gam;


        //for testing:
        //GoToSaveScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSheepCounter();
        if (sheepInPen.Count >= sheepTotal)
        {
            sheepCounterUI.text = "You Win!";
            float finalScore = timer.TimePassed;
            timer.StopTime();
            GoToSaveScore(finalScore);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Sheep") && !sheepInPen.Contains(other.transform.gameObject))
        {
            sheepInPen.Add(other.transform.gameObject);
            UpdateSheepCounter();
            other.transform.gameObject.tag = "SheepInPen";

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("SheepInPen") && sheepInPen.Contains(other.transform.gameObject))
        {
            sheepInPen.Remove(other.transform.gameObject);
            UpdateSheepCounter();
            other.transform.gameObject.tag = "Sheep";
        }
        
    }

    private void UpdateSheepCounter()
    {
        sheepCounterUI.text = "Sheep Count: " + sheepInPen.Count;
        if(sheepRemaining is null)
        {
            Debug.Log("sheep remaining is null");
        }
        sheepRemaining.text = "Sheep remaining: " + (sheepTotal - sheepInPen.Count).ToString();
    }

    private void GoToSaveScore(float score)
    {
        SaveScoreUI saveScoreUI = SaveScoreMenu.GetComponent<SaveScoreUI>();
        if(saveScoreUI is null)
        {
            Debug.Log("SheepZone:GoToSaveScore(): SaveScoreUI is null");
            return;
        }
        saveScoreUI.Display(score);
    }
}
