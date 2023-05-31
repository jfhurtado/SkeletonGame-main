using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [HideInInspector] public bool inWater = false;
    public AudioClip footStep1;
    public AudioClip footStep2;
    public AudioClip waterStep1;
    public AudioClip waterStep2;
    public AudioClip jump;
    public AudioClip slash;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayFootStep1()
    {
        if (inWater == false)
        {
            source.PlayOneShot(footStep1);
        }
        else
        {
            source.PlayOneShot(waterStep1);
        }
    }
    public void PlayFootStep2()
    {
        if (inWater == false)
        {
            source.PlayOneShot(footStep2);
        }
        else
        {
            source.PlayOneShot(waterStep2);
        }
    }
    public void PlayJump()
    {
        source.PlayOneShot(jump);
    }
    public void PlaySlash()
    {
        source.PlayOneShot(slash);
    }

}
