using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartyState { INACTIVE, BASIC, DEFENDING, SKILLSELECTING, SELECTING, ITEMSELECTING }
public enum Actions { WAITING, DEFEND, SKILLS, ATTACK, ITEM }

public class PlayerParty : MonoBehaviour
{
    [Header("External References")]
    public BattleSystem battleSystem;
    public Conductor conductor;
    public ItemManager itemManager;

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
    }

    //      INPUT MANAGER       \\
    public void CurrentInput(char inputs)
    {
        if(battleOptions != PartyState.INACTIVE)
        {
            if ((characters[characterIndex].isAlive) && (characterIndex < characters.Count)) 
            {
                //if (conductor.CheckHitTiming() > 0)
                //{
                    if (battleOptions == PartyState.BASIC)
                    {
                        if (inputs == 'w')
                        {
                            OptionsBasic(0, PartyState.SELECTING);
                        }
                        else if (inputs == 'a')
                        {
                            OptionsBasic(1, PartyState.ITEMSELECTING);
                        }
                        else if (inputs == 's')
                        {
                            OptionsBasic(2, PartyState.DEFENDING);
                        }
                        else if (inputs == 'd')
                        {
                            OptionsBasic(3, PartyState.SKILLSELECTING);
                        }
                    }
                    else if ((battleOptions == PartyState.SELECTING) || (battleOptions == PartyState.SKILLSELECTING) || (battleOptions == PartyState.ITEMSELECTING))
                    {
                        if (inputs == 'w')
                        {
                            OptionsSelection(0, battleOptions);
                        }
                        else if (inputs == 'a')
                        {
                            OptionsSelection(1, battleOptions);
                        }
                        else if (inputs == 's')
                        {
                            OptionsSelection(2, battleOptions);
                        }
                        else if (inputs == 'd')
                        {
                            OptionsSelection(3, battleOptions);
                        }
                    }
                //}
            } 
            else
            {
                StartCoroutine(NextCharacter(.2f));
            }
        }
    }
    void OptionsBasic(int btnPress, PartyState selectionType)
    {
        battleOptions = selectionType;

        if(selectionType == PartyState.DEFENDING)
        {
            characters[characterIndex].RecordAction(Actions.DEFEND ,noAction, noAction);
            StartCoroutine(NextCharacter(.2f));
        }
    }
    void OptionsSelection(int btnPress, PartyState selectionType)
    {
        if(selectionType == PartyState.SELECTING)
        {
            characters[characterIndex].RecordAction(Actions.ATTACK ,btnPress, noAction);
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == PartyState.SKILLSELECTING)
        {
            characters[characterIndex].RecordAction(Actions.SKILLS ,btnPress, noAction);
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == PartyState.ITEMSELECTING)
        {
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
        }
        else
        {
            yield return new WaitForSeconds(delayTime);
            battleOptions = PartyState.INACTIVE;
            battleSystem.state = BattleState.PLAYERTURN;
            battleSystem.StartCoroutine(battleSystem.PlayerTurn());
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
            battleSystem.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(battleSystem.groove), false);
        }
        else if(characters[chara].actionIndex == Actions.SKILLS)
        {
            if(characters[chara].playerStats.playerSkill == 1) //STRONG ATTACK
            {
                characters[chara].playerStats.AttackBoost();
                battleSystem.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(battleSystem.groove), false);
            }
            if(characters[chara].playerStats.playerSkill == 2) //HEAL PARTY
            {
                PartyEffect(characters[chara].playerStats.playerSkill);
                characters[chara].playerStats.AttackWeak();
                battleSystem.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(battleSystem.groove), false);
                //battleUI.UpdateHealth();
            }
            if(characters[chara].playerStats.playerSkill == 3)  //MUlTI ATTACK
            {
                characters[chara].playerStats.AttackSpread();
                battleSystem.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(battleSystem.groove), true);
            }
            if(characters[chara].playerStats.playerSkill == 4) //DEFEND PARTY
            {
                characters[chara].playerStats.AttackWeak();
                PartyEffect(characters[chara].playerStats.playerSkill);
                battleSystem.ExchangeDamage(characters[chara].attackIndex, characters[chara].playerStats.GetAttack(battleSystem.groove), false);
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
                //battleUI.UpdateHealth();
            }
        }
        characters[chara].ResetAction();
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
                }
            }
        }
        else if(!isMulti)
        {
            if(characters[index] != null)
            {
                characters[index].playerStats.ReceiveAttack(attack);
            }
        }
        for(int i = 0; i < characters.Count; i++) 
        {
            if((characters[i].playerStats.health <= 0) && characters[i].isAlive)
            {
                characters[i].isAlive = false;
                characters[i].gameObject.SetActive(false);
            }
        }
    }
}
