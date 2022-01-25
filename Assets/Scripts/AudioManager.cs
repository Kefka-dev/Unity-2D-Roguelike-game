using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic, gameOverMusic, winMusic, titleMenuMusic;

    public AudioSource[] sfx;

    private string FirstPlayPref = "FirstPlay";
    private string musicPref = "musicPref";
    private string sfxPref = "sfxPref";

    private int firstPlayInt;
    public Slider musicSlider, sfxSlider;
    private float musicVolume, sfxVolume;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlayPref);
        if(firstPlayInt == 0)
        {
            musicVolume = 0.25f;
            sfxVolume = .5f;
            musicSlider.value = musicVolume;
            sfxSlider.value = sfxVolume;

            PlayerPrefs.SetFloat(musicPref, musicVolume);
            PlayerPrefs.SetFloat(sfxPref, sfxVolume);
            PlayerPrefs.SetInt(FirstPlayPref, -1);
        } else
        {
            musicVolume = PlayerPrefs.GetFloat(musicPref);
            musicSlider.value = musicVolume;
            sfxVolume = PlayerPrefs.GetFloat(sfxPref);
            sfxSlider.value = sfxVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlayVictoryMusic()
    {
        levelMusic.Stop();
        winMusic.Play();
    }

    public void PlaySFX(int SFXid)
    {
        sfx[SFXid].Stop();
        sfx[SFXid].Play();
    }
    public void UpdateSound()
    {
        levelMusic.volume = musicSlider.value;
        winMusic.volume = musicSlider.value;
        gameOverMusic.volume = musicSlider.value;
        titleMenuMusic.volume = musicSlider.value;

        for (int i = 0; i<sfx.Length; i++)
        {
            sfx[i].volume = sfxSlider.value;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(musicPref, musicSlider.value);
        PlayerPrefs.SetFloat(sfxPref, sfxSlider.value);
        Debug.Log(PlayerPrefs.GetFloat(musicPref));
        Debug.Log(PlayerPrefs.GetFloat(sfxPref));
    }
    /*private void OnApplicationFocus(bool inFocus)
    {
        if(inFocus == false)
        {
            SaveSoundSettings();
        }
    }*/

}
