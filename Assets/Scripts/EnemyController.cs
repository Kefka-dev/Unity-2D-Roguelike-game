using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    public Animator anim;
    public int health;
    public int giveTouchDamage;
    public float rangeToChasePlayer;
    public GameObject[] deatchSplatters;
    public GameObject hitEffect;
    
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public float shootRange;

    public SpriteRenderer theBody;

    private Vector3 moveDirection;
    private float fireCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
                anim.SetBool("isMoving", true);
            }
            else
            {
                moveDirection = Vector3.zero;
                anim.SetBool("isMoving", false);
            }

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
