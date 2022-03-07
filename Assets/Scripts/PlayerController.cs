using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter character;

    public int characterHealth;
    public int characterAttack;
    public int characterDefense;

    public int action = 0;
    public int actionAgainst = -1;
    public int actionFor = -1;

    void Awake() 
    {
        this.characterHealth = character.health;
        this.characterAttack = character.atk;
        this.characterDefense = character.def;
    }

    public void RecordAttack(int enemy) 
    {
        this.action = 1;
        this.actionAgainst = ememy;
    }
    public void RecordSkills(int enemy) 
    {
        this.action = 2;
        this.actionAgainst = ememy;
    }
    public void RecordDefense() 
    {
        this.action = 3;
    }
    public void RecordItem(int chara) 
    {
        this.action = 4;
        this.actionFor = ememy;
    }

}
