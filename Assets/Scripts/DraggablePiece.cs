using UnityEngine;

public class DraggablePiece : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging;

    public Transform targetSlot;
    public float snapDistance = 0.5f;
    public float snapRotationTolerance = 5f;

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
        }

        if (Input.GetKeyDown(KeyCode.R) && IsMouseOver())
        {
            transform.Rotate(0, 0, 90);
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        dragging = false;

        float distance = Vector2.Distance(transform.position, targetSlot.position);
        float angleDifference = Mathf.Abs(Quaternion.Angle(transform.rotation, targetSlot.rotation));

        if (distance <= snapDistance && angleDifference <= snapRotationTolerance)
        {
            transform.position = targetSlot.position;
            transform.rotation = targetSlot.rotation;
            Debug.Log($"Odłamek '{name}' dopasowany do swojego slotu!");
        }
        else
        {
            Debug.Log($"Odłamek '{name}' nie dopasowany");
        }

        MirrorManager.Instance.CheckAllPieces();
    }

    private bool IsMouseOver()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = GetComponent<Collider2D>();
        return col.OverlapPoint(mousePos);
    }

    public bool IsCorrectlyPlaced()
    {
        float distance = Vector2.Distance(transform.position, targetSlot.position);
        float angleDifference = Mathf.Abs(Quaternion.Angle(transform.rotation, targetSlot.rotation));
        return distance <= snapDistance && angleDifference <= snapRotationTolerance;
    }
}
