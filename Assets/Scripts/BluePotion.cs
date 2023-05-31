using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePotion : MonoBehaviour
{
    public delegate void BluePotionsCollect();
    public static BluePotionsCollect onCollect;
    public delegate void BluePotionNear();
    public static BluePotionNear onNear;

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

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<SkeletonMovement>().SetSuperSpeed();
            GetComponent<AudioSource>().Play();
            if (onCollect != null)
            {
                onCollect.Invoke();
            }
        }
    }
}
