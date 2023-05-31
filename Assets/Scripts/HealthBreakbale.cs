using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthBreakbale : MonoBehaviour
{
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

    public void OnSmash()
    {
        Memory.smashMemory.Add(id);
        GetComponent<AudioSource>().Play();
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GameObject drop = transform.GetChild(1).gameObject;
        drop.transform.SetParent(null);
        drop.SetActive(true);
        StartCoroutine(DestroyDelay());
    }
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(.75f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Sword>() != null)
        {
            OnSmash();
        }
    }
}
