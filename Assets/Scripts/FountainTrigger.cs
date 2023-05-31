using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainTrigger : MonoBehaviour
{
    public AudioClip splashIn;
    public AudioClip splashOut;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerAudioManager>() != null)
        {
            source.PlayOneShot(splashIn);
            other.GetComponent<PlayerAudioManager>().inWater = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerAudioManager>() != null)
        {
            source.PlayOneShot(splashOut);
            other.GetComponent<PlayerAudioManager>().inWater = false;
        }
    }
}
