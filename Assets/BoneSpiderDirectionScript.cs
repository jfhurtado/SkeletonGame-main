using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpiderDirectionScript : MonoBehaviour
{
    public GameObject parent;

    void OnTriggerEnter(Collider collider)
    {
        BoneSpiderAI parentBoneSpider = parent.GetComponent<BoneSpiderAI>();

        if (!parentBoneSpider.isDead && collider.attachedRigidbody != null && collider.gameObject.tag == "Player")
        {
            // if it is founding the player in way change the direction and way point 
            if (parentBoneSpider.direction == parentBoneSpider.CLOCKWISE)
            {
                parentBoneSpider.direction = "anticlockwise";
                Debug.Log($"changing direction to {parentBoneSpider.direction}");
                parentBoneSpider.moveAntiClockWise();
                
            }
            else
            {
                parentBoneSpider.direction = parentBoneSpider.CLOCKWISE;
                Debug.Log($"changing direction to {parentBoneSpider.direction}");
                parentBoneSpider.moveClockWise();
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
    }
}
