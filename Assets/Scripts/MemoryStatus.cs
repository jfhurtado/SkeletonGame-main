using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryStatus : MonoBehaviour
{
    string identifier;
    public enum Memorable {Coin, Bone, Smashable, Health, Enemy};

    public Memorable mem;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Awake()
    {
        startPosition = transform.position;
        identifier = SceneManager.GetActiveScene().name + mem + transform.position;
    }

    public void AddToMemory()
    {
        if (mem.ToString() == "Coin")
        {
            Memory.coinMemory.Add(identifier);
        }
        else if(mem.ToString() == "Bone")
        {
            Memory.boneMemory.Add(identifier);
        }
        if (mem.ToString() == "Smashable")
        {
            Memory.smashMemory.Add(identifier);
        }
        else if (mem.ToString() == "Health")
        {
            Memory.activeHealth.Add((identifier, transform.position));
        }
        else if (mem.ToString() == "Enemy")
        {
            Memory.enemyMemory.Add(identifier);
        }
    }

    void SetActivation()
    {
        if (mem.ToString() == "Coin")
        {
            if (Memory.coinMemory.Contains(identifier))
            {
                gameObject.SetActive(false);
            }
        }
        else if (mem.ToString() == "Bone")
        {
            if (Memory.boneMemory.Contains(identifier))
            {
                gameObject.SetActive(false);
            }
        }
        else if (mem.ToString() == "Smashable")
        {
            if (Memory.smashMemory.Contains(identifier))
            {
                gameObject.SetActive(false);
            }
        }
        else if (mem.ToString() == "Health")
        {
            foreach ((string, Vector3) t in Memory.activeHealth)
            {
                if (t.Item1 == identifier)
                {
                    GetComponent<Renderer>().enabled = true;
                    GetComponent<Collider>().enabled = true;
                    transform.position = t.Item2;
                    break;
                }
            }
        }
        else if (mem.ToString() == "Enemy")
        {
            Memory.enemyMemory.Add(identifier);
        }
    }
    
}
