using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float moveSpped;

    public Transform target;

    public Camera mainCamera;

    public bool isBossRoom;
    private bool mapScaleUp;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isBossRoom == true)
        {
            target = PlayerController.instance.transform;
        }
        mapScaleUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpped * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isBossRoom == false)
        {
            Debug.Log("MapOpened");
            if (mapScaleUp == false)
            {
                UIController.instance.mapScaleUp();
                mapScaleUp = true;
            }
            else
            {
                UIController.instance.mapScaleDown();
                mapScaleUp = false;
            }

        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    } 
}
