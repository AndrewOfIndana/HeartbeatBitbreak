using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyDataClass", order = 1)]
public class EnemyCharacter : GenericCreature
{
    public int CritChance = 30;


    public override Attack GetAttack(int atkVal) 
    {
        int damage_mult = 1;
        if (Random.Range(0, 100) <= this.CritChance) 
        {
            damage_mult = 2;        
        }
        int damage = atkVal * damage_mult;
        return new Attack(damage);
    }

    public int GetDefense(int defVal, Attack damage)
    {
        int newDamage = 0;
        int oldDamage = damage.GetDamage();
        if (defVal > oldDamage)
        {
            newDamage = (int)(oldDamage * .5f);
        }
        else if (defVal <= oldDamage)
        {
            newDamage = oldDamage;
        }
        return newDamage;
    }
    public override int GetDefense(Attack damage)
    {
        return this.GetDefense(damage); //Calls the one with an int signature to reduce redunancy
    }
}
