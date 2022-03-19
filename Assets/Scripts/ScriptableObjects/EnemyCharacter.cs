using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyDataClass", order = 1)]
public class EnemyCharacter : GenericCreature
{
    public int CritChance = 30;
    public override void ReceiveAttack(Attack attack) {

        this.health -= attack.GetDamage();

        if (this.health < 0) {

            health = 0;

        }

    }

    public override Attack GetAttack()
    {
        int damage_mult = 1;
        if (Random.Range(0, 100) <= this.CritChance) {
            damage_mult = 2;        
        }
        int damage = this.atk * damage_mult;
        int tohit = Random.Range(0, 20); // Generates a random int between 0-19

        return new Attack(damage, tohit);

    }

}
