using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAttackColliderScript : MonoBehaviour
{
    public  GameObject parent ;

    void OnTriggerEnter(Collider collider)
    {
        SpiderAnimationController parentSpiderScriptObject = parent.GetComponent<SpiderAnimationController>();

        if (!parentSpiderScriptObject.isDead && collider.attachedRigidbody != null && collider.gameObject.tag == "Player")
        {
            Debug.Log("SpiderAttackColliderScript : spider should attack ");
            parentSpiderScriptObject.updateAnimation(SpiderAnimationController.ATTACK, true);                           
        }
    }

    void OnTriggerExit(Collider collider)
    {
        SpiderAnimationController parentSpiderScriptObject = parent.GetComponent<SpiderAnimationController>();

        if (!parentSpiderScriptObject.isDead && collider.attachedRigidbody != null && collider.gameObject.tag == "Player")
        {
            parentSpiderScriptObject.updateAnimation(SpiderAnimationController.WALK, true);
        }
    }
}
