using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public delegate void PlayerHealthChange();
    public static PlayerHealthChange onPlayerHealthChange;

    public delegate void GameOver();
    public static GameOver onGameOver;

    // Start is called before the first frame update
    void Start()
    {
        Heart.onCollect = gainHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyUp(KeyCode.N))
        {
            TakeDamage(1);
        }
        */
    }


    private void gainHealth()
    {
        Debug.Log("gaining health insider player health ");
        Memory.health = Mathf.Min(Totals.maxHealth, Memory.health + 1);

        if (onPlayerHealthChange != null)
        {
            onPlayerHealthChange.Invoke();
        }
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("taking damage insider player health ");
        Memory.health = Mathf.Max(0, Memory.health - damage);
        if (onPlayerHealthChange != null)
        {
            onPlayerHealthChange.Invoke();
        }

        if (Memory.health == 0)
        {
            if (onGameOver != null)
            {
                SkeletonMovement skeleton_dead = GetComponent<SkeletonMovement>();
                skeleton_dead.dead();
                Memory.isGameOver = true;
                onGameOver.Invoke();
            }
        } 
    }

    public override int GetHealth()
    {
        return Memory.health;
    }


}
