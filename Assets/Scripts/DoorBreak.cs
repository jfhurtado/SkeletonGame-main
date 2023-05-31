using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(Rigidbody))]
public class DoorBreak : MonoBehaviour
{
    float explosionForce = 300;
    float explosionRadius = 2.5f;
    Rigidbody rb;
    string id;

    // Start is called before the first frame update
    void Awake()
    {
        id = SceneManager.GetActiveScene().name + "Breakable" + transform.position;
        if (Memory.smashMemory.Contains(id))
        {
            gameObject.SetActive(false);
        }
    }

    public void Break()
    {
        Memory.smashMemory.Add(id);
        if(Memory.foundHints.Contains("Breakable") == false)
        {
            Memory.foundHints.Add("Breakable");
        }
        GetComponent<AudioSource>().Play();
        GetComponent<Collider>().enabled = false;
        Vector3 explosionPosition = transform.position + transform.forward * 1f;
        foreach (Transform child in transform)
        {
            if(child.GetComponent<Rigidbody>() != null)
            {
                rb = child.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
            else if(child.GetComponent<ParticleSystem>() != null)
            {
                child.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                child.gameObject.SetActive(false);
            }
            
        }
        StartCoroutine(SelfDestruct());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Sword>() != null)
        {
            Break();
        }
    }



}
