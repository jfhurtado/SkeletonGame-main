using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Memory 
{
    public static List<(string, Vector3)> activeHealth = new List<(string, Vector3)>();
    public static List<string> coinMemory = new List<string>();
    public static List<string> smashMemory = new List<string>();

    // this list contains all the killed spider ids 
    public static List<string> enemyMemory = new List<string>();
    public static List<string> boneMemory = new List<string>();
    public static List<string> wallMemory = new List<string>();
    public static bool gateOpen = false;
    public static bool friendHasBody = false;
    public static bool isGameOver = false;
    public static bool isPause = false;
    public static bool isDuringTutorial = false;
    public static bool isDoneGeneralTutorial = false;
    public static bool isDoneSuperJumpTutorial = false;
    public static bool isDoneSuperSpeedTutorial = false;
    public static bool isDoneSpiderTutorial = false;
    public static bool isDoneBoneTutorial = false;
    public static bool isDoneCoinTutorial = false;
    public static bool isDoneHeartTutorial = false;
    public static bool isDoneGeneralDirectives = false;
    public static bool isDoneLakeTutorial = false;
    public static int bonesCollected = 0;
    public static int coinsCollected = 0;
    public static int health = Totals.maxHealth;
    public static int nextHint = 0;
    public static List<string> foundHints = new List<string>();
    public static bool metPumpkinHead = false;
    public static bool metGourdFace = false;
    public static int bonesBought = 0;
}
