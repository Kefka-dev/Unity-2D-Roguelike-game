using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;

    //chasing enemy
    public bool shouldChase;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    //runaway enemy
    public bool shouldRunAway;
    public float runawayRange;

    //wanderring enemy
    public bool shouldWander;
    public float wanderLenght, pauseLenght;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    public Animator anim;

    public int health;

    public GameObject[] deatchSplatters;
    public GameObject hitEffect;

    public bool shouldShoot;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;


    public int giveTouchDamage;

    public float shootRange;

    public SpriteRenderer theBody;

    // Start is called before the first frame update
    void Start()
    {
        if (shouldWander)
        {
            pauseCounter = Random.Range(pauseLenght * .75f, pauseLenght * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection = Vector3.zero;
            anim.SetBool("isMoving", false);

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChase)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                anim.SetBool("isMoving", true);
            } else
            {
                if (shouldWander)
                {
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move enemy
                        moveDirection = wanderDirection;

                        if(wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLenght * .75f, pauseLenght * 1.25f);
                        }

                    }

                    if(pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime;
                        if(pauseCounter <= 0)
                        {
                            wanderCounter = Random.Range(wanderLenght * .75f, wanderLenght * 1.25f);

                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                            anim.SetBool("isMoving", true);
                        }
                    }

                }
            }

            if(shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runawayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
                anim.SetBool("isMoving", true);
            }

            /*else
            {
                moveDirection = Vector3.zero;
                anim.SetBool("isMoving", false);
            }*/

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;

            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        } else
        {
            theRB.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //other.GetComponent<EnemyController>().DamageEnemy(giveDamage);
            other.GetComponent<PlayerHealthController>().DamagePlayer();
        }
        
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        AudioManager.instance.PlaySFX(3);

        Instantiate(hitEffect, transform.position, transform.rotation);
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(2);

            int selectedSplatter = Random.Range(0, deatchSplatters.Length);
            int rotation = Random.Range(0, 4);

            Instantiate(deatchSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90));
        }
    }

}
