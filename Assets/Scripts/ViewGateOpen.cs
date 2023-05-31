using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewGateOpen : MonoBehaviour
{
    public Gate gate;
    public Camera gateCam;
    public Camera playerCam;
    public SkeletonMovement skeletonMovement;
    float cutSceneTime = 2.5f;
    float openDelay = .5f;
    public void Open()
    {
        StartCoroutine(ShowOpening());
    }
    public void Close()
    {
        StartCoroutine(ShowClosing());
    }
   
    IEnumerator ShowOpening()
    {
        playerCam.gameObject.SetActive(false);
        gateCam.gameObject.SetActive(true);
        skeletonMovement.enabled = false;
        yield return new WaitForSeconds(openDelay);
        gate.Open();
        yield return new WaitForSeconds(cutSceneTime - openDelay);
        gateCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
        skeletonMovement.enabled = true;
    }
    IEnumerator ShowClosing()
    {
        playerCam.gameObject.SetActive(false);
        gateCam.gameObject.SetActive(true);
        skeletonMovement.enabled = false;
        yield return new WaitForSeconds(openDelay);
        gate.Close();
        yield return new WaitForSeconds(cutSceneTime - openDelay);
        gateCam.gameObject.SetActive(false);
        playerCam.gameObject.SetActive(true);
        skeletonMovement.enabled = true;
    }
}
