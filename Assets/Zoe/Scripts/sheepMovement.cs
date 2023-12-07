using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheepMovement : MonoBehaviour
{
    public float speed = 10f;

    public float turn = 5;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // float randomTurnAmount = Random.Range(-5f, 5f);
        //turn += randomTurnAmount;

        float turn = (float)Math.Sin(Time.time * 0.5) * 5;

        float forward = 1f;

        transform.Translate(speed * Time.deltaTime * new Vector3(0, 0, forward));
        transform.Rotate(speed * Time.deltaTime * new Vector3(0, turn, 0));


      //  transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, forward);


    }
}
