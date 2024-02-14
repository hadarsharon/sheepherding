using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField] private GameObject dog;
    [SerializeField] private float minYPosition = 8f;
    [SerializeField] private float maxYPosition = 12f;
    [SerializeField] private float zoomSpeed = 1f;
    [SerializeField] private float minZoomSize = 5f;
    [SerializeField] private float maxZoomSize = 10f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - dog.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Follow the dog
        transform.position = dog.transform.position - offset;

        // Ensure Y position stays within specified limits
        float clampedYPosition = Mathf.Clamp(transform.position.y, minYPosition, maxYPosition);
        transform.position = new Vector3(transform.position.x, clampedYPosition, transform.position.z);

        // Zoom in/out based on mouse scroll wheel
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        float newSize = Camera.main.orthographicSize - scrollData * zoomSpeed;
        newSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        Camera.main.orthographicSize = newSize;
    }
}