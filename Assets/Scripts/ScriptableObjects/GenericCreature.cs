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
    public int max_atk;
    public int max_def;

    public abstract void ReceiveAttack(Attack attack);
    public abstract Attack GetAttack();

    public void ResetHealth() {
        this.health = this.max_health;
        this.atk = this.max_atk;    
        this.def = this.max_def;    
    }
    public void ResetAtk() {
        this.atk = this.max_atk;    
    }
    public void ResetDef() {
        this.def = this.max_def;    
    }

    public void Heal() {
        this.health = this.health + (this.max_health / 2);

        if(this.health > this.max_health)
        {
            this.health = this.max_health;
        }
    }

    public void AttackBoost() {
        this.atk = this.atk * 2;
    }
    public void AttackSpread() {
        this.atk = this.atk / 2;
    }
    public void AttackWeak() {
        this.atk = this.atk / 3;
    }

    public void DefenseBoost() {
        this.def = this.def * 2;
    }
}
