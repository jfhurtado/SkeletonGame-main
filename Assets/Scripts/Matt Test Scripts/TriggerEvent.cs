using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerEvent : MonoBehaviour
{
    public UnityEvent SpaceTrigger;
    public UnityEvent pTrigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceTrigger.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            pTrigger.Invoke();
        }
    }
}
