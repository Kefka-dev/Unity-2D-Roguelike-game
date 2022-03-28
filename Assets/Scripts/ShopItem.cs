using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;

    private bool inBuyZone;

    public bool isHealthRestore, isHealthUpgrade, isWeapon;

    public int itemPrice;

    public int HealAmmount, MaxHealthIncreaseAmount;

    public Gun[] potentialGuns;
    private Gun theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;


    // Start is called before the first frame update
    void Start()
    {
        if(isWeapon == true)
        {
            int selectedGun = Random.Range(0, potentialGuns.Length);
            theGun = potentialGuns[selectedGun];

            gunSprite.sprite = theGun.gunShopSprite;
            itemPrice = theGun.itemCost;
            infoText.text = theGun.weaponName + "\n - " + theGun.itemCost + " Gold - ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(LevelManager.instance.currentCoins >= itemPrice)
                {

                    if(isHealthRestore && PlayerHealthController.instance.currentHealth!=PlayerHealthController.instance.maxHealth)
                    {
                        LevelManager.instance.spendCoins(itemPrice);
                        PlayerHealthController.instance.HealPlayer(HealAmmount);
                        Destroy(gameObject);
                    }

                    if (isHealthUpgrade)
                    {
                        LevelManager.instance.spendCoins(itemPrice);
                        PlayerHealthController.instance.increaseMaxHealth(MaxHealthIncreaseAmount);
                        Destroy(gameObject);
                    }
                    
                    if (isWeapon)
                    {
                        bool hasGun = false;
                        foreach (Gun gunToCheck in PlayerController.instance.availableGuns)
                        {
                            if (theGun.weaponName == gunToCheck.weaponName)
                            {
                                hasGun = true;
                            }
                        }
                        if (hasGun == false)
                        {
                            LevelManager.instance.spendCoins(itemPrice);
                            Gun gunClone = Instantiate(theGun);
                            gunClone.transform.parent = PlayerController.instance.gunArm;
                            if (gunClone.Minigun == true)
                            {
                                gunClone.transform.position = new Vector3(PlayerController.instance.gunArm.position.x + .1f, PlayerController.instance.gunArm.position.y - .4f, PlayerController.instance.gunArm.position.z);
                                Debug.Log("MAM MINIGUN");
                            }
                            else
                            {
                                gunClone.transform.position = PlayerController.instance.gunArm.position;
                                Debug.Log("MAM INU ZBRAN");
                            }
                            gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                            gunClone.transform.localScale = Vector3.one;

                            PlayerController.instance.availableGuns.Add(gunClone);
                            PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count - 1;
                            PlayerController.instance.switchGun();
                            Destroy(gameObject);
                        }
                    }

                    AudioManager.instance.PlaySFX(19);
                }
                else
                {
                    AudioManager.instance.PlaySFX(20);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
