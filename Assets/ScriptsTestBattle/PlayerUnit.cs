using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [Header("External References")]
    public ItemManager itemManager;
    public PlayerCharacter character;

    [Header("Game Variables")]
    public bool isAlive;
    public Actions actionIndex;
    public int attackIndex;
    public int itemIndex;

    const int deselect = -1;

    void Awake()
    {
        this.character.ResetHealth();
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
        this.character.ResetDef();
        this.character.ResetAtk();
        
        if(!(itemSelect == -1))
        {
            itemManager.EmptyItemName(itemSelect);
        }
    }

    public void PerformAction()
    {
        if(this.actionIndex == Actions.DEFEND) //Defend
        {
            this.character.DefenseBoost();
        }
        else if(this.actionIndex == Actions.ATTACK) //ATTACK
        {
            //gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
        }
        else if(this.actionIndex == Actions.SKILLS)
        {
            if(character.playerSkill == 1) //STRONG ATTACK
            {
                this.character.AttackBoost();
                //gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
            }
            if(character.playerSkill == 2) //HEAL PARTY
            {
                SendMessageUpwards("PartyEffect", character.playerSkill);
                this.character.AttackWeak();
                //gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
                //battleUI.UpdateHealth();
            }
            if(character.playerSkill == 3)  //MUlTI ATTACK
            {
                this.character.AttackSpread();
                //gameManager.ExchangeDamage(true, this.attackIndex, this.character.GetAttack(gameManager.groove));
            }
            if(character.playerSkill == 4) //DEFEND PARTY
            {
                this.character.AttackWeak();
                SendMessageUpwards("PartyEffect", character.playerSkill);
                //gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
            }
        }
        else if(this.actionIndex == Actions.ITEM) 
        {
            ItemInstance usedItem = itemManager.SelectItem(this.itemIndex);
            bool doesItemExist = usedItem.ValidateItem();

            if(doesItemExist)
            {
                SendMessageUpwards("PartyEffect", usedItem.itemStats.itemNo);
                usedItem.ConsumeItem();
                //battleUI.UpdateHealth();
            }
        }
    }

}
