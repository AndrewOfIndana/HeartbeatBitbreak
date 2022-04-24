using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERINPUT, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameManager : MonoBehaviour
{
    public BattleState state;

    [Header("Setup References")]
    public GameObject[] playerPrefabs;
    public GameObject[] enemyPrefabs;
    public Transform[] playerPositions;
    public Transform[] enemyPositions;
    public CanvasGroup beginningScreen;
    public TextMeshProUGUI countDownTimerTxt;
    private float startCountDownTimer;

    [Header("Battle References")]
    public PlayerParty playerParty;
    public EnemyParty enemyParty;
    public BattleUIController battleUI;
    public Transform[] playerforePositions;
    public Transform[] enemyforePositions;
    private List<PlayerUnit> playerUnits = new List<PlayerUnit>();
    private List<EnemyUnit> enemyUnits = new List<EnemyUnit>();

    [Header("Win/Lose References")]
    public CanvasGroup winScreen;
    public CanvasGroup loseScreen;
    private int playerDeaths = 0;
    private int enemyDeaths = 0;
    private bool isGameOver = false;
    private float endCountDownTimer;

    [Header("Game Variables")]
    public int groove = 1;
    //      START FUNCTIONS       \\
    void Start() 
    {
        startCountDownTimer = 3f;
        endCountDownTimer = 0f;
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
        battleUI.SetUnitHealth(playerUnits, enemyUnits);
        yield return new WaitForSeconds(3f);
        beginningScreen.alpha = 0;
        SyncBeat.Instance.StartBeat();
        state = BattleState.PLAYERINPUT;
        PlayerInput();
    }

    //      BATTLELOOP FUNCTIONS       \\
    void PlayerInput()
    {
        if(!isGameOver)
        {
            playerParty.PrimeInput();
        }
    }
    public IEnumerator PlayerTurn()
    {
        //TEST

        SyncBeat.Instance.InactiveBeat();

        if(!isGameOver)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                playerUnits[i].gameObject.transform.position = playerforePositions[i].position;
                playerParty.PerformAction(i);
                yield return new WaitForSeconds(2f);
                ResetPosition();
            }
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator EnemyTurn()
    {

        if(!isGameOver)
        {
            for (int i = 0; i < enemyUnits.Count; i++)
            {
                enemyUnits[i].gameObject.transform.position = enemyforePositions[i].position;
                enemyParty.PerformAction(i);
                yield return new WaitForSeconds(2f);
                ResetPosition();
            }
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERINPUT;

            SyncBeat.Instance.StartBeat();

            PlayerInput();
        }
    }

    //      DAMAGE EXCHANGE FUNCTIONS       \\
    public void ExchangeDamage(int index, Attack attack, bool isMulti)
    {
        if(state == BattleState.PLAYERTURN) 
        {
            enemyUnits[index].gameObject.transform.position = enemyforePositions[index].position;
            enemyParty.Reaction(index, attack, isMulti);
        }
        else if(state == BattleState.ENEMYTURN) 
        {
            playerUnits[index].gameObject.transform.position = playerforePositions[index].position;
            playerParty.Reaction(index, attack, isMulti);
        }
    }
    void ResetPosition() 
    {
        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].gameObject.transform.position = playerPositions[i].position;
            enemyUnits[i].gameObject.transform.position = enemyPositions[i].position;
        }
    }

    //      WIN / LOSE CONDITIONS FUNCTIONS       \\
    void Update()
    {
        if(startCountDownTimer > 0)
        {
            startCountDownTimer -= Time.deltaTime;
            countDownTimerTxt.text = startCountDownTimer.ToString("0");;
        }

        if(state == BattleState.WON)
        {
            state = BattleState.WON;
            BattleResults(winScreen);
        }
        else if(state == BattleState.LOST)
        {
            state = BattleState.LOST;
            BattleResults(loseScreen);
        }
        if(Input.GetKeyDown (KeyCode.Escape))
        {
            Application.Quit();
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
                state = BattleState.LOST;
            }
        }
        else
        {
            enemyDeaths++;
            if(enemyDeaths >= 4)
            {
                isGameOver = true;
                state = BattleState.WON;
            }
        }
    }
    void BattleResults(CanvasGroup endingScreen)
    {
        endCountDownTimer += Time.deltaTime;
        endingScreen.alpha = endCountDownTimer / 1f;

        if(endCountDownTimer > 2f)
        {
            Application.Quit();
        }
    }
}
   
