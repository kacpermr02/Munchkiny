using UnityEngine;

public class clock : MonoBehaviour
{
  [SerializeField]
  private Transform handMinute, handHour;

  [SerializeField]
  private GameObject winText;

  private void Start() 
  {
    winText.SetActive(false);
  }

  private void OnMouseDown() 
  {
    handMinute.Rotate(Vector3.back, 30);
    handHour.Rotate(Vector3.back, 2.5f);

    if ((Mathf.Round(handMinute.rotation.eulerAngles.z * 2) /2) == 30 && (Mathf.Round(handHour.rotation.eulerAngles.z *2) /2) == 212.5f)
    {
        winText.SetActive(true);
    }
  }
}
