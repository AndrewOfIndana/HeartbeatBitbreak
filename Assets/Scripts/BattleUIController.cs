using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIController : MonoBehaviour
{
    //THIS SCRIPT MANAGES THE UI FOR BATTLE, FOR EACH CHARACTER'S HEALTH, THE GROOVE METER AND THE METRONOME
    [Header("Framework")]
    public GameManager gameManager; //Needed to know when to reset metronome
    public Conductor conducter; //Needed for the metronome
    //public PartyController //Needed for groove
    //public PlayerController[] partyMembers; //Need for health
}
