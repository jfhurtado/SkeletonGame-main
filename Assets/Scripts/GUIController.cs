using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GUIController : MonoBehaviour
{
    public RawImage bone;
    public RawImage coin;
    public GameObject healthBar;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Coin.onCollect += updateStats;
        Bone.onCollect += updateStats;
        PlayerHealth.onPlayerHealthChange += updateStats;
        updateStats();
    }

    // Update is called once per frame
    void Update()
    {
        updateStats();
    }

    private void updateStats()
    {
        bone.GetComponentInChildren<TextMeshProUGUI>().text = $"{Memory.bonesCollected}" + "/" + Totals.totalBones;
        coin.GetComponentInChildren<TextMeshProUGUI>().text = $"{Memory.coinsCollected}"; 

        RawImage[] hearts = healthBar.GetComponentsInChildren<RawImage>();
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= Memory.health)
            {
                hearts[i].color = Color.black;
            } else
            {
                hearts[i].color = Color.white;
            }
        }
    }
    private void OnDestroy()
    {
        Coin.onCollect -= updateStats;
        Bone.onCollect -= updateStats;
        PlayerHealth.onPlayerHealthChange -= updateStats;
    }
}
