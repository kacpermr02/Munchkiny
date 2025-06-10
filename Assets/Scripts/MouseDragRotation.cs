using UnityEngine;

public class MouseDragRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float doubleClickThreshold = 0.3f;

    private bool isDragging = false;
    private Quaternion initialRotation;
    private float lastClickTime = -1f;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickThreshold)
            {
                ResetRotation();
            }
            else
            {
                isDragging = true;
            }

            lastClickTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float rotX = Input.GetAxis("Mouse Y") * rotationSpeed;
            float rotY = -Input.GetAxis("Mouse X") * rotationSpeed;

            transform.Rotate(Vector3.up, rotY, Space.World);
            transform.Rotate(Vector3.right, rotX, Space.World);
        }
    }

    private void ResetRotation()
    {
        transform.rotation = initialRotation;
    }
}
