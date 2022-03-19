using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerCharacter character;
    
    public bool isAlive = true;
    public int actionIndex = -1;//records what actions is about to be taken should be -1 to deselct
    public int attackIndex = -1; //record of what enemy is going to be attack should be -1 to deselct
    public int itemIndex = -1; //records what item is going to be selected should be -1 to deselct

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
    }

    public void PerformAction()
    {
        if(this.actionIndex == 0) //Defend
        {
            this.character.DefenseBoost();
        }
        else if(this.actionIndex == 1 || this.actionIndex == 2) 
        {
            gameManager.ExchangeDamage(this.attackIndex, this.character.GetAttack(gameManager.groove));
        }
        else if(this.actionIndex == 3) 
        {
            Debug.Log("Character uses item " + (this.itemIndex + 1));
        }
    }

    public void ResetAction() 
    {
        //Add attack class
        this.actionIndex = -1;
        this.attackIndex = -1;
        this.itemIndex = -1;
    }

    public void Reaction(Attack attack)
    {
        this.character.ReceiveAttack(attack);

        if(character.health <= 0)
        {
            
            gameManager.KillConfirmed(true);
            isAlive = false;
            gameObject.SetActive(false);
        }
    }

}
