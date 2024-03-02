using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SheepBehavior : MonoBehaviour
{
    [SerializeField]
    float sheepSpeedMin, sheepSpeedMax;
    float sheepSpeed;

    [SerializeField]
    float grazeTimerMin, grazeTimerMax;

    [SerializeField]
    float obedienceTimerMin, obedienceTimerMax;

    [SerializeField]
    float barkDistance;

    [SerializeField]
    float minFenceDistance;

    [SerializeField]
    float rayLength = 1f;

    float fenceDistance = float.MaxValue;

    bool hasRunAway = false;
    bool nearFence = false;
    bool isGrounded;

    Rigidbody rigBod;
    Rigidbody dogBod;

    GameObject[] Fences;
    GameObject closestFence;

    float distance;

    private Vector3 gizmoSpherePosition;
    private bool drawGizmo = false;


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
        rigBod = GetComponent<Rigidbody>();

        //Get the Dog
        dogBod = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        //Start the Graze() Coroutine
        StartCoroutine(Graze());

        Fences = GameObject.FindGameObjectsWithTag("Fence");

        gizmoSpherePosition = transform.position;
        drawGizmo = true;
    }

    void Update()
    {
        if (Fences != null && nearFence == false)
        {

            //Check to see how close the Fences are
            foreach (GameObject f in Fences)
            {
                distance = Vector3.Distance(transform.position, f.transform.position);
                if (distance <= fenceDistance)
                {
                    fenceDistance = distance;
                    closestFence = f;
                }
                if (Vector3.Distance(transform.position, closestFence.transform.position) < minFenceDistance)
                {
                    nearFence = true;
                    MoveFromFence(closestFence);

                }
            }
        }
        isGrounded = GroundCheck();
    }

    void FixedUpdate()
    {

        if (!isGrounded)
        {
            Vector3 newVelocity = new Vector3(rigBod.velocity.x, rigBod.velocity.y - 2f, rigBod.velocity.z);
            rigBod.velocity = newVelocity;
        }
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
                    rigBod.velocity = transform.forward * sheepSpeed;
                    countDown -= Time.smoothDeltaTime;
                    yield return null;
                }
                //Stop Sheep from moving when movement is completed
                rigBod.velocity = Vector3.zero;
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
                rigBod.velocity = Vector3.zero;
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
        float dogDistance = Vector3.Distance(dogBod.position, rigBod.position);
        if (dogDistance <= barkDistance)
        {
            StopAllCoroutines();
            StartCoroutine(RunAwayCoroutine());
        }
    }

    void MoveFromFence(GameObject fence)
    {
        StopAllCoroutines();
        StartCoroutine(MoveFromFenceCoroutine(fence));
    }

    IEnumerator RunAwayCoroutine()
    {
        //Get the direction of the Dog in relation to the Sheep
        Vector3 direction = dogBod.position - rigBod.position;
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
            rigBod.velocity = transform.forward * sheepSpeed;
            countDown -= Time.smoothDeltaTime;
            yield return null;
        }
        rigBod.velocity = Vector3.zero;
        //Return to Grazing once obedience timer runs out
        yield return new WaitForSeconds(countDown);
        hasRunAway = true;
        RestartGraze();


        yield return null;
    }

    IEnumerator MoveFromFenceCoroutine(GameObject fence)
    {
        //Set the sheep velocity to zero
        rigBod.velocity = Vector3.zero;

        //pause for a second
        yield return new WaitForSeconds(1.0f);

        //Get the direction of the Fence in relation to the sheep
        Vector3 direction = fence.transform.position - rigBod.position;
        direction.Normalize();
        direction = new Vector3(direction.x, 0f, direction.z);

        //Calculate the rotation to face the opposite direction
        Quaternion rotation = Quaternion.LookRotation(-direction);

        //Rotate the sheep over a period of time
        while (Quaternion.Angle(transform.rotation, rotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 180f * Time.deltaTime);
            yield return new WaitForSeconds(0.02f); // Adjust speed here as well
        }


        //Set countDown timer
        float countDown = Random.Range(obedienceTimerMin, obedienceTimerMax);

        //Move away
        while (countDown > 0)
        {
            sheepSpeed = Random.Range(sheepSpeedMin, sheepSpeedMax);
            rigBod.velocity = transform.forward * sheepSpeed;
            countDown -= Time.smoothDeltaTime;
            yield return null;
        }
        nearFence = false;

        //Return to Grazing
        RestartGraze();

        yield return null;
    }

    bool GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            return true;
        }
       // Debug.Log(transform.position.y);
        return false;
    }

    public void InjectSheepParameters(GameStateManager.LevelData levelData)
    {
        sheepSpeedMin = levelData.sheepSpeedMin;
        sheepSpeedMax = levelData.sheepSpeedMax;
        grazeTimerMin = levelData.grazeTimerMin;
        grazeTimerMax = levelData.grazeTimerMax;
        obedienceTimerMin = levelData.obedienceTimerMin;
        obedienceTimerMax = levelData.obedienceTimerMax;
        barkDistance = levelData.barkDistance;
        minFenceDistance = levelData.minFenceDistance;
    }
}
