// 2. AudioManager.cs
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    private ObjectPool<AudioSource> sfxPool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePool();
    }

    private void InitializePool()
    {
        sfxPool = new ObjectPool<AudioSource>(
            createFunc: () => 
            {
                GameObject obj = new GameObject("SFX_Source");
                obj.transform.SetParent(transform);
                return obj.AddComponent<AudioSource>();
            },
            actionOnGet: (source) => source.gameObject.SetActive(true),
            actionOnRelease: (source) => source.gameObject.SetActive(false),
            actionOnDestroy: (source) => Destroy(source.gameObject),
            defaultCapacity: 10,
            maxSize: 30
        );
    }

    public void PlayMusic(AudioData audioData)
    {
        if (audioData == null || audioData.clip == null) return;
        
        musicSource.outputAudioMixerGroup = audioData.mixerGroup; // Added: Apply Mixer Group
        musicSource.clip = audioData.clip;
        musicSource.volume = audioData.volume;
        musicSource.pitch = audioData.pitch;
        musicSource.loop = true; 
        musicSource.Play();
    }

    public void PlaySFX(AudioData audioData, Vector3 position = default)
    {
        if (audioData == null || audioData.clip == null) return;

        AudioSource source = sfxPool.Get();
        
        source.transform.position = position;
        source.spatialBlend = position == default ? 0f : 1f; 

        // Apply ScriptableObject data
        source.outputAudioMixerGroup = audioData.mixerGroup; // Added: Apply Mixer Group
        source.clip = audioData.clip;
        source.volume = audioData.volume;
        source.loop = audioData.loop;
        source.pitch = audioData.randomizePitch 
            ? audioData.pitch + Random.Range(-audioData.pitchVariance, audioData.pitchVariance) 
            : audioData.pitch;

        source.Play();

        if (!source.loop)
        {
            StartCoroutine(ReturnToPoolAfterDelay(source, audioData.clip.length));
        }
    }

    private IEnumerator ReturnToPoolAfterDelay(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        sfxPool.Release(source);
    }
}
