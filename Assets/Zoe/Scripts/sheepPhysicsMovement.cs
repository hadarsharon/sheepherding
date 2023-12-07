using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheepPhysicsMovement : MonoBehaviour
{
    Rigidbody rb;

    public float forwardForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 force = new Vector3(0, 0, forwardForce);
        rb.AddForce(transform.forward * forwardForce);
    }
}
