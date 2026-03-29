// 1. AudioData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioData", menuName = "Audio System/Audio Data")]
public class AudioData : ScriptableObject
{
    [Header("Core Settings")]
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop = false;

    [Header("Randomization")]
    [Tooltip("Enable to slightly randomize the pitch each time it plays, preventing repetitive sounds.")]
    public bool randomizePitch = false;
    [Range(0f, 0.5f)] public float pitchVariance = 0.1f;
}
