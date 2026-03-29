// 3. AudioTrigger.cs
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioData sfxToPlay;
    public bool playOnAwake = false;
    public bool playAs3D = false;

    private void Start()
    {
        if (playOnAwake)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if (AudioManager.Instance != null && sfxToPlay != null)
        {
            Vector3 pos = playAs3D ? transform.position : default;
            AudioManager.Instance.PlaySFX(sfxToPlay, pos);
        }
    }
}
