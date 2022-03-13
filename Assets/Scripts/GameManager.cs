using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //THIS SCRIPT MANAGES THE WHAT STATE THE BATTLE SYSTEM IS IN, AND WHETHER THE PLAYER HAS WON OR LOST AS WELL AS OTHER GAME RELATED FUNCTIONS
    public PartyController party;
    public EnemyPartyController enemyParty;
    public enum GameStates {STARTING ,INPUT, ACTION, ENEMYACTION, WIN, LOSE}; //A new enumeration that will decide whether it is the player turn, when the player's party will attack and when the enemy will attack.
    public GameStates battleState; //This is the actual state system that the game will keep track of.


    [Header("Time Sync Objects")]
    
    public int groove = 1;

    void Start() 
    {
        battleState = GameStates.INPUT;
    }

    public void ActionPhase() 
    {
        battleState = GameStates.ACTION;
    }

    public void ReactionPhase()
    {
        battleState = GameStates.ENEMYACTION;
        enemyParty.EnemyAction();
    }

    public void InputPhase()
    {
        battleState = GameStates.INPUT;
        party.PlayerInputStart();
    }

    public void SyncBeatObjects() {
        
    }
}
   
