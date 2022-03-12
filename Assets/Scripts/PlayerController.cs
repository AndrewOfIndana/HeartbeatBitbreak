using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter character;
    public int actionIndex = -1;//records what actions is about to be taken should be -1 to deselct
    public int attackIndex = -1; //record of what enemy is going to be attack should be -1 to deselct
    public int itemIndex = -1; //records what item is going to be selected should be -1 to deselct

    public void RecordAction(int action, int enemySelction, int itemSelection)
    {
        this.actionIndex = action;
        this.attackIndex = enemySelction;
        this.itemIndex = itemSelection;
    }

    public void PerformAction()
    {
        if(this.actionIndex == 0) //Defend
        {
            Debug.Log("Character defends");
        }
        else if(this.actionIndex == 1) 
        {
            Debug.Log("Character attacks enemy " + (this.attackIndex + 1));
        }
        else if(this.actionIndex == 2) 
        {
            Debug.Log("Character attacks enemy " + (this.attackIndex + 1));
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
}
