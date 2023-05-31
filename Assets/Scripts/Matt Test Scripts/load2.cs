using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class load2 : MonoBehaviour
{
    public int otherScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Collision");
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(otherScene));
    }


}
