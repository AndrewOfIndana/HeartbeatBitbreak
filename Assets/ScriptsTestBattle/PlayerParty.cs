using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartyState { INACTIVE, BASIC, DEFENDING, SKILLSELECTING, SELECTING, ITEMSELECTING }
public enum Actions { WAITING, DEFEND, SKILLS, ATTACK, ITEM }

public class PlayerParty : MonoBehaviour
{
    public BattleSystem battleSystem;
    public List<PlayerUnit> characters = new List<PlayerUnit>();
    public Conductor conductor;
    public int characterIndex;
    public PartyState battleOptions;
    const int noAction = -1;

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
}
