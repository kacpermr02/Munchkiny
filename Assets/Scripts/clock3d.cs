using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class clock3d : MonoBehaviour
{
    [SerializeField]
    private Transform handMinute, handHour;

    [SerializeField]
    private Transform clockCenter; // Środek zegara, wokół którego będą obracać się wskazówki

    [SerializeField]
    public int sceneIndexToLoad;

    private CheckHour checkHour;

    private void Start()
    {
        checkHour = GameObject.Find("Circle").GetComponent<CheckHour>();
    }

    private void OnMouseDown()
    {
        // Obracanie wskazówek wokół środka zegara
        handMinute.RotateAround(clockCenter.position, Vector3.forward, -30);
        handHour.RotateAround(clockCenter.position, Vector3.forward, -2.5f);

        // Pobranie kąta obrotu wskazówek
        float minuteAngle = Mathf.Round(handMinute.eulerAngles.z * 2) / 2;
        float hourAngle = Mathf.Round(handHour.eulerAngles.z * 2) / 2;

        if (minuteAngle == 90 && hourAngle == 187.5f && checkHour.isClicked)
        {
            StartCoroutine(LoadSceneAfterDelay(1f));
        }
        else
        {
            checkHour.isClicked = false;
            checkHour.GetComponent<Renderer>().material.color = Color.white;
            Debug.Log("Wrong hour");
        }

        Debug.Log("Minute hand: " + minuteAngle);
        Debug.Log("Hour hand: " + hourAngle);
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndexToLoad);
    }
}
