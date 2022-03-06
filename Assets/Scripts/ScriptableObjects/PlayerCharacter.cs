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
        int damage = this.atk;
        int tohit = 20; 

        return new Attack(damage, tohit);
    }
}