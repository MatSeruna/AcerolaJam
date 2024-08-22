using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public bool isMusicMuted;
    public bool isSoundMuted;

    public AudioMixerGroup music;
    public AudioMixerGroup sound;

    public Slider sliderMusic;
    public Slider sliderSound;

    public Toggle toggleMusic;
    public Toggle toggleSound;

    public TextMeshProUGUI textMusicValue;
    public TextMeshProUGUI textSoundValue;
    // Start is called before the first frame update
    void Start()
    {      
        SetMusicVolume();
        SetSoundVolume();       
    }

    private void Update()
    {
               
    }

    public void SetMusicVolume()
    {
        float volume = Mathf.Lerp(-80, 0, sliderMusic.value);
        music.audioMixer.SetFloat("Music", volume);
        textMusicValue.text = volume.ToString();
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume()
    {
        float volume = Mathf.Lerp(-80,0, sliderSound.value);
        sound.audioMixer.SetFloat("Sound", volume);
        textSoundValue.text = volume.ToString();
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void ToggleMusic()
    {
        if (toggleMusic.isOn)
        {
            music.audioMixer.SetFloat("Music", 0);
            sliderMusic.interactable = true;
        }
        else
        {
            music.audioMixer.SetFloat("Music", -80);
            sliderMusic.interactable = false;
        }
    }

    public void ToggleSound()
    {
        if (toggleSound.isOn)
        {
            sound.audioMixer.SetFloat("Sound", 0);
            sliderSound.interactable = true;
        }
        else
        {
            sound.audioMixer.SetFloat("Sound", -80);
            sliderSound.interactable = false;
        }
    }

    void LoadVolume()
    {
        sliderMusic.value = Mathf.Lerp(0, 1, PlayerPrefs.GetFloat("MusicVolume"));
        sliderSound.value = Mathf.Lerp(0, 1, PlayerPrefs.GetFloat("SoundVolume")); 
        SetMusicVolume();
        SetSoundVolume();
    }

}
