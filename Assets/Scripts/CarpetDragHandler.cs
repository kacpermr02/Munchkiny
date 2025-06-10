using UnityEngine;

public class CarpetDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging = false;

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    void OnMouseDrag()
    {
        if (dragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = 0;
            transform.position = newPosition;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (dragging && Input.GetKeyDown(KeyCode.R))
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        transform.Rotate(0f, 0f, 90f);
    }
}