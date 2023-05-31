using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    AudioSource source;
    public AudioClip attack;
    public AudioClip pain;
    public AudioClip playerChaseAlert;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayAttack()
    {
        source.PlayOneShot(attack);
    }
    public void PlayPain()
    {
        source.PlayOneShot(pain);
    }

    public void PlayChaseAlert()
    {
        source.PlayOneShot(playerChaseAlert);
    }
}
