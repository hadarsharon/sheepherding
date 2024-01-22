using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SheepBehavior : MonoBehaviour
{
    [SerializeField]
    float sheepSpeedMin, sheepSpeedMax;
    float sheepSpeed;

    [SerializeField]
    float grazeTimerMin = 3, grazeTimerMax = 7;

    [SerializeField]
    float obedienceTimerMin = 3, obedienceTimerMax = 7;

    [SerializeField]
    float barkDistance;

    bool hasRunAway = false;

    Rigidbody rigbod;
    Rigidbody dogBod;

    private void OnEnable()
    {
        FindObjectOfType<EventManager>().OnBark += RunAway;
    }
    private void OnDisable()
    {
        FindObjectOfType<EventManager>().OnBark -= RunAway;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Give the sheep a random Y rotation value on start
        float randomRotationY = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomRotationY, 0f);

        //Get Sheep RigidBody
        rigbod = GetComponent<Rigidbody>();

        //Get the Dog
        dogBod = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        //Start the Graze() Coroutine
        StartCoroutine(Graze());
    }

    IEnumerator Graze()
    {
        //Choose one of three actions
        int actionChoice = Random.Range(1, 4);
        //Set countDown timer
        float countDown = Random.Range(grazeTimerMin, grazeTimerMax);
        switch (actionChoice)
        {
            case 1:
                //Move the sheep forward over a period of time
                while (countDown > 0)
                {
                    //rigbod.AddForce(transform.forward * sheepSpeed * Time.smoothDeltaTime, ForceMode.Acceleration);
                    sheepSpeed = Random.Range(sheepSpeedMin, sheepSpeedMax);
                    rigbod.velocity = transform.forward * sheepSpeed;
                    countDown -= Time.smoothDeltaTime;
                    yield return null;
                }
                //Stop Sheep from moving when movement is completed
                rigbod.velocity = Vector3.zero;
                break;
            case 2:
                //Rotate the sheep
                float degreesMoved = 0;
                //Set the random degrees the sheep will rotate
                float degreesToMove = Random.Range(1, 180);

                //Randomly choose negative or positive movement for clockwise and counterclockwise rotation
                int chooseRotation = Random.Range(1, 3);
                int rotationDirection;
                if (chooseRotation == 1) rotationDirection = 1;
                else rotationDirection = -1;

                Vector3 rotate = new Vector3(0, rotationDirection, 0);
                //Rotate the sheep over a period of time
                while (degreesMoved < degreesToMove)
                {
                    transform.Rotate(rotate);
                    degreesMoved++;
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(countDown);
                break;
            case 3:
                //Pause the sheep for a period of time
                rigbod.velocity = Vector3.zero;
                while (countDown > 0)
                {
                    countDown -= Time.smoothDeltaTime;
                    yield return null;
                }
                break;
            default:
                break;
        }
        //Restart the Graze() Coroutine
        RestartGraze();

    }

    void RestartGraze()
    {
        StopAllCoroutines();
        StartCoroutine(Graze());
    }

    public void RunAway()
    {
        float dogDistance = Vector3.Distance(dogBod.position, rigbod.position);
        //Debug.Log(dogDistance);
        if (dogDistance <= barkDistance)
        {
            StopAllCoroutines();
            StartCoroutine(RunAwayCoroutine());
       }
    }

    IEnumerator RunAwayCoroutine()
    {
        //Get the direction of the Dog in relation to the Sheep
        Vector3 direction = dogBod.position - rigbod.position;
        direction.Normalize();
        // Calculate the rotation to face the opposite direction
        Quaternion rotation = Quaternion.LookRotation(-direction);
        //Rotate the sheep over a period of time
        while (Quaternion.Angle(transform.rotation, rotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 180f * Time.deltaTime);
            yield return new WaitForSeconds(0.02f); // Adjust speed here as well
        }

        //Set countDown timer
        float countDown = Random.Range(obedienceTimerMin, obedienceTimerMax);
        //Run Away
        while (countDown > 0)
        {
            //rigbod.AddForce(transform.forward * sheepSpeed * Time.smoothDeltaTime, ForceMode.Acceleration);
            sheepSpeed = Random.Range(sheepSpeedMin, sheepSpeedMax);
            rigbod.velocity = transform.forward * sheepSpeed;
            countDown -= Time.smoothDeltaTime;
            yield return null;
        }
        rigbod.velocity = Vector3.zero;
        //Return to Grazing once obedience timer runs out
        yield return new WaitForSeconds(countDown);
        hasRunAway = true;
        StartCoroutine(Graze());


        yield return null;
    }
}
