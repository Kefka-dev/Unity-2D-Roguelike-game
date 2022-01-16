using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public static BossController instance;
    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;

    private float shotCounter;
    private Vector2 moveDirection;
    public Rigidbody2D theRB;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actionCounter = actions[currentAction].actionLenght;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            
            //handle movement
            moveDirection = Vector2.zero;
            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChasePlayer == true)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                    moveDirection.Normalize();

                }

                if(actions[currentAction].moveToPoints == true)
                {
                    moveDirection = actions[currentAction].nextMovePoint.position - transform.position;
                }
            }


            theRB.velocity = moveDirection * actions[currentAction].moveSpeed;

            //handle shooting
            if(actions[currentAction].shouldShoot == true)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShots;

                    foreach(Transform t in actions[currentAction].shotPoints)
                    {
                        Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
                    }
                }
            }
        } else
        {
            currentAction++;
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }
            actionCounter = actions[currentAction].actionLenght;
        }
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLenght;

    public bool shouldMove;
    public bool shouldChasePlayer;
    public bool moveToPoints;
    public float moveSpeed;

    public Transform nextMovePoint;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShots;
    public Transform[] shotPoints;
}