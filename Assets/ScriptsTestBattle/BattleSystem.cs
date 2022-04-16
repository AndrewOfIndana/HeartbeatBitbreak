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
    List<PlayerUnit> playerUnits = new List<PlayerUnit>();
    List<EnemyUnit> enemyUnits = new List<EnemyUnit>();

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

    void PlayerInput()
    {
        playerParty.PrimeInput();
    }
    public IEnumerator PlayerTurn()
    {
        //playerParty.UpdatePlayerAction();
        //enemyParty.UpdateEnemyAction();
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator EnemyTurn()
    {
        //playerParty.UpdatePlayerAction();
        //enemyParty.UpdateEnemyAction();
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERINPUT;
        PlayerInput();
    }
}
