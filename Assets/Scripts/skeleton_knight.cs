using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeleton_knight : MonoBehaviour
{
    // Define speed
    [SerializeField] private float speed;

    private Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool forward = Input.GetKey("w");
        bool run = Input.GetKey("left shift");
        // walking and running through speed
        


        //Boolean
        if (!isWalking && forward)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forward)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (forward && run))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!forward || !run))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
