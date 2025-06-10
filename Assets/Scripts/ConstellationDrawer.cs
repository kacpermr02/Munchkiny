using System.Collections.Generic;
using UnityEngine;

public class ConstellationDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<string> correctOrder;
    private List<Transform> selectedStars = new List<Transform>();
    private Camera cam;

    private bool puzzleCompleted = false;

    void Start()
    {
        cam = Camera.main;
        lineRenderer.positionCount = 0;
        lineRenderer.widthMultiplier = 0.05f;
    }

    void Update()
    {
        if (puzzleCompleted) return;

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.CompareTag("Star"))
            {
                Transform star = hit.transform;
                selectedStars.Add(star);

                StarEffect fx = star.GetComponent<StarEffect>();
                if (fx != null)
                    fx.Flash(Color.white);

                if (selectedStars.Count == 1)
                {
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, star.position);
                    lineRenderer.SetPosition(1, mousePos);
                }
                else
                {
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, star.position);
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
                }

                if (selectedStars.Count == correctOrder.Count)
                {
                    CheckConstellation();
                }
            }
        }

        if (!puzzleCompleted && selectedStars.Count > 0 && selectedStars.Count < correctOrder.Count)
        {
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePos);
        }
    }

    void CheckConstellation()
    {
        if (selectedStars.Count != correctOrder.Count)
            return;

        bool correct = true;

        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (selectedStars[i].name != correctOrder[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            Debug.Log("Poprawny gwiazdozbiór!");
            puzzleCompleted = true;

            foreach (Transform star in selectedStars)
            {
                StarEffect fx = star.GetComponent<StarEffect>();
                if (fx != null)
                    fx.Flash(Color.cyan, 0.5f);
            }
        }
        else
        {
            Debug.Log("Błąd! Spróbuj ponownie.");
            ResetLines();
        }
    }

    void ResetLines()
    {
        selectedStars.Clear();
        lineRenderer.positionCount = 0;
    }
}