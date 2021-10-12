using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakables : MonoBehaviour
{

    public GameObject[] brokenPieces;
    public int maxPieces = 4;

    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropChance;

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
        if(other.tag == "PlayerBullet")
        {
            Destroy(gameObject);

            int piecesToDrop = Random.Range(1, maxPieces);
            
            
            for (int i = 0; i<piecesToDrop; i++)
            {
                int randomPiece = Random.Range(0, brokenPieces.Length);
                Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
            }

            if (shouldDropItem == true)
            {
                float dropChance = Random.Range(0f, 100f);

                if(dropChance < itemDropChance)
                {
                    int randomItem = Random.Range(0, itemsToDrop.Length);

                    Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                }
            }
        }
    }
}
