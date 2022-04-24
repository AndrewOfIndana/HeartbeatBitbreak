using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("External References")]
    public PlayerCharacter playerStats;

    [Header("Stat Variables")]
    public int healthStat;
    public int attackStat;
    public int defenseStat;

    [Header("Game Variables")]
    public bool isAlive;
    public Actions actionIndex;
    public int attackIndex;
    public int itemIndex;

    private const int deselect = -1;

    //      START FUNCTIONS       \\
    void Awake()
    {
        this.healthStat = playerStats.health;
        ResetStats();
        this.isAlive = true;
        this.actionIndex = Actions.WAITING;
        this.attackIndex = deselect;
        this.itemIndex = deselect;
    }

    //      PLAYERTURN FUNCTIONS       \\
    public void RecordAction(Actions recordedAction, int enemySelect, int itemSelect)
    {
        this.actionIndex = recordedAction;
        this.attackIndex = enemySelect;
        this.itemIndex = itemSelect;
        ResetStats();
    }
    public void ResetAction() 
    {
        this.actionIndex = Actions.WAITING;
        this.attackIndex = deselect;
        this.itemIndex = deselect;
    }

    //      STAT RELATED FUNCTIONS       \\
    public void Heal()
    {
        this.healthStat = (int)(this.healthStat * 1.5f);
    }
    public void AttackBoost()
    {
        this.attackStat = (int)(this.defenseStat * 1.5f);
    }
    public void AttackLoss()
    {
        this.attackStat = (int)(this.defenseStat * 0.5f);
    }
    public void AttackSpread()
    {
        this.attackStat = (int)(this.defenseStat * 0.3f);
    }

    public void DefenseBoost()
    {
        this.defenseStat = (int)(this.defenseStat * 1.5f);
    }

    void ResetStats()
    {
        this.attackStat = playerStats.atk;
        this.defenseStat = playerStats.def;
    }
}
