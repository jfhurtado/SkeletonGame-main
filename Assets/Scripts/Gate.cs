using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    Animator anim;
    public Transform hinge1;
    public Transform hinge2;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        if (Memory.gateOpen == true)
        {
            SetOpen();
        }
    }

    public void Close()
    {
        anim.SetBool("Open", false);
        GetComponent<AudioSource>().Play();
    }
    public void Open()
    {
        anim.SetBool("Open", true);
        GetComponent<AudioSource>().Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Memory.gateOpen = true;
            if (Memory.foundHints.Contains("Gate") == false)
            {
                Memory.foundHints.Add("Gate");
            }
        }
    }
    void SetOpen()
    {
        anim.enabled = false;
        GetComponent<Collider>().isTrigger = true;
        hinge1.localEulerAngles = new Vector3(0, 90, 0);
        hinge2.localEulerAngles = new Vector3(0, 270, 0);
    }
}
