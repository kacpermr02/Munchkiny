using UnityEngine;

public class LampLightManager : MonoBehaviour
{
    public GameObject[] Lights;  // Tablica przechowująca wszystkie światła
    public GameObject[] Texts;   // Tablica przechowująca wszystkie teksty

    // Zmienna do śledzenia stanu kliknięcia
    private int clickCount = -1; // Zaczynamy od -1, aby po pierwszym kliknięciu przejść do stanu 0

    void Start()
    {
        // Ustawienie początkowego stanu - wszystko wyłączone
        SetState(2); // Stan 2: Wszystko wyłączone
    }

    // Ta metoda zostanie wywołana, gdy klikniesz na obiekt, do którego jest przypisany ten skrypt
    void OnMouseDown()
    {
        // Zmieniamy stan przełączając między zestawami
        clickCount = (clickCount + 1) % 3; // Cykl: -1 -> 0 -> 1 -> 2 -> 0
        SetState(clickCount);
    }

    // Funkcja ustawia stan w zależności od kliknięcia
    void SetState(int state)
    {
        // Resetujemy wszystkie światła i teksty na początku
        SetAllActive(false);

        switch (state)
        {
            case 0:
                // Aktywacja pierwszego zestawu
                ToggleState(0, true);
                break;

            case 1:
                // Aktywacja drugiego zestawu
                ToggleState(1, true);
                break;

            case 2:
                // Wyłączenie wszystkiego
                break;
        }
    }

    // Funkcja, która włącza/wyłącza światła i teksty na podstawie indeksu
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

    // Funkcja, która wyłącza wszystkie światła i teksty
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
