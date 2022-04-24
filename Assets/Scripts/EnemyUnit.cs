using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUnit : MonoBehaviour
{
    [Header("External References")]
    public EnemyCharacter enemyStats;

    [Header("Stat Variables")]
    public int healthStat;
    public int attackStat;
    public int defenseStat;

    [Header("Game Variables")]
    public bool isAlive;
    public EnemyActions actionIndex;
    public int attackIndex;

    [Header("ui References")]
    public TextMeshProUGUI nameTxt;
    public Image healthBar;

    const int deselect = -1;

    void Awake()
    {
        this.healthStat = enemyStats.health;
        ResetStats();
        this.isAlive = true;
        this.actionIndex = EnemyActions.WAITING;
        this.attackIndex = deselect;
        nameTxt.text = enemyStats.creatureName.ToString();
    }
    public void ResetAction() 
    {
        this.actionIndex = EnemyActions.WAITING;
        this.attackIndex = deselect;
    }
    void ResetStats()
    {
        this.attackStat = enemyStats.atk;
        this.defenseStat = enemyStats.def;
    }
}
