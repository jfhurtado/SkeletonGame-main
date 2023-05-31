using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistMotion : MonoBehaviour
{
    public Transform path;
    int index = 0;
    Transform target;
    Vector3 direction;
    float distance;
    float closeEnough = .1f;
    float motionSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        distance = (target.position - transform.position).magnitude;
        if (distance <= closeEnough)
        {
            index++;
            GetTarget();
        }
        transform.position += direction * motionSpeed * Time.deltaTime;
    }
    void GetTarget()
    {
        target = path.GetChild(index % path.childCount);
        direction = (target.position - transform.position).normalized;
    }
}
