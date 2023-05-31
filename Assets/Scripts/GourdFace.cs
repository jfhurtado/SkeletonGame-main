using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GourdFace : MonoBehaviour
{
    private TutorialController tutorialController;

    bool inRange = false;
    int coinsForBone = 50;
    LookAt look;
    Animator anim;
    int totalBones = 2;
    int bonesBought;
    TMP_Text text;
    public GameObject purchaseBone1;
    public GameObject purchaseBone2;
    public Transform head;
    bool isTalking = false;

    // Start is called before the first frame update
    void Awake()
    {
        bonesBought = Memory.bonesBought;
        anim = GetComponent<Animator>();
        text = transform.Find("Text").GetComponent<TMP_Text>();
        text.text = "";

        tutorialController = GameObject.FindGameObjectWithTag("TutorialScreen").GetComponent<TutorialController>();
        TutorialController.onCompleteGourdFaceTutorial += FinishTextsDisplay;
        TutorialController.onCompleteGourdFaceBone += FinishTextsDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true)
        {

            if (Input.GetKeyDown(KeyCode.Return) && Memory.coinsCollected >= coinsForBone && !isTalking && bonesBought < totalBones)
            {
                BuyBone();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = true;
            look = other.GetComponent<LookAt>();
            if (Memory.metGourdFace == true)
            {
                CheckPurchaseStatus();
            }
            else
            {
                Introduce();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            text.text = "";
        }
    }
    void CheckPurchaseStatus()
    {
        if (bonesBought < totalBones)
        {
            if (Memory.coinsCollected >= coinsForBone)
            {
                text.text = "Trade " + coinsForBone + " coins for Bone. \nPress Enter";
                //StartCoroutine(ListenForInput());
            }
            else
            {
                text.text = "Need " + coinsForBone + " coins for Bone";
            }
        }
        else
        {
            text.text = "No More Bones for sale";
        }
    }
    void Introduce()
    {
        anim.SetTrigger("Talk");
        text.text = "";
        isTalking = true;
        look.LookAtTarget(head);
        if (Memory.foundHints.Contains("PurchaseBone") == false)
        {
            Memory.foundHints.Add("PurchaseBone");
        }
        tutorialController.startGourdFaceIntro();
    }

    void BuyBone()
    {
        Memory.coinsCollected -= coinsForBone;
        bonesBought++;
        isTalking = true;
        text.text = "";
        Memory.bonesBought = bonesBought;
        if (bonesBought == 1)
        {
            purchaseBone1.SetActive(true);
            look.LookAtTarget(purchaseBone1.transform);
        }
        if (Memory.bonesBought == 2)
        {
            purchaseBone2.SetActive(true);
            look.LookAtTarget(purchaseBone2.transform);
        }
        List<string> hints = new List<string>()
        {
            "Look at the crow. Nice doin' business with ya (Press Enter)"
        };
        tutorialController.startGourdFaceBone(hints);
        anim.SetTrigger("Talk");
        StartCoroutine(TalkingFinishDelay());
    }

    public void FinishTextsDisplay()
    {
        CheckPurchaseStatus();
        anim.SetTrigger("Idle");
        look.Resume();
        StartCoroutine(TalkingFinishDelay());
        Memory.metGourdFace = true;
    }

    IEnumerator TalkingFinishDelay()
    {
        yield return new WaitForSeconds(1f);
        isTalking = false;
    }

    private void OnDestroy()
    {
        TutorialController.onCompleteGourdFaceTutorial -= FinishTextsDisplay;
        TutorialController.onCompleteGourdFaceBone -= FinishTextsDisplay;
    }
}
