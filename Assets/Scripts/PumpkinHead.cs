using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PumpkinHead : MonoBehaviour
{
    private TutorialController tutorialController;

    string[] hints = new string[13];
    string hint;
    int nextHint;
    TMP_Text text;
    bool inRange = false;
    int coinsForHint = 10;
    LookAt look;
    Dictionary<int, string> hintCodes = new Dictionary<int, string>();
    Animator anim;
    Transform player;
    float lookWeight;
    public Transform head;
    bool isTalking;
    bool canReceiveEnterClick;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        text = transform.Find("Text").GetComponent<TMP_Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        text.text = "";
        hints[0] = '"' + "Have you checked around the Fountain?" + '"' + "\n(Press Enter)";
        hints[1] = '"' + "Some walls are breakable" + '"' + "\n(Press Enter)";
        hints[2] = '"' + "If you need bones, just sayin', tombs might be a place to look" + '"' + "\n(Press Enter)";
        hints[3] = '"' + "The toxic glow of the lake is beautiful this time of year" + '"' + "\n(Press Enter)";
        hints[4] = '"' + "You want help? Try praying Haha!" + '"' + "\n(Press Enter)";
        hints[5] = '"' + "I hear there's a secret way to open the gate" + '"' + "\n(Press Enter)";
        hints[6] = '"' + "You're going to need to run AWFULLY fast to get through the gate" + '"' + "\n(Press Enter)";
        hints[7] = '"' + "Try not to trip chasing through foggy areas" + '"' + "\n(Press Enter)";
        hints[8] = '"' + "Some spiders are fast, you may need some help catching them" + '"' + "\n(Press Enter)";
        hints[9] = '"' + "There may be something buried in the mountain" + '"' + "\n(Press Enter)";
        hints[10] = '"' + "You'll need one HECK of a running jump to get on the roof" + '"' + "\n(Press Enter)";
        hints[11] = '"' + "You can have both super run AND jump" + '"' + "\n(Press Enter)";
        hints[12] = '"' + "Go ask my brother, why don't ya" + '"' + "\n(Press Enter)";
        hintCodes[0] = "FountainBone";
        hintCodes[1] = "Breakable";
        hintCodes[2] = "TombBone";
        hintCodes[3] = "LakeBone";
        hintCodes[4] = "ChurchBone";
        hintCodes[5] = "Lever";
        hintCodes[6] = "Gate";
        hintCodes[7] = "SlowSpiderBone";
        hintCodes[8] = "FastSpiderBone";
        hintCodes[9] = "MountainBone";
        hintCodes[10] = "RoofBone";
        hintCodes[11] = "RoofBone";
        hintCodes[12] = "PurchaseBone";
        nextHint = Memory.nextHint;

        tutorialController = GameObject.FindGameObjectWithTag("TutorialScreen").GetComponent<TutorialController>();
        TutorialController.onCompletePumpkinHeadTutorial += FinishTextsDisplay;
        TutorialController.onCompletePumpkinHeadHint += FinishTextsDisplay;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger Enter");
            inRange = true;
            look = other.GetComponent<LookAt>();
            if (Memory.metPumpkinHead == true)
            {
                CheckPurchaseStatus();
            }
            else
            {
                Introduce();
            }
        } 
    }
    void CheckPurchaseStatus()
    {
        Debug.Log("Check Status");
        if (nextHint < hints.Length)
        {
            if (Memory.coinsCollected >= coinsForHint)
            {
                text.text = "Trade " + coinsForHint + " coins for hint. \nPress Enter";
            }
            else
            {
                text.text = "Need " + coinsForHint + " coins for hint";
            }
        }
        else
        {
            text.text = "No More Hints";
        }
    }
    
    void Introduce()
    {
        anim.SetTrigger("Talk");
        isTalking = true;
        text.text = "";
        lookWeight = 1.0f;
        //anim.SetLookAtWeight(lookWeight);
        //anim.SetLookAtPosition(player.position);
        look.LookAtTarget(head);
        tutorialController.startPumpkinHeadIntro();
    }

    public void FinishTextsDisplay()
    {
        
        CheckPurchaseStatus();
        anim.SetTrigger("Idle");
        look.Resume();
        StartCoroutine(TalkingFinishDelay());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inRange = false;
            text.text = "";
        }
    }
    void BuyHint()
    {
        Memory.coinsCollected -= coinsForHint;
        isTalking = true;
        text.text = "";
        anim.SetTrigger("Talk");
        look.LookAtTarget(transform);
        List<string> hints = new List<string>()
        {
            GetHint()
        };
        tutorialController.startPumpkinHeadHint(hints);
    }

    public string GetHint()
    {
        if (nextHint < hints.Length)
        {
            while (Memory.foundHints.Contains(hintCodes[nextHint]))
            {
                nextHint++;
            }
            hint = hints[nextHint];
            nextHint++;
            return hint;
        }
        else
        {
            return null;
        }
    }

    private void Update()
    {
        if (inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.Return) && Memory.coinsCollected >= coinsForHint && !isTalking && nextHint < hints.Length)
            {
                BuyHint();
            }
        }
    }

    IEnumerator TalkingFinishDelay()
    {
        yield return new WaitForSeconds(1f);
        Memory.metPumpkinHead = true;
        isTalking = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        Memory.nextHint = nextHint;
        TutorialController.onCompletePumpkinHeadTutorial -= FinishTextsDisplay;
        TutorialController.onCompletePumpkinHeadHint -= FinishTextsDisplay;
    }

    private void enterThrottle()
    {

    }
}
