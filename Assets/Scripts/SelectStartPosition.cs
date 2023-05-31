using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStartPosition : MonoBehaviour
{
    
    string previousScene;
    public Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        
        previousScene = PreviousScene.previousScene;
        if (previousScene != null && !Object.ReferenceEquals(transform.Find(previousScene), null))
        {
            player.position = transform.Find(previousScene).position;
            player.eulerAngles = transform.Find(previousScene).eulerAngles;
        }

    }

}
