using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//area sheep need to be herded into to complete game
public class SheepZone : MonoBehaviour
{
    private List<GameObject> sheepInPen;

    private GameObject[] sheepArray;

    private int sheepTotal;

    [SerializeField]
    private TMPro.TMP_Text sheepCounterUI;

    // Start is called before the first frame update
    void Start()
    {
        sheepInPen = new List<GameObject>();

        sheepArray = GameObject.FindGameObjectsWithTag("Sheep");
        sheepTotal = sheepArray.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sheepInPen.Count >= sheepTotal)
        {
            sheepCounterUI.text = "You Win!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.CompareTag("Sheep") && !sheepInPen.Contains(other.transform.parent.gameObject))
        {
            sheepInPen.Add(other.transform.parent.gameObject);
            UpdateSheepCounter();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.CompareTag("Sheep") && sheepInPen.Contains(other.transform.parent.gameObject))
        {
            sheepInPen.Remove(other.transform.parent.gameObject);
            UpdateSheepCounter();
        }
        
    }

    private void UpdateSheepCounter()
    {
        sheepCounterUI.text = "Sheep Count: " + sheepInPen.Count;
    }
}
