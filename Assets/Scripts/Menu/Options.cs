using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Slider sensSlider, volumeSlider;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private AudioMixer audioMixer;
    private ColourBlindController colourBlindController;
    private void Start()
    {
        colourBlindController = FindObjectOfType<ColourBlindController>();
        InitialisePrefs();
    }

    /// <summary>
    ///     Sets the sensitivity of the players camera.
    /// </summary>
    /// <param name="sensitivity">The value of the volume</param>
    public void SetSensitivity(float sensitivity)
    {
        print($"The sensitivity has changed to {sensitivity}");
        //Saves this sensitivity so if the player reloads it'll keep their sensitivity.
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
    }

    /// <summary>
    ///     Sets the volume of the game.
    /// </summary>
    /// <param name="volume">The value of the volume</param>
    public void SetVolume(float volume)
    {
        print($"The volume has changed to {volume}");
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
        //Saves this volume so if the player reloads it'll keep their volume.
    }

    /// <summary>
    ///     Sets the quality of the game.
    /// </summary>
    /// <param name="quality">The value of the quality</param>
    public void SetQuality(int quality)
    {
        print(quality);
        PlayerPrefs.SetInt("quality", quality);
        QualitySettings.SetQualityLevel(quality);
    }

    /// <summary>
    ///     Sets if it's fullscreen or not.
    /// </summary>
    /// <param name="isFullScreen">If full screen or not</param>
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    /// <summary>
    ///     Sets if colourblindness is on or off
    /// </summary>
    /// <param name="colourBlindValue">The value of the quality</param>
    public void SetColourBlindness(int colourBlindValue)
    {
        print(colourBlindValue);
        PlayerPrefs.SetInt("colourblind", colourBlindValue);
        colourBlindController.currentColourblindSetting = (ColourBlindController.ColouBlindMode)colourBlindValue;

    }

    //Default values for player.
    private void InitialisePrefs()
    {
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            var previousSensitivity = PlayerPrefs.GetFloat("sensitivity", 5);
            sensSlider.value = previousSensitivity;
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            var previousVolume = PlayerPrefs.GetFloat("volume", 0.75f);
            audioMixer.SetFloat("volume", Mathf.Log10(previousVolume) * 20);
            volumeSlider.value = previousVolume;
        }

        if (PlayerPrefs.HasKey("colourblind"))
        {
            var previousColourBlindness = PlayerPrefs.GetInt("colourblind",0);
        }

        if (PlayerPrefs.HasKey("quality"))
        {
            var previousQuality = PlayerPrefs.GetInt("quality", 2);
            QualitySettings.SetQualityLevel(previousQuality);
            qualityDropdown.value = previousQuality;
        }
    }
}