using UnityEngine;
using UnityEngine.UIElements;

public class Columns : MonoBehaviour
{
    private int[][] results, correctCombinations;
    public GameObject[] targetObjects;
    public Vector3[] desiredRotations;

    void Start()
    {
        results = new int[targetObjects.Length][];
        correctCombinations = new int[targetObjects.Length][];

        for (int i = 0; i < targetObjects.Length; i++)
        {
            results[i] = new int[] { 5, 5, 5, 5 };
        }

        correctCombinations[0] = new int[] { 2, 5, 1, 0 };
        correctCombinations[1] = new int[] { 9, 4, 7, 8 }; 

        RotatateRullerLockPuzzle.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "Ruller1":
                UpdateResults(0, number);
                break;

            case "Ruller2":
                UpdateResults(1, number);
                break;

            case "Ruller3":
                UpdateResults(2, number);
                break;

            case "Ruller4":
                UpdateResults(3, number);
                break;
        }

        for (int i = 0; i < targetObjects.Length; i++)
        {
            if (IsCombinationCorrect(i))
            {
                Debug.Log("Opened!");
                SetRotation(targetObjects[i], desiredRotations[i]);
            }
        }
    }

    private void UpdateResults(int position, int number)
    {
        for (int i = 0; i < results.Length; i++)
        {
            results[i][position] = number;
        }
    }

    private bool IsCombinationCorrect(int objectIndex)
    {
        for (int j = 0; j < correctCombinations[objectIndex].Length; j++)
        {
            if (results[objectIndex][j] != correctCombinations[objectIndex][j])
            {
                return false;
            }
        }
        return true;
    }

    private void SetRotation(GameObject obj, Vector3 rotation)
    {
        if (obj != null)
        {
            obj.transform.rotation = Quaternion.Euler(rotation);
            Debug.Log($"Rotacja obiektu {obj.name} ustawiona na {rotation}");
        }
    }

    private void OnDestroy()
    {
        RotatateRullerLockPuzzle.Rotated -= CheckResults;
    }
}
