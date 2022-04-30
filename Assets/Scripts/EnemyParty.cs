using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyActions { WAITING, ATTACK }

public class EnemyParty : MonoBehaviour
{
    [Header("External References")]
    public GameManager gameManager;
    public BattleMenuController battleMenu;
    public BattleUIController battleUI;
    public ScreenShake cameraShake;

    private List<EnemyUnit> enemies = new List<EnemyUnit>();

    private const int noAction = -1;

    public void SetPartyMembers(List<EnemyUnit> eUnits) 
    {
        enemies = eUnits;
        battleMenu.SetEnemyNames(eUnits);
    }
    public void PerformAction(int enemi)
    {
        if(enemies[enemi].isAlive)
        {
            enemies[enemi].actionIndex = EnemyActions.ATTACK;
            enemies[enemi].attackIndex = Random.Range(0, 3);
            gameManager.ExchangeDamage(enemies[enemi].attackIndex, enemies[enemi].enemyStats.GetAttack(enemies[enemi].attackStat), false);
            battleUI.UpdateEnemyHealth();
            enemies[enemi].ResetAction();
        }
    }

    public void Reaction(int index, Attack attack, bool isMulti)
    {
        if(isMulti)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if(enemies[index].isAlive)
                {
                    StartCoroutine(cameraShake.Shake(.2f, .1f));
                    enemies[i].healthStat -= enemies[i].enemyStats.GetDefense(enemies[i].defenseStat, attack);
                    Instantiate(enemies[i].hitPrefab, enemies[i].transform);
                    battleUI.UpdateEnemyHealth();
                }
            }
        }
        else if(!isMulti)
        {
            if(enemies[index].isAlive)
            {
                StartCoroutine(cameraShake.Shake(.2f, .1f));
                enemies[index].healthStat -= enemies[index].enemyStats.GetDefense(enemies[index].defenseStat, attack);
                Instantiate(enemies[index].hitPrefab, enemies[index].transform);
                battleUI.UpdateEnemyHealth();
            }
        }
        for(int i = 0; i < enemies.Count; i++) 
        {
            if((enemies[i].healthStat <= 0) && enemies[i].isAlive)
            {
                enemies[i].isAlive = false;
                enemies[i].gameObject.SetActive(false);
                gameManager.KillConfirmed(false);
            }
        }
    }
}
