using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public float moveSpeed;
    
    public Rigidbody2D theRB;
    
    public Transform gunArm;
  
    public Animator anim;
    
    /*public GameObject bullet;
    public Transform firePoint;
  
    public float timeBetweenShots;
    private float shotCounter;*/

    public SpriteRenderer bodySR;
    

    public float dashSpeed = 8f, dashLenght = .5f, dashCooldown = 1f, dashInvincibility = .5f;

    [HideInInspector]
    public bool canMove = true;

    private Vector2 moveInput;
    private Camera theCam;
    

    private float activeMovespeed;
    private float dashDuration, dashCooldownCounter;

    public List<Gun> availableGuns = new List<Gun>();
    [HideInInspector]
    public int currentGun;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;

        activeMovespeed = moveSpeed;

        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunDescription.text = availableGuns[currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !LevelManager.instance.isPaused)
        {



            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();
            //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);

            theRB.velocity = moveInput * activeMovespeed;

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);


            //otocenie hraca a zbrane tak aby mierenie zbranou vyzeralo normalne
            if (mousePos.x < screenPoint.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                gunArm.transform.localScale = new Vector3(-0.75f, -0.75f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                gunArm.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }

            //otacanie ruk
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            /*shotCounter -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && shotCounter <= 0)
            {

                Instantiate(bullet, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(13);
                shotCounter = timeBetweenShots;
            }

            if (Input.GetMouseButton(0))
            {

                if (shotCounter <= 0)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(13);
                    shotCounter = timeBetweenShots;
                }
            }*/
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(availableGuns.Count > 0)
                {
                    currentGun++;
                    if (currentGun > availableGuns.Count - 1)
                    {
                        currentGun = 0;
                    }
                    switchGun();
                }
                else
                {
                    Debug.Log("Player has no guns!");
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {

                if (dashCooldownCounter <= 0 && dashDuration <= 0)
                {
                    AudioManager.instance.PlaySFX(9);
                    activeMovespeed = dashSpeed;
                    dashDuration = dashLenght;
                    Physics2D.IgnoreLayerCollision(8, 10, true);

                    PlayerHealthController.instance.BeInvincible(dashInvincibility);
                    anim.SetTrigger("dash");
                }
            }

            if (dashDuration > 0)
            {
                dashDuration -= Time.deltaTime;
                if (dashDuration <= 0)
                {
                    activeMovespeed = moveSpeed;
                    dashCooldownCounter = dashCooldown;
                    Physics2D.IgnoreLayerCollision(8, 10, false);
                }
            }

            if (dashCooldownCounter > 0)
            {
                dashCooldownCounter -= Time.deltaTime;
            }


            //zistovanie ci sa hrac hybe na to aby sme vedeli ktora animacia ma prebiehat 
            if (moveInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

        } else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
    }

    public void switchGun()
    {
        foreach(Gun theGun in availableGuns)
        {
            theGun.gameObject.SetActive(false);

        }

        availableGuns[currentGun].gameObject.SetActive(true);

        UIController.instance.currentGun.sprite = availableGuns[currentGun].gunUI;
        UIController.instance.gunDescription.text = availableGuns[currentGun].weaponName;
    }
}
