using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoinBreakable : MonoBehaviour
{
    string id;

    // Start is called before the first frame update
    void Awake()
    {
        id = SceneManager.GetActiveScene().name + "Breakable" + transform.position;
        if (Memory.smashMemory.Contains(id))
        {
            DropObject();
            gameObject.SetActive(false);
        }
    }
    public void OnSmash()
    {
        Memory.smashMemory.Add(id);
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        DropObject();
        StartCoroutine(DestroyDelay());
    }
    void DropObject()
    {
        if (tag != "Untagged")
        {
            GameObject drop = transform.GetChild(1).gameObject;
            drop.transform.SetParent(null);
            drop.SetActive(true);
        }
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
