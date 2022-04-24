using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacter", menuName = "ScriptableObjects/PlayerDataClass", order = 0)]
public class PlayerCharacter : GenericCreature
{
    public int playerSkill;
    
    public Attack GetAttack(int atkVal, int bonus) 
    {
        int damage = atkVal + bonus;
        return new Attack(damage);
    }

    public override Attack GetAttack(int atkVal)
    {
        return this.GetAttack(atkVal); //Calls the one with an int signature to reduce redunancy
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