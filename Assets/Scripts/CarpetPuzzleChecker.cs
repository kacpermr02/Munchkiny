using UnityEngine;
using System.Collections;

public class CarpetPuzzleChecker : MonoBehaviour
{
    [Header("Carpet Transforms")]
    public Transform carpet1;
    public Transform carpet2;
    public Transform carpet3;

    [Header("Correct Positions")]
    public Vector3 correctPosition1;
    public Vector3 correctPosition2;
    public Vector3 correctPosition3;

    [Header("Correct Rotations (Z in degrees)")]
    public float correctRotation1;
    public float correctRotation2;
    public float correctRotation3;

    [Header("Tolerances")]
    public float allowedDistance = 0.5f;
    public float allowedRotationDifference = 1f;

    [Header("Puzzle Result")]
    [SerializeField] private GameObject combinedCarpet;
    [SerializeField] private float delayBeforeSwap = 1.5f;

    private bool puzzleSolved = false;

    void Start()
    {
        if (combinedCarpet != null)
            combinedCarpet.SetActive(false);
    }

    void Update()
    {
        if (!puzzleSolved && IsSolved())
        {
            puzzleSolved = true;
            Debug.Log("Puzzle solved!");
            StartCoroutine(SwapCarpetsWithDelay());
        }
    }

    bool IsSolved()
    {
        return
            Vector3.Distance(carpet1.position, correctPosition1) < allowedDistance &&
            Vector3.Distance(carpet2.position, correctPosition2) < allowedDistance &&
            Vector3.Distance(carpet3.position, correctPosition3) < allowedDistance &&

            Mathf.Abs(carpet1.eulerAngles.z - correctRotation1) < allowedRotationDifference &&
            Mathf.Abs(carpet2.eulerAngles.z - correctRotation2) < allowedRotationDifference &&
            Mathf.Abs(carpet3.eulerAngles.z - correctRotation3) < allowedRotationDifference;
    }

    IEnumerator SwapCarpetsWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeSwap);

        carpet1.gameObject.SetActive(false);
        carpet2.gameObject.SetActive(false);
        carpet3.gameObject.SetActive(false);

        if (combinedCarpet != null)
            combinedCarpet.SetActive(true);
    }
}