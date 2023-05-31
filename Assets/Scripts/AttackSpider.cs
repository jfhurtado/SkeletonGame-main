using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("entering player collision");
        // player coming in front of spider 
        if (collision.gameObject.tag == "HeartSpider" )
        {
            Debug.Log("player is in front of spider ");

            SpiderAnimationController spiderAnimController = collision.gameObject.GetComponent<SpiderAnimationController>();
            spiderAnimController.receiveAttackFromSkeleton("level2");
        }

    }
}
