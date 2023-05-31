using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bone : MonoBehaviour
{
    public delegate void BoneCollect();
    public static BoneCollect onCollect;
    public string hint;

    string id;
    private void Awake()
    {
        CheckStatus();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<AudioSource>().Play();
            Memory.boneMemory.Add(id);
            if(Memory.foundHints.Contains(hint) == false)
            {
                Memory.foundHints.Add(hint);
            }
            Memory.bonesCollected++;
            Debug.Log("Bones: " + Memory.bonesCollected);
            if (onCollect != null)
            {
                onCollect.Invoke();
            }
        }

    }
    void CheckStatus()
    {
        id = SceneManager.GetActiveScene().name + "Bone" + transform.position;
        if (Memory.boneMemory.Contains(id))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        CheckStatus();
    }
}
