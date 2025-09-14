using UnityEngine;

public class ingamemusic : MonoBehaviour
{
    AudioSource MusicSource;
    AudioSource SoundSource;

    void Start()
    {
        MusicSource = GameObject.FindGameObjectWithTag("IngameMusic").GetComponent<AudioSource>();
        SoundSource = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            PlayerPrefs.SetFloat("soundVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    private void Load()
    {
        MusicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        SoundSource.volume = PlayerPrefs.GetFloat("soundVolume");
    }
}