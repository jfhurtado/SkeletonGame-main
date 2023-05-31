using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotion : MonoBehaviour
{
    public delegate void GreenPotionsCollect();
    public static GreenPotionsCollect onCollect;
    public delegate void GreenPotionNear();
    public static GreenPotionNear onNear;

    private GameObject lurch;
    private float triggerThreadhold = 2f;

    private void Start()
    {
        lurch = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(lurch.transform.position, transform.position) <= triggerThreadhold && onNear != null)
        {
            onNear.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<SkeletonMovement>().SetSuperJump();
            GetComponent<AudioSource>().Play();
            if (onCollect != null)
            {
                onCollect.Invoke();
            }
        }
    }
}
