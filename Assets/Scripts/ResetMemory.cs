using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMemory : MonoBehaviour
{
    public static void Reset()
    {
        Memory.activeHealth.Clear();
        Memory.boneMemory.Clear();
        Memory.coinMemory.Clear();
        Memory.enemyMemory.Clear();
        Memory.gateOpen = false;
        Memory.smashMemory.Clear();
        Memory.wallMemory.Clear();
        Memory.isGameOver = false;
        Memory.isPause = false;
        Memory.isDuringTutorial = false;
        Memory.isDoneGeneralTutorial = false;
        Memory.isDoneSuperJumpTutorial = false;
        Memory.isDoneSuperSpeedTutorial = false;
        Memory.isDoneSpiderTutorial = false;
        Memory.isDoneBoneTutorial = false;
        Memory.isDoneCoinTutorial = false;
        Memory.isDoneLakeTutorial = false;
        Memory.friendHasBody = false;
        Memory.metPumpkinHead = false;
        Memory.metGourdFace = false;
        Memory.bonesCollected = 0;
        Memory.coinsCollected = 0;
        Memory.health = Totals.maxHealth;
        Memory.nextHint = 0;
    }
}
