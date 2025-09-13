using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioSource gameMusic; // AudioSource untuk music in-game

    void Start()
    {
        // Cari AudioSource music in-game jika belum diassign
        if (gameMusic == null)
        {
            gameMusic = GameObject.FindGameObjectWithTag("GameMusic")?.GetComponent<AudioSource>();
        }

        // Setup PlayerPrefs dan slider
        if (!PlayerPrefs.HasKey("gameMusicVolume"))
        {
            PlayerPrefs.SetFloat("gameMusicVolume", 0.5f); // Default volume 50%
            LoadMusicVolume();
        }
        else
        {
            LoadMusicVolume();
        }

        // Add listener untuk slider
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        // Ubah volume music in-game
        if (gameMusic != null)
        {
            gameMusic.volume = volume;
        }

        SaveMusicVolume();
    }

    private void LoadMusicVolume()
    {
        if (musicSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("gameMusicVolume", 0.5f);
            musicSlider.value = savedVolume;

            // Apply volume ke music
            if (gameMusic != null)
            {
                gameMusic.volume = savedVolume;
            }
        }
    }

    private void SaveMusicVolume()
    {
        if (musicSlider != null)
        {
            PlayerPrefs.SetFloat("gameMusicVolume", musicSlider.value);
        }
    }

    // Method untuk assign AudioSource secara manual
    public void SetGameMusic(AudioSource musicSource)
    {
        gameMusic = musicSource;
        if (gameMusic != null && musicSlider != null)
        {
            gameMusic.volume = musicSlider.value;
        }
    }

    // Method untuk mute/unmute music
    public void ToggleMusic(bool isOn)
    {
        if (gameMusic != null)
        {
            gameMusic.mute = !isOn;
        }
    }
}