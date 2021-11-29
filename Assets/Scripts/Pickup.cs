using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isMedkit, isCoin, isGun;

    public int healAmount;

    public int coinValue;

    public Gun theGun;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(isMedkit == true && PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.HealPlayer(healAmount);
                Destroy(gameObject);

                AudioManager.instance.PlaySFX(8);
            } else if(isGun == true)
            {
                bool hasGun = false;
                foreach(Gun gunToCheck in PlayerController.instance.availableGuns)
                {
                    if(theGun.weaponName == gunToCheck.weaponName)
                    {
                        hasGun = true;
                    }
                }

                if(hasGun == false)
                {
                    Gun gunClone = Instantiate(theGun);
                    gunClone.transform.parent = PlayerController.instance.gunArm;
                    if(gunClone.Minigun == true)
                    {
                        gunClone.transform.position = new Vector3(PlayerController.instance.gunArm.position.x + .1f, PlayerController.instance.gunArm.position.y -.4f, PlayerController.instance.gunArm.position.z);
                        Debug.Log("MAM MINIGUN");
                    } else
                    {
                        gunClone.transform.position = PlayerController.instance.gunArm.position;
                        Debug.Log("MAM INU ZBRAN");
                    }
                    gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    gunClone.transform.localScale = Vector3.one;

                    PlayerController.instance.availableGuns.Add(gunClone);
                    PlayerController.instance.currentGun = PlayerController.instance.availableGuns.Count- 1;
                    PlayerController.instance.switchGun();

                    Destroy(gameObject);
                }


            } else if(isCoin == true)
            {
                LevelManager.instance.getCoins(coinValue);
                Destroy(gameObject);

                AudioManager.instance.PlaySFX(6);
            }

        }
    }
}
