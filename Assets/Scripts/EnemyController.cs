using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameManager gameManager;
    public EnemyCharacter enemy;
    
    public int actionIndex = -1;//records what actions is about to be taken should be -1 to deselct
    public int attackIndex = -1; //record of what enemy is going to be attack should be -1 to deselct

    void Awake()
    {
        enemy.ResetHealth();
    }

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
        gameManager.ExchangeDamage(this.attackIndex, this.enemy.GetAttack());
    }

    public void ResetAction() 
    {
        //Add attack class
        this.actionIndex = -1;
        this.attackIndex = -1;
    }

    public void Reaction(Attack attack)
    {
        this.enemy.ReceiveAttack(attack);

        if(enemy.health <= 0)
        {
            
            gameManager.KillConfirmed(false);
            Destroy(this.gameObject);
        }
    }
}
