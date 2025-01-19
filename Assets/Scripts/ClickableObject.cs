using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableObject : MonoBehaviour
{
    public enum ActionType { None, LoadScene, StartDialogue }
    public ActionType action;
    public string targetSceneName;

    public void OnClick()
    {
        switch (action)
        {
            case ActionType.LoadScene:
                if (!string.IsNullOrEmpty(targetSceneName))
                {
                    SceneManager.LoadScene(targetSceneName);
                }
                break;

            case ActionType.StartDialogue:
                Debug.Log($"Rozpoczynam dialog z {gameObject.name}");
                break;

            default:
                Debug.Log($"Kliknięto {gameObject.name}, ale brak przypisanej akcji.");
                break;
        }
    }
}
