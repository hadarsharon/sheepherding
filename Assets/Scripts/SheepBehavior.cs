using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SheepBehavior : MonoBehaviour
{
    [SerializeField]
    float moveTimerMin, moveTimerMax;

    float degreesToMove;

    Rigidbody rigbod;
    // Start is called before the first frame update
    void Start()
    {
        //Get Sheep RigidBody
        rigbod = GetComponent<Rigidbody>();

        //Start the Graze() Coroutine
        StartCoroutine(Graze());
    }

    IEnumerator Graze()
    {
        //Choose one of three actions
        int actionChoice = Random.Range(1, 4);
        //Set countDown timer
        float countDown = Random.Range(moveTimerMin, moveTimerMax);
        switch (actionChoice)
        {
            case 1:
                //Move the sheep forward over a period of time
                while(countDown > 0)
                {
                    rigbod.AddForce(transform.forward * 650 * Time.smoothDeltaTime, ForceMode.Acceleration);
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
                degreesToMove = Random.Range(1, 180);

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
                    degreesMoved ++;
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
        StartCoroutine(Graze());

    }
}
