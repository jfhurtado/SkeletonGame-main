using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    public delegate void MainCharacterNear();
    public static MainCharacterNear onNear;

    public ViewAldrich view;

    private TutorialController tutorialController;
    private GameObject lurch;
    private float triggerThreadhold = 6f;
    Vector3 lurchPostition;
    // Start is called before the first frame update
    void Start()
    {
        lurch = GameObject.FindGameObjectWithTag("Player");
        tutorialController = GameObject.FindGameObjectWithTag("TutorialScreen").GetComponent<TutorialController>();
        if (Memory.friendHasBody == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lurch.transform.position, transform.position) <= triggerThreadhold) {
            if (onNear != null)
            {
                onNear.Invoke();
            }
            if(Memory.bonesCollected >= Totals.totalBones && Memory.friendHasBody == false)
            {
                SetBone();
                Memory.friendHasBody = true;
            }
        }
        lurchPostition = new Vector3(lurch.transform.position.x, transform.position.y, lurch.transform.position.z);
        transform.LookAt(lurchPostition);
    }
    void ActivateBody()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).GetComponent<ParticleSystem>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bone")
        {
            other.gameObject.SetActive(false);
            ActivateBody();
            StartCoroutine(cutsceneOffDelay());
            tutorialController.VictoryMessage();
        }
    }
    void SetBone()
    {
        view.ActivateCam();
        Transform bone = transform.Find("Bone");
        bone.position = lurch.transform.position;
        bone.parent = null;
        bone.gameObject.SetActive(true);
        lurch.GetComponent<Animator>().SetFloat("speed", 0);
    }

    IEnumerator cutsceneOffDelay()
    {
        yield return new WaitForSeconds(2);
        view.DeactivateCam();
    }

}
