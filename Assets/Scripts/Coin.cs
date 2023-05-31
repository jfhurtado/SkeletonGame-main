using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public delegate void CoinCollect();
    public static CoinCollect onCollect;
    string id;
    public AudioClip clip;
    private void Awake()
    {
        id = SceneManager.GetActiveScene().name + "Coin" + transform.position;
        if (Memory.coinMemory.Contains(id))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;
            Memory.coinMemory.Add(id);
            Memory.coinsCollected++;
            if (onCollect != null)
            {
                onCollect.Invoke();
            }
        }

    }
    




}
