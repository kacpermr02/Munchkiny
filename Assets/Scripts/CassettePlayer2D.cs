using UnityEngine;

public class CassettePlayer2D : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool isRewinding = false;

    void Update()
    {
        if (isRewinding)
        {
            audioSource.time = Mathf.Max(0f, audioSource.time - Time.deltaTime * 2f);
        }
    }

    public void Play()
    {
        isRewinding = false;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void Stop()
    {
        isRewinding = false;

        audioSource.Pause();
    }

    public void Rewind()
    {
        isRewinding = true;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            audioSource.Pause();
        }
    }
}
