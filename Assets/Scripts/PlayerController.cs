using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacter character;

    public int characterHealth;
    public int characterAttack;
    public int characterDefense;

    //What do these values do? What does a 0 represent? Does a 0 for an action value have the same meaning as the 0 for actionAgainst or ActionFor?
    public int action = 0;
    public int actionAgainst = -1;
    public int actionFor = -1;

    void Awake() 
    {
        //Why do we have these values? This seems to just duplicate the functionality in PlayerCharacter character? those values are public so we should just directly access them
        this.characterHealth = character.health; 
        this.characterAttack = character.atk;
        this.characterDefense = character.def;
    }

    public void RecordAttack(int enemy) 
    {
        this.action = 1;
        this.actionAgainst = enemy;
    }
    public void RecordSkills(int enemy) 
    {
        this.action = 2;
        this.actionAgainst = enemy;
    }
    public void RecordDefense() 
    {
        this.action = 3;
    }
    public void RecordItem(int chara) 
    {
        this.action = 4;

        //I commented this line out so that it would compile
        //this.actionFor = ememy; //This doesn't exist in this scope???
    }

}
