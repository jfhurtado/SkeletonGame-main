using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents : MonoBehaviour
{
    public Transform aldrich;
    public GameObject player;
    public GameObject purchaseBone1;
    public GameObject purchaseBone2;
    // Start is called before the first frame update
    void Start()
    {
        if(Memory.bonesBought > 0)
        {
            purchaseBone1.SetActive(true);
        }
        if(Memory.bonesBought > 1)
        {
            purchaseBone2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LeverFound()
    {
        if (Memory.foundHints.Contains("Lever") == false)
        {
            Memory.foundHints.Add("Lever");
        }
    }

}
