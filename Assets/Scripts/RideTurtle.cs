using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideTurtle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Turtle");
        other.transform.SetParent(transform);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Fell Off");
        other.transform.SetParent(null);
    }

}
