using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorManager : MonoBehaviour
{
    public static MirrorManager Instance;

    public DraggablePiece[] pieces;
    public string nextSceneName;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckAllPieces()
    {
        foreach (var piece in pieces)
        {
            if (!piece.IsCorrectlyPlaced())
                return;
        }

        Debug.Log("Wszystkie odłamki na miejscu! Ładowanie następnego poziomu...");
        SceneManager.LoadScene(nextSceneName);
    }
}
