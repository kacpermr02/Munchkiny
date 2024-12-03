using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableObject : MonoBehaviour
{
    public enum ActionType { None, LoadScene, StartDialogue } // Typ akcji
    public ActionType action; // Wybór akcji w inspektorze
    public string targetSceneName; // Nazwa sceny, jeśli akcja to LoadScene

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
                // Możesz dodać logikę dialogu tutaj.
                break;

            default:
                Debug.Log($"Kliknięto {gameObject.name}, ale brak przypisanej akcji.");
                break;
        }
    }
}
