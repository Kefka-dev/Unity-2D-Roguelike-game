using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float deceleration = 5f;

    public float lifetime = 3f;

    public SpriteRenderer SR;
    public float fadeSpeed = 9;

    private Vector3 moveDirectrion;
    

    // Start is called before the first frame update
    void Start()
    {
        moveDirectrion.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirectrion.y = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirectrion * Time.deltaTime;
        moveDirectrion = Vector3.Lerp(moveDirectrion, Vector3.zero, deceleration * Time.deltaTime);

        lifetime -= Time.deltaTime;
        if(lifetime < 0)
        {
            SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, Mathf.MoveTowards(SR.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (SR.color.a == 0f)
            {
                Destroy(gameObject);
            }

        }
    }
}
