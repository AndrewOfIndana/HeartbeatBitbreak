using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //THIS SCRIPT MANAGES THE WHAT STATE THE BATTLE SYSTEM IS IN, AND WHETHER THE PLAYER HAS WON OR LOST AS WELL AS OTHER GAME RELATED FUNCTIONS
    public enum GameStates {STARTING ,INPUT, ACTION, ENEMYACTION, WIN, LOSE}; //A new enumeration that will decide whether it is the player turn, when the player's party will attack and when the enemy will attack.
    public GameStates battleState = GameStates.INPUT; //This is the actual state system that the game will keep track of.
    public int groove = 1;

    public void ActionPhase() 
    {
        battleState = GameStates.ACTION;
        //play animation find out how much time it takes for characters to do actions
        Invoke("ReactionPhase", 5f);
    }

    void ReactionPhase()
    {
        battleState = GameStates.ENEMYACTION;
        //ADD ENEMY ACTIONS
        Invoke("InputPhase", 5f);
    }

    void InputPhase()
    {
        battleState = GameStates.INPUT;
    }
}
