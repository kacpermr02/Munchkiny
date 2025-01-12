using UnityEngine;
using UnityEngine.UIElements;

public class LockControlPuzzle : MonoBehaviour
{
    private int[] result, correctCombination;
    public GameObject targetObject;
    public Vector3 desiredRotation;
    void Start()
    {
        result = new int[]{5, 5, 5, 5};
        correctCombination = new int[] {2, 1, 3, 7};
        RotatateRullerLockPuzzle.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "Ruller1":
            result[0] = number;
            break;

            case "Ruller2":
            result[1] = number;
            break;

            case "Ruller3":
            result[2] = number;
            break;

            case "Ruller4":
            result[3] = number;
            break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1] && result[2] == correctCombination[2] && result[3] == correctCombination[3])
        {
            Debug.Log("Opedned!");
            SetRotation(targetObject, desiredRotation);
        }

    }

    private void SetRotation(GameObject obj, Vector3 rotation)
    {
        if (obj != null)
        {
            obj.transform.rotation = Quaternion.Euler(rotation); // Ustaw rotację
            Debug.Log($"Rotacja obiektu {obj.name} ustawiona na {rotation}");
        }
    }

    
    private void OnDestroy() 
    {
        RotatateRullerLockPuzzle.Rotated -= CheckResults;
    }
 }


