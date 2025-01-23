using UnityEngine;
using UnityEngine.SceneManagement;

public class RedLoadLevel : MonoBehaviour
{
    public int level;
    private void OnMouseDown() 
    {
        SceneManager.LoadScene(level);
        Debug.Log("Click");
    }
}
