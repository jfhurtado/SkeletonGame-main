using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Call skeleton gameobject
    public GameObject target;

    // create vector offset to ami camera at skeleton
    public Vector3 offset;

    //add bool if we want to use offset values directly
    public bool useOffsetValues;

    //Camera rotation
    public float rotateSpeed;

    public GameObject pivot;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!useOffsetValues)
        {
            offset = target.transform.position - transform.position;
        }

        Debug.Log(pivot.transform.position);

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Memory.isPause) return;
        //Get tge X position of the mouse and rotate target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        // Get Y position of the mouse and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.transform.Rotate(-vertical, 0, 0);

        //Move the camera based on the current rotation of the target & the original offset
        float desiredYAngle = target.transform.eulerAngles.y;

        float desiredXAngle = pivot.transform.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.transform.position - (rotation * offset);

        if(transform.position.y < target.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        }


        // use lookat function to always follow the gameobject
        //transform.LookAt(target.transform);
    }
}
