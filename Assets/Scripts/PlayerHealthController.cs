using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;

    public float durationInvinc;

    private float invincCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // subject to change
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
            if(invincCounter <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r,
                    PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }

        }
    }
    public void DamagePlayer()
    {
        if(invincCounter <= 0)
        {
            currentHealth --;
            invincCounter = durationInvinc;

            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, 
                PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);

            if(currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);

                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void BeInvincible(float duration)
    {
        invincCounter = duration;
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r,
            PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
