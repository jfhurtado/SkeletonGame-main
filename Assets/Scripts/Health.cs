using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void TakeDamage(int damage);
    public abstract int GetHealth();
}
