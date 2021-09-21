using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 8f;
    public Rigidbody2D theRB;
    public int giveDamage = 20;

    // Start is called before the first frame update
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
        Destroy(gameObject);

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(giveDamage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
