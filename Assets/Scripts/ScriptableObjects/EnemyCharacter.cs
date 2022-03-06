using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyDataClass", order = 1)]
public class EnemyCharacter : GenericCreature
{

    public override void  ReceiveAttack(Attack attack) {

        this.health -= attack.GetDamage();

        if (this.health < 0) {

            health = 0;

        }

    }

    public override Attack GetAttack()
    {
        
        int damage = this.atk;
        int tohit = Random.Range(0, 20); // Generates a random int between 0-19

        return new Attack(damage, tohit);

    }

}
