using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartyState { INACTIVE, BASIC, DEFENDING, SKILLSELECTING, SELECTING, ITEMSELECTING }
public enum Actions { WAITING, DEFEND, SKILLS, ATTACK, ITEM }

public class PlayerParty : MonoBehaviour
{
    [Header("External References")]
    public GameManager gameManager;
    public Conductor conductor;
    public ItemManager itemManager;
    public BattleMenuController battleMenu;
    public BattleUIController battleUI;
    public UIAudio AudioManager;
    public ScreenShake cameraShake;

    private List<PlayerUnit> characters = new List<PlayerUnit>();
    public PartyState battleOptions;
    public int characterIndex;
    private const int noAction = -1;
    private float checkTimer = .2f;
    const float switchTime = .2f;
    private bool isThisTurnOver;

    //      START FUNCTIONS       \\
    public void SetPartyMembers(List<PlayerUnit> pUnits) 
    {
        characters = pUnits;
        battleOptions = PartyState.INACTIVE;
    }
    public void PrimeInput()
    {
        battleOptions = PartyState.BASIC;
        for(int i = 0; i < characters.Count; i++)
        {
            characters[i].ResetAura();
        }
        characterIndex = 0;
        characters[characterIndex].gameObject.transform.position = gameManager.playerforePositions[characterIndex].position;
        battleMenu.ResetMenu();
        battleUI.SwitchUI(true);
        battleUI.UpdateHealth();
        battleUI.UpdateHud(gameManager.groove, characterIndex);
    }

    //      INPUT MANAGER       \\
    void Update() 
    {
        if(SyncBeat.TurnOverFlag && (battleOptions != PartyState.INACTIVE))
        {
            characterIndex = characters.Count;
            for(int i = 0; i < characters.Count; i++)
            {
                characters[i].gameObject.transform.position = gameManager.playerPositions[i].position;
            }
            battleOptions = PartyState.INACTIVE;
            battleMenu.FinishMenu();
            battleUI.SwitchUI(false);
            battleUI.UpdateHud(gameManager.groove, -1);
            gameManager.state = BattleState.PLAYERTURN;
            gameManager.StartCoroutine(gameManager.PlayerTurn());
        }
        else if (gameManager.state == BattleState.ENEMYTURN && SyncBeat.TurnOverFlag == true)
        {
            SyncBeat.TurnOverFlag = false;
        }

        if((battleOptions != PartyState.INACTIVE) && (characters[characterIndex].isAlive == false))
        {
            checkTimer -= Time.deltaTime;
            if(checkTimer <= 0)
            {
                checkTimer += switchTime;
                if(isThisTurnOver)
                {
                    isThisTurnOver = false;
                    StartCoroutine(NextCharacter(switchTime));
                }
            }
        }
    }

    public void CurrentInput(char inputs)
    {
        if(battleOptions != PartyState.INACTIVE)
        {
            if ((characters[characterIndex].isAlive) && (characterIndex < characters.Count)) 
            {
                if (conductor.CheckHitTiming() > 0)
                {
                    AudioManager.PlaySFX(UIAudio.SoundEffectTags.CORRECT_HIT);
                    if (battleOptions == PartyState.BASIC)
                    {
                        if (inputs == 'w')
                        {
                            OptionsOneBeat(0, PartyState.SELECTING);
                        }
                        else if (inputs == 'a')
                        {
                            OptionsOneBeat(1, PartyState.ITEMSELECTING);
                        }
                        else if (inputs == 's')
                        {
                            OptionsOneBeat(2, PartyState.DEFENDING);
                        }
                        else if (inputs == 'd')
                        {
                            OptionsOneBeat(3, PartyState.SKILLSELECTING);
                        }
                    }
                    else if ((battleOptions == PartyState.SELECTING) || (battleOptions == PartyState.SKILLSELECTING) || (battleOptions == PartyState.ITEMSELECTING))
                    {
                        if (inputs == 'w')
                        {
                            OptionsTwoBeats(0, battleOptions);
                        }
                        else if (inputs == 'a')
                        {
                            OptionsTwoBeats(1, battleOptions);
                        }
                        else if (inputs == 's')
                        {
                            OptionsTwoBeats(2, battleOptions);
                        }
                        else if (inputs == 'd')
                        {
                            OptionsTwoBeats(3, battleOptions);
                        }
                    }
                } 
                else
                {
                    AudioManager.PlaySFX(UIAudio.SoundEffectTags.MISSED_INPUT);
                }
            } 
            else
            {
                StartCoroutine(NextCharacter(switchTime));
            }
        }
    }
    void OptionsOneBeat(int btnPress, PartyState selectionType)
    {
        battleOptions = selectionType;
        battleMenu.MenuOptionOneBeat(btnPress);

        if(selectionType == PartyState.DEFENDING)
        {
            characters[characterIndex].RecordAction(Actions.DEFEND ,noAction, noAction);
            StartCoroutine(NextCharacter(switchTime));
        }
    }
    void OptionsTwoBeats(int btnPress, PartyState selectionType)
    {
        if(selectionType == PartyState.SELECTING)
        {
            battleMenu.MenuOptionTwoBeats(btnPress);
            characters[characterIndex].RecordAction(Actions.ATTACK ,btnPress, noAction);
            StartCoroutine(NextCharacter(switchTime));
        }
        else if(selectionType == PartyState.SKILLSELECTING)
        {
            battleMenu.MenuOptionTwoBeats(btnPress);

            if(gameManager.groove >= characters[characterIndex].playerStats.playerGrooveCost)
            {
                gameManager.groove -= characters[characterIndex].playerStats.playerGrooveCost;
                battleUI.UpdateHud(gameManager.groove, characterIndex);
                characters[characterIndex].RecordAction(Actions.SKILLS ,btnPress, noAction);
            }
            else
            {
                characters[characterIndex].RecordAction(Actions.ATTACK ,btnPress, noAction);
            }
            StartCoroutine(NextCharacter(switchTime));
        }
        else if(selectionType == PartyState.ITEMSELECTING)
        {
            battleMenu.MenuOptionTwoBeats(btnPress);
            characters[characterIndex].RecordAction(Actions.ITEM ,noAction, btnPress);
            if(btnPress != noAction)
            {
                itemManager.EmptyItemName(btnPress);
            }
            StartCoroutine(NextCharacter(switchTime));
        }
    }
    IEnumerator NextCharacter(float delayTime) 
    {
        yield return new WaitForSeconds(delayTime);
        characterIndex++;

        if (characterIndex < characters.Count) {
            isThisTurnOver = true;
            battleOptions = PartyState.BASIC;
            battleMenu.ResetMenu();
            battleUI.SwitchUI(true);
            battleUI.UpdateHud(gameManager.groove, characterIndex);
            characters[(characterIndex-1)].gameObject.transform.position = gameManager.playerPositions[(characterIndex-1)].position;
            characters[characterIndex].gameObject.transform.position = gameManager.playerforePositions[characterIndex].position;
        }
        else
        {
            battleOptions = PartyState.INACTIVE;
            isThisTurnOver = false;
            for(int i = 0; i < characters.Count; i++)
            {
                characters[i].gameObject.transform.position = gameManager.playerPositions[i].position;
            }
            yield return new WaitForSeconds(delayTime);
            battleMenu.FinishMenu();
            SyncBeat.TurnOverFlag = false;
            battleUI.SwitchUI(false);
            battleUI.UpdateHud(gameManager.groove, -1);
            gameManager.state = BattleState.PLAYERTURN;
            gameManager.StartCoroutine(gameManager.PlayerTurn());
        }
    }

