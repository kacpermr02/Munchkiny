using UnityEngine;

public class ExitOnEscape : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Aplikacja została zamknięta.");
        }
    }
}
