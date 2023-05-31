using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneLerp : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (target.position - transform.position).normalized * moveSpeed * Time.deltaTime;
    }
}
