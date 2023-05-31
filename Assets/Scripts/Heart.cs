using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Heart : MonoBehaviour
{
    public delegate void HeartCollect();
    public static HeartCollect onCollect;

    Vector3 previousPosition1 = new Vector3(-1,-1,-1);
    Vector3 previousPosition2 = new Vector3(-2, -2, -2);
    Vector3 currentPosition = new Vector3(0, 0, 0);

    string id;
    // Start is called before the first frame update
    void Start()
    {
        bool inList = false;
        foreach ((string, Vector3) health in Memory.activeHealth)
        {
            if (health.Item1 == id)
            {
                inList = true;
                break;
            }
        }
        if (inList == false)
        {
            StartCoroutine(AddToMemory());
        }
    }
    public void SetID(string identifier)
    {
        id = identifier;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect();
            if (onCollect != null)
            {
                onCollect.Invoke();
            }
        }
    }
    void Collect()
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        StopAllCoroutines();
        if (id != null)
        {
            if (Memory.activeHealth.Contains((id, transform.position)))
            {
                Memory.activeHealth.Remove((id, transform.position));
            }
        }
        //Destroy(gameObject);
    }

    IEnumerator AddToMemory()
    {
        while(currentPosition != previousPosition2)
        {
            previousPosition2 = previousPosition1;
            previousPosition1 = currentPosition;
            currentPosition = transform.position;

            yield return new WaitForEndOfFrame();
        }
        id = SceneManager.GetActiveScene().name + "Heart" + currentPosition;
        Memory.activeHealth.Add((id, currentPosition));
        
    }


}
