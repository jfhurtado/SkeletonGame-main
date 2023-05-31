using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPreviousScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PreviousScene.previousScene = SceneManager.GetActiveScene().name;
    }


}
