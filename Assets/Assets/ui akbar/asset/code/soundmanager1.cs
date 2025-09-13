using UnityEngine;
using UnityEngine.UI;

public class soundmanager1 : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] Slider MusicSlider;
    [SerializeField] AudioSource MusicSource;

    [Header("Sound")]
    [SerializeField] Slider SoundSlider;
    [SerializeField] AudioSource SoundSource;


    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            PlayerPrefs.SetFloat("soundVolume", 1);
        }
        LoadSliderValue();
        LoadVolume();
    }

    public void ChangeVolumeMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", MusicSlider.value);
        MusicSource.volume = PlayerPrefs.GetFloat("musicVolume");
    }

    public void ChangeVolumeSfx()
    {
        PlayerPrefs.SetFloat("soundVolume", SoundSlider.value);
        SoundSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }

    void LoadSliderValue()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SoundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    void LoadVolume()
    {
        MusicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        SoundSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }
}