using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter character;

    public int characterHealth;
    public int characterAttack;
    public int characterDefense;

    void Awake() 
    {
        characterHealth = character.health;
        characterAttack = character.atk;
        characterDefense = character.def;
    }

    public void RecordAttack(int enemy) 
    {
        
    }
}
