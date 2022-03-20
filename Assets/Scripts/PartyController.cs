using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public GameManager gameManager;
    public BattleUIController battleUI;
    public BattleMenuController battleMenu;
    public Conductor conducter;
    public PlayerController[] characters;

    public enum InputStates {INACTIVE, BASIC, DEFENDING, SKILLS, SELECTING, ITEMSELECTION} //A enum containing each and every input option based on states

    public InputStates inputOptions; //decleration of the current input option avaliable
    public int characterIndex;

    void Start()
    {
        characterIndex = 0;
    }

    public void PlayerInputStart()
    {
        characterIndex = 1;
        battleUI.SwitchUI(true);
        battleMenu.ResetMenu();
        inputOptions = InputStates.BASIC;//sets inputoption to basic by default
    }

    public void PlayerInput(char playerIn)
    {
        if (characters[(characterIndex-1)].isAlive == true) { 
            if(conducter.CheckHitTiming() > 0) //CheckingHitTiming returns a 0 for a miss so any integer greater than that indicates a sucessful hit
            {
                if (characterIndex < 5) //if all four characters have gone, then this will not work
                {
                    if (inputOptions == InputStates.BASIC)
                    {
                        if (playerIn == 'w')
                        {
                            PlayerBasic(0, InputStates.SELECTING);
                        }
                        else if (playerIn == 'a')
                        {
                            PlayerBasic(1, InputStates.ITEMSELECTION);
                        }
                        else if (playerIn == 's')
                        {
                            PlayerBasic(2, InputStates.DEFENDING);
                        }
                        else if (playerIn == 'd')
                        {
                            PlayerBasic(3, InputStates.SKILLS);
                        }
                    }
                    else if (inputOptions == InputStates.SELECTING || inputOptions == InputStates.SKILLS || inputOptions == InputStates.ITEMSELECTION)
                    {
                        if (playerIn == 'a')
                        {
                            PlayerProcess(0, inputOptions, 0);
                        }
                        else if (playerIn == 's')
                        {
                            PlayerProcess(1, inputOptions, 1);
                        }
                        else if (playerIn == 'd')
                        {
                            PlayerProcess(2, inputOptions, 2);
                        }
                        else if (playerIn == 'f')
                        {
                            PlayerProcess(3, inputOptions, 3);
                        }
                    }
                }
            }
            else
            {
            //TODO Implement the reseting of the flow meter
            }
        } else
        {
            StartCoroutine(NextCharacter(.2f));
        }
    }

    public void PlayerBasic(int menuSelection, InputStates selectionType)
    {
        inputOptions = selectionType;
        battleMenu.BasicOptionsMenu(menuSelection);

        if(selectionType == InputStates.DEFENDING)
        {
            characters[(characterIndex-1)].RecordAction(0 ,-1, -1);
            StartCoroutine(NextCharacter(.2f));
        }
    }

    public void PlayerProcess(int menuSelection, InputStates selectionType, int selection)
    {
        if(selectionType == InputStates.SELECTING)
        {
            battleMenu.SelectionOptionMenu(menuSelection);
            characters[(characterIndex-1)].RecordAction(1 ,selection, -1);
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == InputStates.SKILLS)
        {
            battleMenu.SelectionOptionMenu(menuSelection);
            characters[(characterIndex-1)].RecordAction(2 ,selection, selection);
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == InputStates.ITEMSELECTION)
        {
            battleMenu.ItemOptionMenu(menuSelection);
            characters[(characterIndex-1)].RecordAction(3 ,-1, selection);
            StartCoroutine(NextCharacter(.2f));
        }
    }

    IEnumerator NextCharacter(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        characterIndex++;

        if (characterIndex < 5) {
            inputOptions = InputStates.BASIC;
            battleUI.SwitchUI(true);
            battleMenu.ResetMenu();
        }
        else
        {
            yield return new WaitForSeconds(delayTime);
            characterIndex = 0;
            inputOptions = InputStates.INACTIVE;
            battleMenu.FinishMenu();
            battleUI.SwitchUI(false);
            gameManager.ActionPhase();
            PlayerOutput();
        }
    }

    void PlayerOutput() 
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].PerformAction();
            characters[i].ResetAction();
        }
        Invoke("DumbFunctionInvoke", 2f);
    }

    public void PartyEffect(int skill)
    {
        for(int i = 0; i < characters.Length; i++)
        {
            if(characters[i] != null)
            {
                if(skill == 2)
                {
                    characters[i].character.Heal();
                }
                else if(skill == 3)
                {
                    characters[i].character.AttackBoost();
                }

                else if(skill == 4)
                {
                    characters[i].character.DefenseBoost();
                }
            }
        }
    }

    void DumbFunctionInvoke()
    {
        gameManager.ReactionPhase();
    }

    public void RecieveAttacks(int index, Attack attack)
    {
        if(characters[index] != null)
            characters[index].Reaction(attack);
    }
}