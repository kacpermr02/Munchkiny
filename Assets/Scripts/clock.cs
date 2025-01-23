using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections;

public class clock : MonoBehaviour
{
  [SerializeField]
  private Transform handMinute, handHour;

  [SerializeField]
  public int sceneIndexToLoad;

  private CheckHour checkHour;

  private void Start() 
  { 
    checkHour = GameObject.Find("Circle").GetComponent<CheckHour>();
  }

  private void OnMouseDown() 
  {
    handMinute.Rotate(Vector3.back, 30);
    handHour.Rotate(Vector3.back, 2.5f);

    if ((Mathf.Round(handMinute.rotation.eulerAngles.z * 2) / 2) == 90 && (Mathf.Round(handHour.rotation.eulerAngles.z * 2) / 2) == 187.5f && checkHour.isClicked == true)
    {
      StartCoroutine(LoadSceneAfterDelay(1f));
    }
    else
    {
      checkHour.isClicked = false;
      checkHour.GetComponent<SpriteRenderer>().color = Color.white;
      Debug.Log("Wrong hour");
    }

    Debug.Log("Minute hand: " + handMinute.rotation.eulerAngles.z);
    Debug.Log("Hour hand: " + handHour.rotation.eulerAngles.z);

  }

  private IEnumerator LoadSceneAfterDelay(float delay)
  {
      yield return new WaitForSeconds(delay);
      SceneManager.LoadScene(sceneIndexToLoad);
  }

}
