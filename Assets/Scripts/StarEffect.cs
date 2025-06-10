using UnityEngine;

public class StarEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector3 originalScale;
    private Color originalColor;
    public ParticleSystem particles;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalColor = sr.color;

        if (particles == null)
        {
            particles = GetComponentInChildren<ParticleSystem>();
        }
    }

    public void Flash(Color flashColor, float duration = 0.2f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(flashColor, duration));

        if (particles != null)
        {
            particles.Play();
        }
    }

    private System.Collections.IEnumerator FlashCoroutine(Color flashColor, float duration)
    {
        float t = 0f;
        float half = duration / 2f;

        while (t < half)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 1.5f, t / half);
            sr.color = Color.Lerp(originalColor, flashColor, t / half);
            transform.localScale = originalScale * scale;
            yield return null;
        }

        t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1.5f, 1f, t / half);
            sr.color = Color.Lerp(flashColor, originalColor, t / half);
            transform.localScale = originalScale * scale;
            yield return null;
        }

        sr.color = originalColor;
        transform.localScale = originalScale;
    }
}