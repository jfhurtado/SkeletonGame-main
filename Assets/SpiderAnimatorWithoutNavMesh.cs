using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimatorWithoutNavMesh : MonoBehaviour
{
    private Animator spiderAnimator;
    // Start is called before the first frame update
    void Start()
    {
        spiderAnimator = GetComponent<Animator>();
        spiderAnimator.SetBool("walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.position + new Vector3(0, 0, -0.01f);
    }
}
