using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused;
    public int currentCoins;

    private string musicPref = "musicPref";
    private string sfxPref = "sfxPref";
    public Transform startPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.transform.position = startPoint.position;
        PlayerController.instance.canMove = true;

        currentCoins = CharacterTracker.instance.currentCoins;
        Time.timeScale = 1f;

        AudioManager.instance.levelMusic.volume = PlayerPrefs.GetFloat(musicPref);
        AudioManager.instance.winMusic.volume = PlayerPrefs.GetFloat(musicPref);
        AudioManager.instance.gameOverMusic.volume = PlayerPrefs.GetFloat(musicPref);
        //AudioManager.instance.titleMenuMusic.volume = PlayerPrefs.GetFloat(musicPref);

        for (int i = 0; i < AudioManager.instance.sfx.Length; i++)
        {
            AudioManager.instance.sfx[i].volume = PlayerPrefs.GetFloat(sfxPref);
        }

        UIController.instance.coinText.text = currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayVictoryMusic();

        PlayerController.instance.canMove = false;

        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(waitToLoad);

        CharacterTracker.instance.currentCoins = currentCoins;
        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;

        SceneManager.LoadScene(nextLevel);
    }

    public void pauseUnpause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);

            isPaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);

            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void getCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.coinText.text = currentCoins.ToString();
    }

    public void spendCoins(int amount)
    {
        currentCoins -= amount;
        if(currentCoins < 0)
        {
            currentCoins = 0;
        }
        UIController.instance.coinText.text = currentCoins.ToString();
    }
}
