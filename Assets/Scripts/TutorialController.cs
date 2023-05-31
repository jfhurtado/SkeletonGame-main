using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TutorialController : MonoBehaviour
{
    public delegate void Tutorial();
    public static Tutorial onCompletePumpkinHeadTutorial;
    public static Tutorial onCompletePumpkinHeadHint;
    public static Tutorial onCompleteGourdFaceTutorial;
    public static Tutorial onCompleteGourdFaceBone;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI generalTutoral;
    public Transform aldrich;
    public GameObject player;

    private List<string> tutorialTexts;
    private string currentTutorialType;
    private int currentSentenceIdx;
    private DateTime generalDirectivesStartTime;
    private float tutorialDisplayPeriod = 3;
    private DateTime tutorialStartTime;
    private float skippableDisplayPeriod = 0.5f;
    private List<string> boneTutorialTexts;
    private List<string> coinTutorialTexts;
    private List<string> healthTutorialTexts;
    private List<string> superSpeedTutorialTexts;
    private List<string> superJumpTutorialTexts;
    private List<string> generalTutorialTexts;
    private List<string> lakeTutorialTexts;
    private List<string> vistoryTexts;
    private List<string> pumpkinHeadntroTexts;
    private List<string> gourdFaceIntroTexts;


    // Start is called before the first frame update
    void Start()
    {
        currentTutorialType = "";
        tutorialStartTime = DateTime.Now;
        generalDirectivesStartTime = DateTime.Now;
        tutorialPanel.SetActive(false);
        tutorialTexts = new List<string>();
        currentSentenceIdx = 0;
        boneTutorialTexts = new List<string>()
        {
            '"' + "You have collected a bone! There are " + (Totals.totalBones - 1) + " more, you need to find them all for me!" +'"'
        };
        coinTutorialTexts = new List<string>()
        {
            '"' + "You have collected a coin! They should be all over the place" + '"'
        };
        healthTutorialTexts = new List<string>()
        {
            '"' + "You lost one one health! You will die if you lose all the health! To gain health, collect the heart." + '"'
        };
        superSpeedTutorialTexts = new List<string>()
        {
            '"' + "You have found the super speed potion! Drink it, you will have super speed for a brief time!" + '"'
        };
        superJumpTutorialTexts = new List<string>()
        {
            '"' + "You have found the super jump potion! Drink it, you can jump higher for a brief time!" + '"'
        };
        generalTutorialTexts = new List<string>() {
            '"' + "Ser Lurch, thank god you are here!" + '"' + "(Press Enter)",
            '"' + "The spiders, they stole my money, and even all my bones!" + '"' + "(Press Enter)",
            '"' + "Please get my bones back. You can keep the money if you find it" + '"' + "(Press Enter)",
        };
        vistoryTexts = new List<string>()
        {
            '"' + "Great job Ser Lurch! You have found all my bones, thanks you!" + '"' + "(Press Enter)"
        };
        lakeTutorialTexts = new List<string>()
        {
            '"' + "You fool! You can't swim! Don't jump into the lake!" + '"'
        };
        pumpkinHeadntroTexts = new List<string>()
        {
            '"' + "Well if it isn't Ser Lurch. I suppose you're looking for Aldrich's bones?" + '"' + "(Press Enter)",
            '"' + "I saw where they went. Bring me some of those coins and maybe I'll give ya' a hint" + '"' + "(Press Enter)"
        };
        gourdFaceIntroTexts = new List<string>()
        {
            '"' + "I already found a couple of those bones you're looking for."  + '"' + "(press Enter)",
            '"' + "For enough coins, maybe I'll give one to ya'" + '"' + "(press Enter)"
        };

        Bone.onCollect += StartBoneTutorial;
        Coin.onCollect += StartCoinTutorial;
        //PlayerHealth.onPlayerHealthChange += StartHealthTutorial;
        BluePotion.onNear += StartSuperSpeedTutorial;
        GreenPotion.onNear += StartSuperJumpTutorial;
        FriendController.onNear += StartGeneralTutorial;
        FallInLake.onFallIntoLake += StartLakeTutorial;

        if (Memory.isDoneGeneralDirectives)
        {
            generalTutoral.enabled = false;
        }
    }

    public void OnDestroy()
    {
        Bone.onCollect -= StartBoneTutorial;
        Coin.onCollect -= StartCoinTutorial;
        //PlayerHealth.onPlayerHealthChange -= StartHealthTutorial;
        BluePotion.onNear -= StartSuperSpeedTutorial;
        GreenPotion.onNear -= StartSuperJumpTutorial;
        FriendController.onNear -= StartGeneralTutorial;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Memory.isDoneGeneralDirectives)
        {
            if (Memory.isDuringTutorial)
            {
                generalTutoral.enabled = false;
                generalDirectivesStartTime = DateTime.Now;
            }
            else if ((DateTime.Now - generalDirectivesStartTime).TotalSeconds < 15) generalTutoral.enabled = true;
            else
            {
                generalTutoral.enabled = false;
                Memory.isDoneGeneralDirectives = true;
            }
        }

        if (!Memory.isDuringTutorial) return;
        if (((DateTime.Now - tutorialStartTime).TotalSeconds >= tutorialDisplayPeriod && !isSkippable()) ||
            (isSkippable() && Input.GetKeyDown(KeyCode.Return) && (DateTime.Now - tutorialStartTime).TotalSeconds >= skippableDisplayPeriod))
        {
            currentSentenceIdx++;
            if (currentSentenceIdx == tutorialTexts.Count)
            {
                StopTutorial();
            } else
            {
                RefreshTutorialSentence();
            }
        }
    }

    private void StopTutorial()
    {
        currentSentenceIdx = 0;
        Memory.isDuringTutorial = false;
        tutorialPanel.SetActive(false);
        switch (currentTutorialType)
        {
            case "bone":
                Bone.onCollect -= StartBoneTutorial;
                Memory.isDoneBoneTutorial = true;
                break;
            case "coin":
                Coin.onCollect -= StartCoinTutorial;
                Memory.isDoneCoinTutorial = true;
                break;
            case "health":
                PlayerHealth.onPlayerHealthChange -= StartHealthTutorial;
                Memory.isDoneHeartTutorial = true;
                break;
            case "superSpeed":
                Memory.isDoneSuperSpeedTutorial = true;
                break;
            case "superJump":
                Memory.isDoneSuperJumpTutorial = true;
                break;
            case "general":
                Memory.isDoneGeneralTutorial = true;
                GameController.UnpauseGameWithoutEvent();
                player.GetComponent<LookAt>().Resume();
                break;
            case "victory":
                break;
            case "lake":
                Memory.isDoneLakeTutorial = true;
                GameController.UnpauseGameWithoutEvent();
                break;
            case "pumpkinhead":
                if (onCompletePumpkinHeadTutorial != null)
                {
                    onCompletePumpkinHeadTutorial.Invoke();
                }
                GameController.UnpauseGameWithoutEvent();
                break;
            case "pumpkinheadHint":
                if (onCompletePumpkinHeadHint != null)
                {
                    onCompletePumpkinHeadHint.Invoke();
                }
                GameController.UnpauseGameWithoutEvent();
                break;
            case "gourdface":
                if (onCompleteGourdFaceTutorial != null)
                {
                    onCompleteGourdFaceTutorial.Invoke();
                }
                GameController.UnpauseGameWithoutEvent();
                break;
            case "gourdfaceBone":
                if (onCompleteGourdFaceBone != null)
                {
                    onCompleteGourdFaceBone.Invoke();
                }
                GameController.UnpauseGameWithoutEvent();
                break;
            default:
                break;
        }
        currentTutorialType = "";
        nameText.text = "";
        tutorialTexts = new List<string>();
    }

    private bool isSkippable()
    {
        return currentTutorialType == "general" ||
            currentTutorialType == "pumpkinhead" ||
            currentTutorialType == "gourdface" ||
            currentTutorialType == "pumpkinheadHint" ||
            currentTutorialType == "gourdfaceBone" ||
            currentTutorialType == "victory";
    }

    private void StartTutorial()
    {
        Memory.isDuringTutorial = true;
        currentSentenceIdx = 0;
        tutorialPanel.SetActive(true);
        RefreshTutorialSentence();
    }

    private void RefreshTutorialSentence()
    {
        tutorialText.text = tutorialTexts[currentSentenceIdx];
        tutorialStartTime = DateTime.Now;
    }

    public void StartBoneTutorial()
    {
        if (Memory.isDoneBoneTutorial) return;
        tutorialTexts = boneTutorialTexts;
        currentTutorialType = "bone";
        nameText.text = "Friend Aldrich";
        StartTutorial();
    }

    public void StartCoinTutorial()
    {
        if (Memory.isDoneCoinTutorial) return;
        tutorialTexts = coinTutorialTexts;
        currentTutorialType = "coin";
        nameText.text = "Friend Aldrich";
        StartTutorial();
    }

    public void StartHealthTutorial()
    {
        if (Memory.isDoneHeartTutorial) return;
        if (Memory.health == Totals.maxHealth) return;
        tutorialTexts = healthTutorialTexts;
        currentTutorialType = "health";
        nameText.text = "Friend Aldrich";
        StartTutorial();
    }

    public void StartSuperSpeedTutorial()
    {
        if (Memory.isDoneSuperSpeedTutorial) return;
        BluePotion.onNear -= StartSuperSpeedTutorial;
        tutorialTexts = superSpeedTutorialTexts;
        currentTutorialType = "superSpeed";
        nameText.text = "Friend Aldrich";
        StartTutorial();
    }

    public void StartSuperJumpTutorial()
    {
        if (Memory.isDoneSuperJumpTutorial) return;
        GreenPotion.onNear -= StartSuperJumpTutorial;
        tutorialTexts = superJumpTutorialTexts;
        currentTutorialType = "superJump";
        nameText.text = "Friend Aldrich";
        StartTutorial();
    }

    public void StartGeneralTutorial()
    {
        if (Memory.isDoneGeneralTutorial) return;
        FriendController.onNear -= StartGeneralTutorial;
        tutorialTexts = generalTutorialTexts;
        currentTutorialType = "general";
        nameText.text = "Friend Aldrich";
        GameController.PauseGameWithoutEvent();
        player = GameObject.FindGameObjectWithTag("Player");
        aldrich = GameObject.FindGameObjectWithTag("Aldrich").transform;
        player.GetComponent<LookAt>().LookAtTarget(aldrich);
        StartTutorial();
    }

    public void VictoryMessage()
    {
        if (Memory.bonesCollected == Totals.totalBones)
        {
            tutorialTexts = vistoryTexts;
            currentTutorialType = "victory";
            nameText.text = "Friend Aldrich";
            StartTutorial();
        }
    }

    public void StartLakeTutorial()
    {
        if (Memory.isDoneLakeTutorial) return;
        FallInLake.onFallIntoLake -= StartLakeTutorial;
        Invoke("startLakeTutorial", 1);        
    }

    private void startLakeTutorial()
    {
        tutorialTexts = lakeTutorialTexts;
        currentTutorialType = "lake";
        nameText.text = "Friend Aldrich";
        GameController.PauseGameWithoutEvent();
        StartTutorial();
    }

    public void startPumpkinHeadIntro()
    {
        tutorialTexts = pumpkinHeadntroTexts;
        currentTutorialType = "pumpkinhead";
        nameText.text = "Pumpkin Head";
        StartTutorial();
    }

    public void startPumpkinHeadHint(List<string> hints)
    {
        tutorialTexts = hints;
        currentTutorialType = "pumpkinheadHint";
        nameText.text = "Pumpkin Head";
        StartTutorial();
    }

    public void startGourdFaceIntro()
    {
        tutorialTexts = gourdFaceIntroTexts;
        currentTutorialType = "gourdface";
        nameText.text = "Gourd Face";
        StartTutorial();
    }

    public void startGourdFaceBone(List<string> hints)
    {
        tutorialTexts = hints;
        currentTutorialType = "gourdfaceBone";
        nameText.text = "Gourd Face";
        StartTutorial();
    }
}
