using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public delegate void Bark();
    public event Bark OnBark;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnBark?.Invoke();
        }
    }
}
