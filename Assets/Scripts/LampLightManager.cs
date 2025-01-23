using UnityEngine;

public class LampLightManager : MonoBehaviour
{
    public GameObject[] Lights;
    public GameObject[] Texts;
    
    private int clickCount = -1;

    void Start()
    {
        SetState(2);
    }

    void OnMouseDown()
    {
        clickCount = (clickCount + 1) % 3;
        SetState(clickCount);
    }

    void SetState(int state)
    {
        SetAllActive(false);

        switch (state)
        {
            case 0:
                ToggleState(0, true);
                break;
            case 1:
                ToggleState(1, true);
                break;
            case 2:
                break;
        }
    }

    void ToggleState(int index, bool state)
    {
        if (Lights != null && index < Lights.Length)
        {
            Lights[index].SetActive(state);
        }
        
        if (Texts != null && index < Texts.Length)
        {
            Texts[index].SetActive(state);
        }
    }

    void SetAllActive(bool state)
    {
        if (Lights != null)
        {
            foreach (var light in Lights)
            {
                light.SetActive(state);
            }
        }

        if (Texts != null)
        {
            foreach (var text in Texts)
            {
                text.SetActive(state);
            }
        }
    }
}
