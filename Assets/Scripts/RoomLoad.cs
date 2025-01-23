using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomLoad : MonoBehaviour
{
    public int level;
    private void OnMouseDown() 
    {
        SceneManager.LoadScene(level);
    }
}
