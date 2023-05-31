using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Lever : MonoBehaviour
{
    [SerializeField] UnityEvent leverOn;
    [SerializeField] UnityEvent leverOff;
    public FloatEvent sendTimeLimit;
    bool on = false;
    public bool timedOff;
    public float timeLimit;
    Animator anim;
    bool canBeHit = true;
    Coroutine delay;
    Coroutine timer;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Sword>() != null && Memory.gateOpen == false && canBeHit == true)
        {

        }
    }

    public void Hit()
    {
        if (Memory.gateOpen == false && canBeHit == true)
        {
            on = !on;
            canBeHit = false;
            delay = StartCoroutine(Delay());

            if (on == true)
            {
                anim.SetBool("Open", true);                
            }
            else
            {
                StopCoroutine(timer);
                anim.SetBool("Open", false);
            }
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeLimit);
        anim.SetBool("Open", false);
        on = false;
        leverOff.Invoke();
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        if (on == true)
        {
            leverOn.Invoke();
            if (timedOff == true)
            {
                sendTimeLimit.Invoke(timeLimit);
                timer = StartCoroutine(Timer());
            }
        }
        else
        {
            leverOff.Invoke();
            if (timedOff == true)
            {
                StopCoroutine(timer);
            }
        }
        yield return new WaitForSeconds(1);
        canBeHit = true;
    }

}
