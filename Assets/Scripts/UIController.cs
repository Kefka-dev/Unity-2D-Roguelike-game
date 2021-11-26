using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText, coinText;

    public RawImage mapRenderer;

    public GameObject deathScreen;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;


    public string newGameScene, mainMenuScene;

    public GameObject pauseMenu;

    public Animator minimapAnim;
    public Camera miniMapCam;
    public int zoomedCamSize, normalCamSize;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime)); 
            if(fadeScreen.color.a == 0)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1)
            {
                fadeToBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void newGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(newGameScene);
    }

    public void mainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(mainMenuScene);
    }

    public void resume()
    {
        LevelManager.instance.pauseUnpause();
    }

    public void mapLowOppacity()
    {
        mapRenderer.color = new Color(mapRenderer.color.r, mapRenderer.color.g, mapRenderer.color.b, .2f);
        //mapRenderer.color = new Color(mapRenderer.color.r, mapRenderer.color.g, Mathf.MoveTowards(mapRenderer.color.a, .2f, mapFadeSpeed * Time.deltaTime));
    }

    public void mapNormalOppacity()
    {
        mapRenderer.color = new Color(mapRenderer.color.r, mapRenderer.color.g, mapRenderer.color.b, .6f);
        //mapRenderer.color = new Color(mapRenderer.color.r, mapRenderer.color.g, Mathf.MoveTowards(mapRenderer.color.a, .6f, mapFadeSpeed * Time.deltaTime));

    }

    public void mapScaleUp()
    {
        minimapAnim.SetBool("ScaleUp", true);
        miniMapCam.orthographicSize = zoomedCamSize;
    }

    public void mapScaleDown()
    {
        minimapAnim.SetBool("ScaleUp", false);
        miniMapCam.orthographicSize = normalCamSize;
    }
}
