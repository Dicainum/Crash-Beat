using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VolumeController : MonoBehaviour
{
    [Header("UI Settings")]
    public Slider volumeSlider; // The UI Slider

    private List<AudioSource> audioSources = new List<AudioSource>(); // List to store all AudioSources
    private Dictionary<AudioSource, float> initialVolumes = new Dictionary<AudioSource, float>(); // Store initial volumes
    private Saves saves;
    private float volume = 1f;

    private void Start()
    {
        audioSources.AddRange(FindObjectsOfType<AudioSource>());
        foreach (var audioSource in audioSources)
        {
            initialVolumes[audioSource] = audioSource.volume;
        }
        saves = Saves.saves;
        volume = saves._sound;
        AdjustVolume(volume);
        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
        }
    }
    void OnEnable()
    {
        if (volumeSlider == null)
        {
            Debug.Log("Slider is not assigned.");
            return;
        }
        audioSources.AddRange(FindObjectsOfType<AudioSource>());

        // Save the initial volume for each AudioSource
        foreach (var audioSource in audioSources)
        {
            initialVolumes[audioSource] = audioSource.volume;
        }

        // Set slider to represent 100% initially
        //volumeSlider.value = 1f;

        // Add listener to the slider
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(AdjustVolume);
        }
    }

    void AdjustVolume(float sliderValue)
    {
        // Adjust volume proportionally to the initial volume for each AudioSource
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = initialVolumes[audioSource] * sliderValue;
        }
        saves._sound = sliderValue;
    }

    private void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(AdjustVolume);
        }
        // Clean up listener when the object is destroyed
    }
}