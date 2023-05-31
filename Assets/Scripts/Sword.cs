using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    int damage;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    public int GetDamage()
    {
        return damage;
    }
    public void SetDamage(int d)
    {
        damage = d;
    }
    public void DisableCollider()
    {
        col.enabled = false;
    }
    public void EnableCollider()
    {
        col.enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        col.enabled = false;
        // player coming in front of spider 
        if (collision.gameObject.tag == "HeartSpider")
        {
            col.enabled = true;
            SpiderAnimationController spiderAnimController = collision.gameObject.GetComponent<SpiderAnimationController>();
            spiderAnimController.receiveAttackFromSkeleton("level2");
        }
       
        else if (collision.gameObject.tag == "BoneSpider")
        {
            Debug.Log("player is in front of spider ");
            col.enabled = true;
            BoneSpiderAI boneSpiderAI = collision.gameObject.GetComponent<BoneSpiderAI>();
            boneSpiderAI.receiveAttackFromSkeleton("level2");
        }
        else if(collision.gameObject.GetComponent<Lever>() != null)
        {
            col.enabled = true;
            collision.gameObject.GetComponent<Lever>().Hit();
        }
        else
        {
            col.enabled = false;
        }

    }
}
