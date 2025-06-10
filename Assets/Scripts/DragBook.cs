using UnityEngine;

public class DragBook : MonoBehaviour
{
    public int correctSlotIndex;

    private Vector3 offset;
    private bool dragging = false;

    public float moveSpeed = 10f;
    public Vector3 targetPosition; 

    private void Start()
    {
        targetPosition = transform.position;
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
        SnapAndSwap();
        BooksManager.instance.CheckBooks();
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            mousePosition.z = 0;
            transform.position = mousePosition;
            targetPosition = transform.position;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void SnapAndSwap()
    {
        Transform[] slots = BooksManager.instance.slots;

        Transform closestSlot = null;
        float closestDistance = float.MaxValue;

        foreach (Transform slot in slots)
        {
            float dist = Vector2.Distance(transform.position, slot.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestSlot = slot;
            }
        }

        if (closestSlot == null) return;

        DragBook bookOnSlot = FindBookAtPosition(closestSlot.position);

        if (bookOnSlot != null && bookOnSlot != this)
        {
            Vector3 tempTarget = bookOnSlot.targetPosition;
            bookOnSlot.targetPosition = this.targetPosition;
            this.targetPosition = tempTarget;
        }
        else
        {
            targetPosition = closestSlot.position;
        }
    }

    DragBook FindBookAtPosition(Vector3 position)
    {
        DragBook[] books = BooksManager.instance.books;
        foreach (DragBook book in books)
        {
            if (book != this && Vector2.Distance(book.targetPosition, position) < 0.1f)
            {
                return book;
            }
        }
        return null;
    }
}
