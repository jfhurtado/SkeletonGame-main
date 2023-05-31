using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInLake : MonoBehaviour
{
    public Transform respawnPoint;
    public AudioClip splash;
    public delegate void FallIntoLake();
    public static FallIntoLake onFallIntoLake;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (onFallIntoLake != null)
            {
                onFallIntoLake.Invoke();
            }
            other.GetComponent<AudioSource>().PlayOneShot(splash);
            other.GetComponent<PlayerHealth>().TakeDamage(1);
            other.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = respawnPoint.position;
            other.gameObject.transform.eulerAngles = respawnPoint.eulerAngles;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}
