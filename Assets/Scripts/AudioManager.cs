using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SFX")]
    public AudioClip phoneRing;
    public AudioClip correctDispatch;
    public AudioClip incorrectDispatch;
    public AudioClip cameraStatic;

    [Header("Ambience")]
    public AudioClip stormLoop;
    [Range(0f,1f)] public float[] volumeByStage = { 0.3f, 0.6f, 1f };

    [Header("Music")]
    public AudioClip backgroundMusic;
    [Range(0f,1f)] public float musicVolume = 0.5f;

    AudioSource sfxSource;
    AudioSource ambienceSource;
    AudioSource musicSource;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        sfxSource = gameObject.AddComponent<AudioSource>();
        ambienceSource = gameObject.AddComponent<AudioSource>();
        ambienceSource.loop = true;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.clip = backgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) => sfxSource.PlayOneShot(clip);

    public void SetStormStage(int stage)
    {
        if (!ambienceSource.isPlaying)
        {
            ambienceSource.clip = stormLoop;
            ambienceSource.Play();
        }
        ambienceSource.volume = volumeByStage[stage - 1];
    }
}