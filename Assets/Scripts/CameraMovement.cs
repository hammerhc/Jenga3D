using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float dragSpeed;
    public float zoomSpeed;

    private Vector3 lastDragPosition;
    private float minDistance = 15f;
    private float maxDistance = 90f;
    private float distance = 50f;
    private float rotationYAxis = 0.0f;
    private float rotationXAxis = 0.0f;

    void Update()
    {
        scrollCamera();
        moveCamera();
    }

    void LateUpdate()
    {
        transform.LookAt(target.position);
    }

    void moveCamera()
    {
        if (Input.GetMouseButtonDown(0))
            lastDragPosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = lastDragPosition - Input.mousePosition;

            rotationYAxis = delta.x * dragSpeed * Time.deltaTime;
            rotationXAxis = delta.y * dragSpeed * Time.deltaTime;

            transform.RotateAround(target.position, transform.up, rotationYAxis);
            transform.RotateAround(target.position, transform.right, rotationXAxis);

            lastDragPosition = Input.mousePosition;
        }
    }

    void scrollCamera()
    {
        distance = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }
}
