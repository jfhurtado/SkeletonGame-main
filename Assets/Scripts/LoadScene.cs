using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int sceneToLoad;
    // Start is called before the first frame update
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Load(sceneToLoad);
        }
    }
    public void Load(int level)
    {
        SceneManager.LoadScene(level);
    }

}
