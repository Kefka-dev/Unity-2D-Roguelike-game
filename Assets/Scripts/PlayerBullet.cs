using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 8f;
    public Rigidbody2D theRB;
    public int giveDamage = 20;
    public GameObject impactEffect;

    public bool ShouldBulletDecay;
    public float timeToLive;

    // Start is called before the first frame update

    private void Awake()
    {
        if (ShouldBulletDecay)
        {
            Destroy(gameObject, timeToLive);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed; //transform.right - do prava od toho ako je objekt otoceny
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(giveDamage);
        }

        if(other.tag == "Boss")
        {
            BossController.instance.TakeDamage(giveDamage);

            Instantiate(BossController.instance.hitEffect, transform.position, transform.rotation);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
