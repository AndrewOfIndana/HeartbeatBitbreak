using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERINPUT, PLAYERTURN, ENEMYTURN, }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [Header("Setup References")]
    public GameObject[] playerPrefabs;
    public GameObject[] enemyPrefabs;
    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    [Header("Battle References")]
    public PlayerParty playerParty;
    public EnemyParty enemyParty;
    public List<PlayerUnit> playerUnits = new List<PlayerUnit>();
    public List<EnemyUnit> enemyUnits = new List<EnemyUnit>();

    public int groove = 1;
    //      START FUNCTIONS       \\
    void Start() 
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }
    IEnumerator SetupBattle()
    {
        for(int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject playerObjects = Instantiate(playerPrefabs[i], playerPositions[i]);
            playerUnits.Add(playerObjects.GetComponent<PlayerUnit>());
            GameObject enemyObjects = Instantiate(enemyPrefabs[i], enemyPositions[i]);
            enemyUnits.Add(enemyObjects.GetComponent<EnemyUnit>());
        }
        playerParty.SetPartyMembers(playerUnits);
        enemyParty.SetPartyMembers(enemyUnits);
        yield return new WaitForSeconds(3f);
        state = BattleState.PLAYERINPUT;
        PlayerInput();
    }

    //      BATTLELOOP FUNCTIONS       \\
    void PlayerInput()
    {
        playerParty.PrimeInput();
    }
    public IEnumerator PlayerTurn()
    {
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerParty.PerformAction(i);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator EnemyTurn()
    {
        for (int i = 0; i < enemyUnits.Count; i++)
        {
            enemyParty.PerformAction(i, playerUnits.Count);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERINPUT;
        PlayerInput();
    }

    //      DAMAGE EXCHANGE FUNCTIONS       \\
    public void ExchangeDamage(int index, Attack attack, bool isMulti)
    {
        if(state == BattleState.PLAYERTURN) 
        {
            enemyParty.Reaction(index, attack, isMulti);
        }
        else if(state == BattleState.ENEMYTURN) 
        {
            playerParty.Reaction(index, attack, isMulti);
        }
    }
}
