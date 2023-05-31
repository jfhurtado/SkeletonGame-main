using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SimpleMove : MonoBehaviour
{
    float moveSpeed = 2;
    float rotSpeed = 90;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = new Vector3(Mathf.Sin(transform.localEulerAngles.y * Mathf.PI / 180f),0, Mathf.Cos(transform.localEulerAngles.y * Mathf.PI / 180f)) * Input.GetAxis("Vertical") * moveSpeed;

        transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotSpeed * Time.deltaTime;
    }
}
