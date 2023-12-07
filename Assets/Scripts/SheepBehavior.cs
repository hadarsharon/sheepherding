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
        rigbod = GetComponent<Rigidbody>();
        StartCoroutine(Graze());
    }

    IEnumerator Graze()
    {
        int actionChoice = Random.Range(1, 4);
        Debug.Log(actionChoice);
        float countDown = Random.Range(moveTimerMin, moveTimerMax);
        switch (actionChoice)
        {
            case 1:
                Debug.Log("Moving");
                while(countDown > 0)
                {
                    rigbod.AddForce(transform.forward * 650 * Time.smoothDeltaTime, ForceMode.Acceleration);
                    countDown -= Time.smoothDeltaTime;
                    yield return null;
                }
                rigbod.velocity = Vector3.zero;
                break;
            case 2:
                Debug.Log("Rotating");
                float degreesMoved = 0;
                degreesToMove = Random.Range(1, 180);
                int chooseRotation = Random.Range(1, 3);
                int rotationDirection = 1;
                if (chooseRotation == 1) rotationDirection = 1;
                else rotationDirection = -1; 

                Vector3 rotate = new Vector3(0, rotationDirection, 0);
                while (degreesMoved < degreesToMove)
                {
                    transform.Rotate(rotate);
                    degreesMoved ++;
                    yield return new WaitForSeconds(0.02f);
                }
                yield return new WaitForSeconds(countDown);
                break;
            case 3:
                Debug.Log("Pausing");
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
        StartCoroutine(Graze());

    }
}
