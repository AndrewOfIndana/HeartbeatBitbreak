using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacter", menuName = "ScriptableObjects/PlayerDataClass", order = 0)]
public class PlayerCharacter : GenericCreature
{
    public override void ReceiveAttack(Attack attack)
    {
        if (attack.GetToHit() >= this.def) {

            this.health -= attack.GetDamage();

            if (this.health < 0) {
                health = 0;
            }
        }
    }

    public override Attack GetAttack()
    {
        return this.GetAttack(0); //Calls the one with an int signature to reduce redunancy
    }

    public Attack GetAttack(int bonus) {
        int damage = this.atk + bonus;
        int tohit = 20; //20 is higher than any valid def value so it should always hit

        return new Attack(damage, tohit);
    
    }
}