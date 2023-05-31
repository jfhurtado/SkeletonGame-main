using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float selfDestructTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DestroySelf()
    {
        Destruct();
    }

    IEnumerator Destruct()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }
}
