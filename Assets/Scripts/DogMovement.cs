using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DogMovement : MonoBehaviour
{
    [SerializeField]
    float forwardSpeed;

    [SerializeField]
    float backwardSpeed;

    [SerializeField]
    float strafeSpeed;

    Rigidbody rigBod;

    bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        rigBod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        followCursor();

    }

    private void FixedUpdate()
    {
        //Check to see if the Dog should be moving
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            isMoving = !isMoving;
        }

        //Move Forward
        if (Input.GetKey(KeyCode.W))
        {
            moveForward(forwardSpeed);
            isMoving = true;
        }

        //Move Backward
        if (Input.GetKey(KeyCode.S))
        {
            moveBackward(backwardSpeed);
            isMoving = true;
        }

        //Strafe Left
        if (Input.GetKey(KeyCode.A))
        {
            strafeLeft(strafeSpeed);
            isMoving = true;
        }

        //Strafe Right
        if (Input.GetKey(KeyCode.D))
        {
            strafeRight(strafeSpeed);
            isMoving = true;
        }

        //Bark
        if (Input.GetKey(KeyCode.Space))
        {
            
        }

        // Stop the rigidbody from moving if no buttons are pressed
        if (!isMoving)
        {
            rigBod.velocity = Vector3.zero;
        }
    }

    /** Make Dag follow the player's cursor*/
    void followCursor()
    {
        //Get mouse position
        Vector3 mousePos = Input.mousePosition;

        //Convert mouse position to a point in the world
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Set targetPos to mouse position point in world
            Vector3 targetPos = hit.point;

            // Rotate Dag
            transform.LookAt(targetPos);

            //Block x and z rotations
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

    void moveForward(float forwardSpeed)
    {
        rigBod.AddForce(transform.forward * forwardSpeed, ForceMode.Acceleration);
    }

    void moveBackward(float backwardSpeed)
    {
        rigBod.AddForce(-transform.forward * backwardSpeed, ForceMode.Acceleration);
    }

    void strafeLeft(float strafeSpeed)
    {
        Vector3 left = new Vector3(-1, 0, 0);
        rigBod.AddForce(left * strafeSpeed, ForceMode.Acceleration);
    }

    void strafeRight(float strafeSpeed)
    {
        Vector3 right = new Vector3(1, 0, 0);
        rigBod.AddForce(right * strafeSpeed, ForceMode.Acceleration);
    }

    void bark()
    {

    }
}
