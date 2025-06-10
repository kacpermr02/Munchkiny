using UnityEngine;

public class BooksManager : MonoBehaviour
{
    public static BooksManager instance;

    public Transform[] slots; 
    public DragBook[] books;

    private bool puzzleSolved = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Invoke("CheckBooks", 0.5f);
    }

    private void Update()
    {
        if (!puzzleSolved)
        {
            CheckBooks();
        }
    }

    public void CheckBooks()
    {
        bool allCorrect = true;

        for (int i = 0; i < slots.Length; i++)
        {
            DragBook bookOnSlot = FindBookAtPosition(slots[i].position);

            if (bookOnSlot == null || bookOnSlot.correctSlotIndex != i)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect && !puzzleSolved)
        {
            puzzleSolved = true;
            Debug.Log("Rozwiązano!");
        }
    }

    private DragBook FindBookAtPosition(Vector3 position)
    {
        foreach (var book in books)
        {
            if (Vector2.Distance(book.targetPosition, position) < 0.1f)
            {
                return book;
            }
        }
        return null;
    }
}
