using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject m_Ground;
    public float dragSpeed = 2;

    private Vector3 lastDragPosition;

    private void LateUpdate()
    {
        Camera.main.transform.LookAt(m_Ground.transform.position);

        if (Input.GetMouseButtonDown(0))
            lastDragPosition = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * dragSpeed);
            lastDragPosition = Input.mousePosition;
        }
    }
}
