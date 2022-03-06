using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericCreature : ScriptableObject
{
    // Start is called before the first frame update

    public int health;
    public int atk;
    public int def;

    public abstract void ReceiveAttack();
}
