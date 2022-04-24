using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCreature : ScriptableObject
{
    public string creatureName;
    public int health;
    public int atk;
    public int def;

    public abstract int GetDefense(Attack attack);
    public abstract Attack GetAttack(int atkVal);
}
