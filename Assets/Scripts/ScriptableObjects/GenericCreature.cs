using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCreature : ScriptableObject
{
    // Start is called before the first frame update

    public int health; //Number of damage character can take before dying
    public int atk;    //Number of damage units done on correct hit
    public int def;    //Value the the Attack.ToHit has to meet or hit in in order to inflict damage
    public int max_health;

    public abstract void ReceiveAttack(Attack attack);
    public abstract Attack GetAttack();

    public void ResetHealth() {
        this.health = this.max_health;    
    }
}
