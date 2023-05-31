using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAldrich : MonoBehaviour
{
    public Camera aldrichCam;
    public Camera gameCam;
    public SkeletonMovement playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ActivateCam()
    {
        gameCam.gameObject.SetActive(false);
        aldrichCam.gameObject.SetActive(true);
        playerController.enabled = false;
    }
    public void DeactivateCam()
    {
        aldrichCam.gameObject.SetActive(false);
        gameCam.gameObject.SetActive(true);
        playerController.enabled = true;
    }
}
