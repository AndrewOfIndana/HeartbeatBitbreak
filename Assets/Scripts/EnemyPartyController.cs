using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartyController : MonoBehaviour
{
    public GameManager gameManager;
    public EnemyController[] enemies;

    public void EnemyAction()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].RandomAction();
            enemies[i].PerformAction();
            enemies[i].ResetAction();
        }
        Invoke("DumbFunctionInvoke", 2f);
    }

    void DumbFunctionInvoke()
    {
        gameManager.InputPhase();
    }

    public void RecieveAttacks(bool isMulti, int index, Attack attack)
    {
        if(isMulti)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                if(enemies[i] != null)
                {
                    enemies[i].Reaction(attack);
                }
            }
        }
        else if(!isMulti)
        {
            if(enemies[index] != null)
            {
                enemies[index].Reaction(attack);
            }
        }
    }
}
