using UnityEngine;

public class MovePuzzle : MonoBehaviour
{
    public int row, col;
    PuzzleController puzzleMg;

    void Start()
    {
        GameObject gamemanager = GameObject.Find("PuzzleController");
        puzzleMg = gamemanager.GetComponent<PuzzleController>();
    }

    void Update()
    {
        
    }

    void OnMouseDown() 
    {
        Debug.Log("Row is: " + row + " Col is: " + col);
        puzzleMg.countMove += 1;
        puzzleMg.row = row;
        puzzleMg.col = col;
        puzzleMg.controlStart = true;
    }
}
