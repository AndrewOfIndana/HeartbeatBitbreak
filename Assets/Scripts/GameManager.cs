using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //THIS SCRIPT MANAGES THE WHAT STATE THE BATTLE SYSTEM IS IN, AND WHETHER THE PLAYER HAS WON OR LOST AS WELL AS OTHER GAME RELATED FUNCTIONS
    public PartyController party;
    public EnemyPartyController enemyParty;
    public enum GameStates {STARTING ,INPUT, ACTION, ENEMYACTION, WIN, LOSE}; //A new enumeration that will decide whether it is the player turn, when the player's party will attack and when the enemy will attack.
    public GameStates battleState; //This is the actual state system that the game will keep track of.
    public int playerDeaths = 0;
    public int enemyDeaths = 0;
    public CanvasGroup winScreen;
    public CanvasGroup loseScreen;
    float timer = 0;
    public bool isGameOver = false;

    [Header("Time Sync Objects")]
    
    public int groove = 1;

    void Start() 
    {
        Invoke("StartBattlePhase", 3f);
    }

    public void StartBattlePhase()
    {
        if(!isGameOver)
        {
            SyncBeat.Instance.StartBeat();
            battleState = GameStates.INPUT;
            party.PlayerInputStart();
        }
    }

    public void ActionPhase() 
    {
        if(!isGameOver)
        {
            battleState = GameStates.ACTION;
        }
    }

    public void ReactionPhase()
    {
        if(!isGameOver)
        {
            battleState = GameStates.ENEMYACTION;
            enemyParty.EnemyAction();
        }
    }

    public void InputPhase()
    {
        if(!isGameOver)
        {
            battleState = GameStates.INPUT;
            party.PlayerInputStart();
        }
    }

    public void ExchangeDamage(bool isMulti, int index, Attack attack) 
    {
        if(battleState == GameStates.ACTION) 
        {
            enemyParty.RecieveAttacks(isMulti, index, attack);
        }
        else if(battleState == GameStates.ENEMYACTION) 
        {
            party.RecieveAttacks(index, attack);
        }
    }

    public void KillConfirmed(bool isPlayer)
    {
        if(isPlayer)
        {
            playerDeaths++;
            if(playerDeaths >= 4)
            {
                isGameOver = true;
                battleState = GameStates.LOSE;
            }
        }
        else
        {
            enemyDeaths++;
            if(enemyDeaths >= 4)
            {
                isGameOver = true;
                battleState = GameStates.WIN;
            }
        }
    }

    void Update()
    {
        if(battleState == GameStates.WIN)
        {
            battleState = GameStates.WIN;
            BattleResults(winScreen);
        }
        else if(battleState == GameStates.LOSE)
        {
            battleState = GameStates.LOSE;
            BattleResults(loseScreen);
        }
    }

    void BattleResults(CanvasGroup endingScreen)
    {
        timer += Time.deltaTime;
        endingScreen.alpha = timer / 1f;

        if(timer > 2f)
        {
            Application.Quit();
        }
    }
}
   
