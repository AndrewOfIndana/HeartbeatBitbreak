using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActions { WAITING, ATTACK }

public class EnemyParty : MonoBehaviour
{
    [Header("External References")]
    public BattleSystem battleSystem;

    private List<EnemyUnit> enemies = new List<EnemyUnit>();

    private const int noAction = -1;

    public void SetPartyMembers(List<EnemyUnit> eUnits) 
    {
        enemies = eUnits;
    }
    public void PerformAction(int enemi, int playerLeft)
    {
        enemies[enemi].actionIndex = EnemyActions.ATTACK;
        enemies[enemi].attackIndex = Random.Range(0, playerLeft);
        battleSystem.ExchangeDamage(enemies[enemi].attackIndex, enemies[enemi].enemyStats.GetAttack(), false);
        enemies[enemi].ResetAction();
    }

    public void Reaction(int index, Attack attack, bool isMulti)
    {
        if(isMulti)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i] != null)
                {
                    enemies[i].enemyStats.ReceiveAttack(attack);
                }
            }
        }
        else if(!isMulti)
        {
            if(enemies[index] != null)
            {
                enemies[index].enemyStats.ReceiveAttack(attack);
            }
        }
        for(int i = 0; i < enemies.Count; i++) 
        {
            if((enemies[i].enemyStats.health <= 0) && enemies[i].isAlive)
            {
                enemies[i].isAlive = false;
                enemies[i].gameObject.SetActive(false);
            }
        }
    }
}