    //      PLAYERTURN FUNCTIONS       \\
    public void PerformAction(int chara)
    {
        if(characters[chara].actionIndex == Actions.DEFEND) //Defend
        {
            characters[chara].DefenseBoost();
        }
        else if(characters[chara].actionIndex == Actions.ATTACK) //ATTACK
        {
            gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(characters[chara].attackStat, gameManager.groove), false);
            AudioManager.PlaySFX(UIAudio.SoundEffectTags.PLAYER_HIT);
        }
        else if(characters[chara].actionIndex == Actions.SKILLS)
        {
            if(characters[chara].playerStats.playerSkill == 1) //STRONG ATTACK
            {
                characters[chara].AttackBoost();
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(characters[chara].attackStat, gameManager.groove), false);
            }
            if(characters[chara].playerStats.playerSkill == 2) //HEAL PARTY
            {
                characters[chara].AttackSpread();
                PartyEffect(characters[chara].playerStats.playerSkill);
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(characters[chara].attackStat, gameManager.groove), false);
                battleUI.UpdateHealth();
            }
            if(characters[chara].playerStats.playerSkill == 3)  //MUlTI ATTACK
            {
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(characters[chara].attackStat, gameManager.groove), true);
            }
            if(characters[chara].playerStats.playerSkill == 4) //DEFEND PARTY
            {
                characters[chara].AttackLoss();
                PartyEffect(characters[chara].playerStats.playerSkill);
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(characters[chara].attackStat, gameManager.groove), false);
            }
        }
        else if(characters[chara].actionIndex == Actions.ITEM) 
        {
            ItemInstance usedItem = itemManager.SelectItem(characters[chara].itemIndex);
            bool doesItemExist = usedItem.ValidateItem();

            if(doesItemExist)
            {
                PartyEffect(usedItem.itemStats.itemNo);
                usedItem.ConsumeItem();
                battleUI.UpdateHealth();
            }
        }
        characters[chara].ResetAction();
        battleUI.UpdateHealth();
    }
    void PartyEffect(int skill)
    {
        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i] != null)
            {
                if(skill == 2)
                {
                    characters[i].Heal();
                    battleUI.UpdateHealth();
                }
                else if(skill == 3)
                {
                    characters[i].AttackBoost();
                }

                else if(skill == 4)
                {
                    characters[i].DefenseBoost();
                }
            }
        }
    }
    
    //      ENEMYTURN FUNCTIONS       \\
    public void Reaction(int index, Attack attack, bool isMulti)
    {
        if(characters[index].isAlive)
        {
            StartCoroutine(cameraShake.Shake(.2f, .1f));
            characters[index].healthStat -= characters[index].playerStats.GetDefense(characters[index].defenseStat, attack);
            Instantiate(characters[index].hitPrefab, characters[index].transform);
            battleUI.UpdateHealth();
        }

        for(int i = 0; i < characters.Count; i++) 
        {
            if((characters[i].healthStat <= 0) && characters[i].isAlive)
            {
                characters[i].isAlive = false;
                characters[i].gameObject.SetActive(false);
                gameManager.KillConfirmed(true);
            }
        }
    }
}
