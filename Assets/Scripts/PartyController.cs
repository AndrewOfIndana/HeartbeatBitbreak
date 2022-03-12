using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public GameManager gameManager;
    public BattleMenuController battleMenu;
    public Conductor conducter;
    public PlayerCameras playerCamera; //Needed for the camera to move to the next player
    public PlayerController[] characters;

    public enum InputStates {INACTIVE, BASIC, DEFENDING, SKILLS, SELECTING, ITEMSELECTION} //A enum containing each and every input option based on states

    public InputStates inputOptions; //decleration of the current input option avaliable
    public int characterIndex;

    void Start()
    {
        PlayerInputStart();
    }

    public void PlayerInputStart()
    {
        characterIndex = 1;
        battleMenu.ResetMenu();
        playerCamera.SwitchCamera(characterIndex);
        inputOptions = InputStates.BASIC;//sets inputoption to basic by default
    }

    public void PlayerInput(char playerIn)
    {
        if (conducter.CheckHitTiming() > 0) { //CheckingHitTiming returns a 0 for a miss so any integer greater than that indicates a sucessful hit
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

        } else
        {
            //TODO Implement the reseting of the flow meter
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
        playerCamera.SwitchCamera(characterIndex);

        if (characterIndex < 5) {
            inputOptions = InputStates.BASIC;
            battleMenu.ResetMenu();
        }
        else
        {
            yield return new WaitForSeconds(delayTime);
            characterIndex = 0;
            inputOptions = InputStates.INACTIVE;
            playerCamera.SwitchCamera(characterIndex);
            battleMenu.FinishMenu();
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

    void DumbFunctionInvoke()
    {
        gameManager.ReactionPhase();
    }
}