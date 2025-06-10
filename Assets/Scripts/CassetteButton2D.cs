using UnityEngine;

public class CassetteButton2D : MonoBehaviour
{
    public enum ButtonType { Play, Stop, Rewind }
    public ButtonType type;
    public CassettePlayer2D player;

    private void OnMouseDown()
    {
        switch (type)
        {
            case ButtonType.Play:
                player.Play();
                break;
            case ButtonType.Stop:
                player.Stop();
                break;
            case ButtonType.Rewind:
                player.Rewind();
                break;
        }
    }
}
