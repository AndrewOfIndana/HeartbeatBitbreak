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

    public void RecieveAttacks(int index, Attack attack)
    {
        if(enemies[index] != null)
            enemies[index].Reaction(attack);
    }
}
