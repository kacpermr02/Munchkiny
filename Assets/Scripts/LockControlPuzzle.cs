using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class LockControlPuzzle : MonoBehaviour
{
    private int[] result, correctCombination;
    public GameObject targetObject;
    public Vector3 desiredRotation;
    public int sceneIndexToLoad;

    void Start()
    {
        result = new int[]{5, 5, 5, 5};
        correctCombination = new int[] {4, 4, 5, 2};
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
            Debug.Log("Opened!");
            SetRotation(targetObject, desiredRotation);
            StartCoroutine(LoadSceneAfterDelay(1f));
        }
    }

    private void SetRotation(GameObject obj, Vector3 rotation)
    {
        if (obj != null)
        {
            obj.transform.rotation = Quaternion.Euler(rotation); 
            Debug.Log($"Rotacja obiektu {obj.name} ustawiona na {rotation}");
        }
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndexToLoad);
    }

    private void OnDestroy() 
    {
        RotatateRullerLockPuzzle.Rotated -= CheckResults;
    }
}
