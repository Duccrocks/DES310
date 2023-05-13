using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///     Used to handle audio across scenes.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private bool firstMusicSourceIsPlaying;

    private AudioSource musicSource;
    private AudioSource musicSource2;

    private AudioSource sfxSource;
    private float defaultPitch;

    //Makes use of the singleton pattern to create an instance that can be accessed from anywhere in the level.
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // If doesnt have an instance of AudioManager get one
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Adds 2 audio sources for music
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();

        //Adds an audio source for sfx
        sfxSource = gameObject.AddComponent<AudioSource>();

        //Gets all children of master (where the first instance is the parent mixer group, all following being children)
        var audioMixerGroups = audioMixer.FindMatchingGroups("Master");
        musicSource.outputAudioMixerGroup = audioMixerGroups[1];
        musicSource2.outputAudioMixerGroup = audioMixerGroups[1];
        sfxSource.outputAudioMixerGroup = audioMixerGroups[2];


        //Makes the Audiomanager persist throughout on scene change.
        DontDestroyOnLoad(gameObject);

        //Loops the music tracks, just incase a new one doesn't play in time.
        musicSource.loop = true;
        musicSource2.loop = true;
        defaultPitch = sfxSource.pitch;
    }

    private void OnEnable()
    {
        PauseMenu.OnPause += TogglePause;
    }

    private void OnDisable()
    {
        PauseMenu.OnPause -= TogglePause;
    }

    /// <summary>
    ///     Used to play a song with an Audio clip.
    /// </summary>
    /// <param name="musicClip">The song you want to play.</param>
    public void PlayMusic(AudioClip musicClip)
    {
        //A ternary operator, essentially a if statement, if firstMusicSourceIsPlaying is true.
        var activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;
        //Sets the active audio source to the music clip.
        activeSource.clip = musicClip;
        //And plays the song
        activeSource.Play();
    }

    /// <summary>
    ///     Plays the audio clip with a noticeable fade in and fade out.
    /// </summary>
    /// <param name="newClip">The song that you want to play.</param>
    /// <param name="transitionTime">How long to transition in and out of it.</param>
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        //A ternary operator, essentially a if statement, if firstMusicSourceIsPlaying is true. 
        var activeSource = firstMusicSourceIsPlaying ? musicSource : musicSource2;
        //Starts the coroutine and passes through the parameters.
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        //If not playing something, play it.
        if (!activeSource.isPlaying) activeSource.Play();

        var t = 0.0f;
        // Fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = 1 - t / transitionTime;
            yield return null;
        }

        //Stops playing the song.
        activeSource.Stop();
        //Sets the audio source to a new song.
        activeSource.clip = newClip;
        //Play that song.
        activeSource.Play();

        // Fade in.
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            //Fades the volume in.
            activeSource.volume = t / transitionTime;
            //No wait time.
            yield return null;
        }
    }

    /// <summary>
    ///     Plays a sound effect with an Audio clip.
    /// </summary>
    /// <param name="sfxClip">The sound effect to be played.</param>
    public void PlaySound(AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        //Oneshot used so sounds don't overlap
        sfxSource.PlayOneShot(sfxClip);
    }

    /// <summary>
    ///     Overloaded Playsound method with volume control.
    /// </summary>
    /// <param name="sfxClip">The sound effect to be played.</param>
    /// <param name="volume">The volume of the sound effect.</param>
    public void PlaySound(AudioClip sfxClip, float volume)
    {
        //Sets the audiosource to the clip.
        sfxSource.clip = sfxClip;
        //Oneshot used so sounds don't overlap.
        sfxSource.PlayOneShot(sfxClip, volume);
    }

    /// <summary>
    ///     Plays a sound effect with an Audio clip.
    /// </summary>
    /// <param name="sfxClip">The sound effect to be played.</param>
    public void PlaySoundOnce(AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        //Oneshot used so sounds don't overlap
        sfxSource.PlayOneShot(sfxClip);
    }

    /// <summary>
    ///     Overloaded Playsound method with volume control.
    /// </summary>
    /// <param name="sfxClip">The sound effect to be played.</param>
    /// <param name="volume">The volume of the sound effect.</param>
    public void PlaySoundOnce(AudioClip sfxClip, float volume)
    {
        //Sets the audiosource to the clip.
        sfxSource.clip = sfxClip;
        //Oneshot used so sounds don't overlap.
        sfxSource.PlayOneShot(sfxClip, volume);
    }

    public void IncreaseSFXPitch(float pitchMultiplier)
    {
        sfxSource.pitch *= pitchMultiplier;
    }

    public void ResetSFXPitch()
    {
        sfxSource.pitch = defaultPitch;
    }

    public void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            musicSource.Pause();
            musicSource2.Pause();
            sfxSource.Pause();
        }
        else
        {
            musicSource.UnPause();
            musicSource2.UnPause();
            sfxSource.UnPause();
        }
    }

    public void StopAll()
    {
        musicSource.Stop();
        musicSource2.Stop();
        sfxSource.Stop();
    }
}