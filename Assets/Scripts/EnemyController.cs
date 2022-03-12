using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyCharacter enemy;
    public int actionIndex = -1;//records what actions is about to be taken should be -1 to deselct
    public int attackIndex = -1; //record of what enemy is going to be attack should be -1 to deselct

    public void RandomAction()
    {
        int randomAction = Random.Range(0, 10);
        if(randomAction <= 6) {
            this.actionIndex = 0;
        } else if (randomAction >= 7) {
            this.actionIndex = 1;
        }
        this.attackIndex = Random.Range(0, 4);
    }
    
    public void PerformAction()
    {
        if(this.actionIndex == 0) 
        {
            Debug.Log("Enemy attacks character " + (this.attackIndex + 1));
        }
        else if(this.actionIndex == 1) 
        {
            Debug.Log("Enemy crits character " + (this.attackIndex + 1));
        }
    }

    public void ResetAction() 
    {
        //Add attack class
        this.actionIndex = -1;
        this.attackIndex = -1;
    }
}
