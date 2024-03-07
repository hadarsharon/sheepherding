using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timePassed;
    private bool timerOn;

    [SerializeField]
    TMPro.TMP_Text timerText;

    public float TimePassed => timePassed;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
        timerOn = true;
        DisplayTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timePassed += Time.deltaTime;
            DisplayTime();
        }
    }

    void DisplayTime()
    {
        timerText.text = "Time: " + timePassed.ToString("0.00");
    }

    public void StopTime()
    {
        timerOn = false;
    }
}
