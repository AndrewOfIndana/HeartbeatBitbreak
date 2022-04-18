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

    private List<PlayerUnit> characters = new List<PlayerUnit>();
    private PartyState battleOptions;
    private int characterIndex;
    
    private const int noAction = -1;

    //      START FUNCTIONS       \\
    public void SetPartyMembers(List<PlayerUnit> pUnits) 
    {
        characters = pUnits;
        battleOptions = PartyState.INACTIVE;
    }
    public void PrimeInput()
    {
        battleOptions = PartyState.BASIC;
        characterIndex = 0;
        battleMenu.ResetMenu();
        battleUI.SwitchUI(true);
    }

    //      INPUT MANAGER       \\
    public void CurrentInput(char inputs)
    {
        if(battleOptions != PartyState.INACTIVE)
        {
            if ((characters[characterIndex].isAlive) && (characterIndex < characters.Count)) 
            {
                if (conductor.CheckHitTiming() > 0)
                {
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
            } 
            else
            {
                StartCoroutine(NextCharacter(.2f));
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
            StartCoroutine(NextCharacter(.2f));
        }
    }
    void OptionsTwoBeats(int btnPress, PartyState selectionType)
    {
        if(selectionType == PartyState.SELECTING)
        {
            battleMenu.MenuOptionTwoBeats(btnPress);
            characters[characterIndex].RecordAction(Actions.ATTACK ,btnPress, noAction);
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == PartyState.SKILLSELECTING)
        {
            battleMenu.MenuOptionTwoBeats(btnPress);
            characters[characterIndex].RecordAction(Actions.SKILLS ,btnPress, noAction);
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == PartyState.ITEMSELECTING)
        {
            battleMenu.MenuOptionTwoBeats(btnPress);
            characters[characterIndex].RecordAction(Actions.ITEM ,noAction, btnPress);
            if(btnPress != noAction)
            {
                itemManager.EmptyItemName(btnPress);
            }
            StartCoroutine(NextCharacter(.2f));
        }
    }
    IEnumerator NextCharacter(float delayTime) 
    {
        yield return new WaitForSeconds(delayTime);
        characterIndex++;

        if (characterIndex < characters.Count) {
            battleOptions = PartyState.BASIC;
            battleMenu.ResetMenu();
            battleUI.SwitchUI(true);
        }
        else
        {
            yield return new WaitForSeconds(delayTime);
            battleOptions = PartyState.INACTIVE;
            battleMenu.FinishMenu();
            battleUI.SwitchUI(false);
            gameManager.state = BattleState.PLAYERTURN;
            gameManager.StartCoroutine(gameManager.PlayerTurn());
        }
    }

    //      PLAYERTURN FUNCTIONS       \\
    public void PerformAction(int chara)
    {
        if(characters[chara].actionIndex == Actions.DEFEND) //Defend
        {
            characters[chara].playerStats.DefenseBoost();
        }
        else if(characters[chara].actionIndex == Actions.ATTACK) //ATTACK
        {
            gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(gameManager.groove), false);
        }
        else if(characters[chara].actionIndex == Actions.SKILLS)
        {
            if(characters[chara].playerStats.playerSkill == 1) //STRONG ATTACK
            {
                characters[chara].playerStats.AttackBoost();
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(gameManager.groove), false);
            }
            if(characters[chara].playerStats.playerSkill == 2) //HEAL PARTY
            {
                PartyEffect(characters[chara].playerStats.playerSkill);
                characters[chara].playerStats.AttackWeak();
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(gameManager.groove), false);
                battleUI.UpdateHealth();
            }
            if(characters[chara].playerStats.playerSkill == 3)  //MUlTI ATTACK
            {
                characters[chara].playerStats.AttackSpread();
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(gameManager.groove), true);
            }
            if(characters[chara].playerStats.playerSkill == 4) //DEFEND PARTY
            {
                characters[chara].playerStats.AttackWeak();
                PartyEffect(characters[chara].playerStats.playerSkill);
                gameManager.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(gameManager.groove), false);
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
                    characters[i].playerStats.Heal();
                    battleUI.UpdateHealth();
                }
                else if(skill == 3)
                {
                    characters[i].playerStats.AttackBoost();
                }

                else if(skill == 4)
                {
                    characters[i].playerStats.DefenseBoost();
                }
            }
        }
    }
    
    //      ENEMYTURN FUNCTIONS       \\
    public void Reaction(int index, Attack attack, bool isMulti)
    {
        if(isMulti)
        {
            for(int i = 0; i < characters.Count; i++)
            {
                if(characters[i] != null)
                {
                    characters[i].playerStats.ReceiveAttack(attack);
                    battleUI.UpdateHealth();
                }
            }
        }
        else if(!isMulti)
        {
            if(characters[index] != null)
            {
                characters[index].playerStats.ReceiveAttack(attack);
                battleUI.UpdateHealth();
            }
        }
        for(int i = 0; i < characters.Count; i++) 
        {
            if((characters[i].playerStats.health <= 0) && characters[i].isAlive)
            {
                characters[i].isAlive = false;
                characters[i].gameObject.SetActive(false);
                gameManager.KillConfirmed(true);
            }
        }
    }
}
