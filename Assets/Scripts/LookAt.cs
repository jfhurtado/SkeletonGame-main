using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    SkeletonMovement playerController;
    Transform gameCam;
    public Transform cameraLookPos;
    GameObject freeLook;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<SkeletonMovement>();
        anim = GetComponent<Animator>();
        gameCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        freeLook = GameObject.FindGameObjectWithTag("FreeLook");
    }


    public void LookAtTarget(Transform target)
    {
        playerController.enabled = false;
        anim.SetFloat("speed", 0);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        freeLook.SetActive(false);
        gameCam.position = cameraLookPos.position;
        gameCam.LookAt(target);
    }
    public void Resume()
    {
        playerController.enabled = true;
        freeLook.SetActive(true);
    }
    

}
