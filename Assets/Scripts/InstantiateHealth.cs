using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHealth : MonoBehaviour
{
    public GameObject health;
    GameObject h;
    // Start is called before the first frame update
    void Awake()
    {
        foreach ((string, Vector3) ah in Memory.activeHealth)
        {
            h = Instantiate(health, ah.Item2, Quaternion.EulerAngles(-90, 0, 0));
            h.GetComponent<Heart>().SetID(ah.Item1);
        }
    }

}
