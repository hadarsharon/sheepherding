using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField]
    public GameObject dog;
    [SerializeField]
    public float YPosition = 8f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - dog.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = dog.transform.position - offset;

        transform.position = new Vector3(transform.position.x, YPosition, transform.position.z);
    }
}