using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("External References")]
    public PlayerCharacter playerStats;

    [Header("Game Variables")]
    public bool isAlive;
    public Actions actionIndex;
    public int attackIndex;
    public int itemIndex;

    private const int deselect = -1;

    void Awake()
    {
        this.playerStats.ResetHealth();
        this.isAlive = true;
        this.actionIndex = Actions.WAITING;
        this.attackIndex = deselect;
        this.itemIndex = deselect;
    }

    public void RecordAction(Actions recordedAction, int enemySelect, int itemSelect)
    {
        this.actionIndex = recordedAction;
        this.attackIndex = enemySelect;
        this.itemIndex = itemSelect;
        this.playerStats.ResetDef();
        this.playerStats.ResetAtk();
    }

    public void ResetAction() 
    {
        this.actionIndex = Actions.WAITING;
        this.attackIndex = deselect;
        this.itemIndex = deselect;
    }
}
