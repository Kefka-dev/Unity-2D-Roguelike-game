using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bullet;
    //public Transform[] firePoints;
    public Transform firePoint;
    public int firepointID;

    public float timeBetweenShots;
    private float shotCounter;

    public bool Revolver, Minigun, Shotgun;

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
                if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Revolver == false))
                {
                    /*if(Shotgun == true)
                    {
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        Instantiate(bullet, firePoint2.position, firePoint2.rotation);
                        Instantiate(bullet, firePoint3.position, firePoint3.rotation);
                        AudioManager.instance.PlaySFX(13);
                        shotCounter = timeBetweenShots;
                    }*/
                    //else
                    //{
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        AudioManager.instance.PlaySFX(13);
                        shotCounter = timeBetweenShots;
                    //}
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
