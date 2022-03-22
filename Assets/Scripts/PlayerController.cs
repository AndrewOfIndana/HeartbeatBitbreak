using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public ItemManager itemManager;
    public BattleUIController battleUI;
    public PartyController party;
    public PlayerCharacter character;
    
    public bool isAlive = true;
    private int actionIndex = -1;//records what actions is about to be taken should be -1 to deselct
    private int attackIndex = -1; //record of what enemy is going to be attack should be -1 to deselct
    private int itemIndex = -1; //records what item is going to be selected should be -1 to deselct

    void Awake()
    {
        this.character.ResetHealth();
        isAlive = true;
    }

    public void RecordAction(int action, int enemySelction, int itemSelection)
    {
        this.actionIndex = action;
        this.attackIndex = enemySelction;
        this.itemIndex = itemSelection;
        this.character.ResetDef();
        this.character.ResetAtk();
        
        if(!(itemSelection == -1))
        {
            itemManager.EmptyItemName(itemSelection);
        }
    }

    public void PerformAction()
    {
        if(this.actionIndex == 0) //Defend
        {
            this.character.DefenseBoost();
        }
        else if(this.actionIndex == 1) 
        {
            gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
        }
        else if(this.actionIndex == 2)
        {
            if(character.playerSkill == 1) //great attack
            {
                this.character.AttackBoost();
                gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
            }
            if(character.playerSkill == 2) //heal all
            {
                party.PartyEffect(character.playerSkill);
                this.character.AttackWeak();
                gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
                battleUI.UpdateHealth();
            }
            if(character.playerSkill == 3)  //attack all
            {
                this.character.AttackSpread();
                gameManager.ExchangeDamage(true, this.attackIndex, this.character.GetAttack(gameManager.groove));
            }
            if(character.playerSkill == 4) //def all
            {
                this.character.AttackWeak();
                party.PartyEffect(character.playerSkill);
                gameManager.ExchangeDamage(false, this.attackIndex, this.character.GetAttack(gameManager.groove));
            }
        }
        else if(this.actionIndex == 3) 
        {
            ItemInstance usedItem = itemManager.SelectItem(this.itemIndex);
            bool doesItemExist = usedItem.ValidateItem();

            if(doesItemExist)
            {
                party.PartyEffect(usedItem.itemStats.itemNo);
                usedItem.ConsumeItem();
                battleUI.UpdateHealth();
            }
        }
    }

    public void ResetAction() 
    {
        //Add attack class
        this.actionIndex = -1;
        this.attackIndex = -1;
        this.itemIndex = -1;
        battleUI.UpdateHealth();
    }

    public void Reaction(Attack attack)
    {
        this.character.ReceiveAttack(attack);
        battleUI.UpdateHealth();

        if((character.health <= 0) && this.isAlive)
        {
            gameManager.KillConfirmed(true);
            this.isAlive = false;
            gameObject.SetActive(false);
        }
    }

}
