using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleController : MonoBehaviour
{
    public int row, col, countMove;
    public int rowBlank, colBlank;
    public int sizeRow, sizeCol;
    public int level = 0;

    public bool controlStart = false;
    public bool checkComplete;
    public bool isComplete;

    private GameObject placeholder;

    public List<GameObject> imageKeyList;
    public List<GameObject> pictureList;
    public List<GameObject> checkPointList;

    private GameObject[,] imageKeyGrid;
    private GameObject[,] pictureGrid;
    private GameObject[,] checkPointGrid;

    private int checkpointIndex = 0;
    private int imageKeyIndex = 0;
    private int completedCount = 0;

    void Start()
    {
        imageKeyGrid = new GameObject[sizeRow, sizeCol];
        pictureGrid = new GameObject[sizeRow, sizeCol];
        checkPointGrid = new GameObject[sizeRow, sizeCol];

        if (level == 1)
        {
            InitializeEasyPuzzle();
        }
        else if (level == 2)
        {
            InitializeHardPuzzle();
        }

        InitializeCheckPoints();
        InitializeImageKeys();
        LocateBlankTile();
    }

    void InitializeCheckPoints()
    {
        for (int i = 0; i < sizeRow; i++)
        {
            for (int j = 0; j < sizeCol; j++)
            {
                checkPointGrid[i, j] = checkPointList[checkpointIndex];
                checkpointIndex++;
            }
        }
    }

    void InitializeImageKeys()
    {
        for (int i = 0; i < sizeRow; i++)
        {
            for (int j = 0; j < sizeCol; j++)
            {
                imageKeyGrid[i, j] = imageKeyList[imageKeyIndex];
                imageKeyIndex++;
            }
        }  
    }

    void LocateBlankTile()
    {
        for (int i = 0; i < sizeRow; i++)
        {
            for (int j = 0; j < sizeCol; j++)
            {
                if (pictureGrid[i, j].name.CompareTo("blank") == 0)
                {
                    rowBlank = i;
                    colBlank = j;
                    return;
                }
            }
        }
    }

    void Update()
    {
        if (controlStart)
        {
            controlStart = false;
            if (countMove == 1)
            {
                HandleTileMovement();
            }
        }
    }

    void HandleTileMovement()
    {
        if (pictureGrid[row, col] != null && pictureGrid[row, col].name.CompareTo("blank") != 0) 
        {
            if (IsAdjacentToBlankTile())
            {
                SwapTiles();
                countMove = 0;
            }
            else
            {
                countMove = 0;
            }
        }
        else
        {
            countMove = 0;
        }
    }

    bool IsAdjacentToBlankTile()
    {
        return (rowBlank != row && colBlank == col && Mathf.Abs(row - rowBlank) == 1) ||
               (rowBlank == row && colBlank != col && Mathf.Abs(col - colBlank) == 1);
    }

    void FixedUpdate() 
    {
        if (checkComplete)
        {
            checkComplete = false;
            ValidatePuzzleCompletion();
        }
    }

    void ValidatePuzzleCompletion()
    {
        for (int i = 0; i < sizeRow; i++)
        {
            for (int j = 0; j < sizeCol; j++)
            {
                if (imageKeyGrid[i, j].name.CompareTo(pictureGrid[i, j].name) == 0)
                {
                    completedCount++;
                }
                else
                {
                    completedCount = 0;
                    Debug.Log("Not win");
                    return;
                }
            }
        }

        if (completedCount == checkPointList.Count)
        {
            isComplete = true;
            Debug.Log("Win");
        }
    }

    void SwapTiles()
    {
        placeholder = pictureGrid[rowBlank, colBlank];
        pictureGrid[rowBlank, colBlank] = pictureGrid[row, col];
        pictureGrid[row, col] = placeholder;

        UpdateTileTargets(rowBlank, colBlank);
        UpdateTileTargets(row, col);

        rowBlank = row;
        colBlank = col;
    }

    void UpdateTileTargets(int targetRow, int targetCol)
    {
        pictureGrid[targetRow, targetCol].GetComponent<ImageController>().target = checkPointGrid[targetRow, targetCol];
        pictureGrid[targetRow, targetCol].GetComponent<ImageController>().moveBegin = true;
    }

    void InitializeEasyPuzzle()
    {
        pictureGrid[0, 0] = pictureList[0];
        pictureGrid[0, 1] = pictureList[2];
        pictureGrid[0, 2] = pictureList[5];
        pictureGrid[1, 0] = pictureList[4];
        pictureGrid[1, 1] = pictureList[1];
        pictureGrid[1, 2] = pictureList[7];
        pictureGrid[2, 0] = pictureList[3];
        pictureGrid[2, 1] = pictureList[6];
        pictureGrid[2, 2] = pictureList[8];
    }

    void InitializeHardPuzzle()
    {
        pictureGrid[0, 0] = pictureList[4];
        pictureGrid[0, 1] = pictureList[0];
        pictureGrid[0, 2] = pictureList[1];
        pictureGrid[0, 3] = pictureList[2];
        pictureGrid[1, 0] = pictureList[8];
        pictureGrid[1, 1] = pictureList[6];
        pictureGrid[1, 2] = pictureList[7];
        pictureGrid[1, 3] = pictureList[11];
        pictureGrid[2, 0] = pictureList[12];
        pictureGrid[2, 1] = pictureList[5];
        pictureGrid[2, 2] = pictureList[14];
        pictureGrid[2, 3] = pictureList[10];
        pictureGrid[3, 0] = pictureList[13];
        pictureGrid[3, 1] = pictureList[9];
        pictureGrid[3, 2] = pictureList[15];
        pictureGrid[3, 3] = pictureList[3];
    }
}
