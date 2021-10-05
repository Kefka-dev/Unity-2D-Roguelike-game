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
    
    public GameObject bullet;
    public Transform firePoint;
  
    public float timeBetweenShots;
    
    public SpriteRenderer bodySR;

    public float dashSpeed = 8f, dashLenght = .5f, dashCooldown = 1f, dashInvincibility = .5f;

    private Vector2 moveInput;
    private Camera theCam;
    private float shotCounter;

    private float activeMovespeed;
    private float dashDuration, dashCooldownCounter;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;

        activeMovespeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
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

        shotCounter -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shotCounter <= 0)
        {

            Instantiate(bullet, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {

            if (shotCounter <= 0)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);

                shotCounter = timeBetweenShots;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (dashCooldownCounter <= 0 && dashDuration <= 0)
            {
                activeMovespeed = dashSpeed;
                dashDuration = dashLenght;

                anim.SetTrigger("dash");
            }
        }

        if (dashDuration > 0)
        {
            dashDuration -= Time.deltaTime;
            if(dashDuration <= 0)
            {
                activeMovespeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
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
        } else
        {
            anim.SetBool("isMoving", false);
        }


    }
}
