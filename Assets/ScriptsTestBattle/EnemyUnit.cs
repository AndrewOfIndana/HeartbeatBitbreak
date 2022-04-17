using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [Header("External References")]
    public EnemyCharacter enemyStats;

    [Header("Game Variables")]
    public bool isAlive;
    public EnemyActions actionIndex;
    public int attackIndex;

    const int deselect = -1;

    void Awake()
    {
        this.enemyStats.ResetHealth();
        this.isAlive = true;
        this.actionIndex = EnemyActions.WAITING;
        this.attackIndex = deselect;
    }
    public void ResetAction() 
    {
        this.actionIndex = EnemyActions.WAITING;
        this.attackIndex = deselect;
    }
}
