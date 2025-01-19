using UnityEngine;

public class ImageController : MonoBehaviour
{
    public GameObject target;
    public bool moveBegin = false;
    PuzzleController puzzleMg;

    void Start()
    {
        GameObject gamemanager = GameObject.Find("PuzzleController");
        puzzleMg = gamemanager.GetComponent<PuzzleController>();

    }

    void Update()
    {
        if (moveBegin)
        {
            moveBegin =  false;
            this.transform.position = target.transform.position;
            puzzleMg.checkComplete = true;

        }
    }
}
