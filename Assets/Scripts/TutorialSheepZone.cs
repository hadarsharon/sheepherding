using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSheepZone : MonoBehaviour
{
    private List<GameObject> sheepInPen;

    [SerializeField]
    private TMPro.TMP_Text sheepCounterUI;

    [SerializeField]
    private TMPro.TMP_Text sheepRemaining;

    [SerializeField]
    private GameObject endTutorialScreen;


    // Start is called before the first frame update
    void Start()
    {
        sheepInPen = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSheepCounter();
        if (sheepInPen.Count == 1)
        {
            sheepCounterUI.text = "You Win!";
            endTutorialScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Sheep") && !sheepInPen.Contains(other.transform.gameObject))
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
        if (sheepRemaining is null)
        {
            Debug.Log("sheep remaining is null");
        }
        int sheepLeft = 1;
        if(sheepInPen.Count == 1)
        {
            sheepLeft = 0;
        }
        sheepRemaining.text = "Sheep remaining: " + sheepLeft.ToString();
    }

    private void GoToSaveScore(float score)
    {
        /*SaveScoreUI saveScoreUI = SaveScoreMenu.GetComponent<SaveScoreUI>();
        if (saveScoreUI is null)
        {
            Debug.Log("SheepZone:GoToSaveScore(): SaveScoreUI is null");
            return;
        }
        saveScoreUI.Display(score);*/
    }
}
