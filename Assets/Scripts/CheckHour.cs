using UnityEngine;

public class CheckHour : MonoBehaviour
{
    public bool isClicked = false;

    void OnMouseDown()
    {
        isClicked = true;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

}
