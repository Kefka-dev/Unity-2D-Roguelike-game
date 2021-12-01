using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("Shotgun settings")]
    public bool Shotgun;
    public Transform firePointShotgun1, firePointShotgun2, firePointShotgun3;
    
    [Header("Variables")]
    public GameObject bullet;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;
    public bool isAutomatic, Minigun;

    public string weaponName;
    public Sprite gunUI;

    public int itemCost;
    public Sprite gunShopSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if(shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && isAutomatic == false))
                {
                    if(Shotgun == true)
                    {
                        Instantiate(bullet, firePointShotgun1.position, firePointShotgun1.rotation);
                        Instantiate(bullet, firePointShotgun2.position, firePointShotgun2.rotation);
                        Instantiate(bullet, firePointShotgun3.position, firePointShotgun3.rotation);
                        AudioManager.instance.PlaySFX(13);
                        shotCounter = timeBetweenShots;
                    }
                    else
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        AudioManager.instance.PlaySFX(13);
                        shotCounter = timeBetweenShots;
                    }
                }
                
                /*if (Input.GetMouseButton(0))
                {

                    if (shotCounter <= 0)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        AudioManager.instance.PlaySFX(13);
                        shotCounter = timeBetweenShots;
                    }
                }*/
            }
        }
    }
}
