using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isCorrectSlot = false;
    public GameObject correctBook;
    public Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void AssignBook(GameObject book)
    {
        if (book == correctBook)
        {
            isCorrectSlot = true;
        }
        else
        {
            isCorrectSlot = false;
        }
    }
}
