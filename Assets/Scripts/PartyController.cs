using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public BattleMenuController battleMenu;
    public Conductor conducter;
    public PlayerCameras playerCamera; //Needed for the camera to move to the next player


    public enum InputStates {INACTIVE, BASIC, DEFENDING, SKILLS, SELECTING, ITEMSELECTION} //A enum containing each and every input option based on states

    public InputStates inputOptions; //decleration of the current input option avaliable
    public int characterIndex = 1;

    void Start()
    {
        playerCamera.SwitchCamera(characterIndex);
        inputOptions = InputStates.BASIC;//sets inputoption to basic by default
    }

    public void PlayerInput(char playerIn)
    {
        if (conducter.CheckHitTiming() > 0) {
            if (characterIndex < 5)
            {
                if (inputOptions == InputStates.BASIC)
                {
                    if (playerIn == 'w')
                    {
                        battleMenu.BasicOptionsMenu(0);
                        inputOptions = InputStates.SELECTING;
                    }
                    else if (playerIn == 'a')
                    {
                        battleMenu.BasicOptionsMenu(1);
                        inputOptions = InputStates.ITEMSELECTION;
                    }
                    else if (playerIn == 's')
                    {
                        battleMenu.BasicOptionsMenu(2);
                        inputOptions = InputStates.DEFENDING;
                        PlayerProcess(inputOptions, 0);
                    }
                    else if (playerIn == 'd')
                    {
                        battleMenu.BasicOptionsMenu(3);
                        inputOptions = InputStates.SKILLS;
                    }
                }
                else if (inputOptions == InputStates.SELECTING)
                {
                    if (playerIn == 'a')
                    {
                        battleMenu.SelectionOptionMenu(0);
                        PlayerProcess(inputOptions, 0);
                    }
                    else if (playerIn == 's')
                    {
                        battleMenu.SelectionOptionMenu(1);
                        PlayerProcess(inputOptions, 1);
                    }
                    else if (playerIn == 'd')
                    {
                        battleMenu.SelectionOptionMenu(2);
                        PlayerProcess(inputOptions, 2);
                    }
                    else if (playerIn == 'f')
                    {
                        battleMenu.SelectionOptionMenu(3);
                        PlayerProcess(inputOptions, 3);
                    }
                }
                else if (inputOptions == InputStates.SKILLS)
                {
                    if (playerIn == 'a')
                    {
                        battleMenu.SelectionOptionMenu(0);
                        PlayerProcess(inputOptions, 0);
                    }
                    else if (playerIn == 's')
                    {
                        battleMenu.SelectionOptionMenu(1);
                        PlayerProcess(inputOptions, 1);
                    }
                    else if (playerIn == 'd')
                    {
                        battleMenu.SelectionOptionMenu(2);
                        PlayerProcess(inputOptions, 2);
                    }
                    else if (playerIn == 'f')
                    {
                        battleMenu.SelectionOptionMenu(3);
                        PlayerProcess(inputOptions, 3);
                    }
                }

                else if (inputOptions == InputStates.ITEMSELECTION)
                {
                    if (playerIn == 'a')
                    {
                        battleMenu.ItemOptionMenu(0);
                        PlayerProcess(inputOptions, 0);
                    }
                    else if (playerIn == 's')
                    {
                        battleMenu.ItemOptionMenu(1);
                        PlayerProcess(inputOptions, 1);
                    }
                    else if (playerIn == 'd')
                    {
                        battleMenu.ItemOptionMenu(2);
                        PlayerProcess(inputOptions, 2);
                    }
                    else if (playerIn == 'f')
                    {
                        battleMenu.ItemOptionMenu(3);
                        PlayerProcess(inputOptions, 3);
                    }
                }
            }

        }
    }

    public void PlayerProcess(InputStates selectionType, int selection)
    {
        if(selectionType == InputStates.DEFENDING)
        {
            Debug.Log("YOU DEFEND");
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == InputStates.SELECTING)
        {
            Debug.Log("YOU ATTACK ENEMY " + (selection+1));
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == InputStates.SKILLS)
        {
            Debug.Log("YOU ATTACK ENEMY " + (selection+1));
            StartCoroutine(NextCharacter(.2f));
        }
        else if(selectionType == InputStates.ITEMSELECTION)
        {
            Debug.Log("YOU USE ITEM ON CHARACTER " + (selection+1));
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
            StartCoroutine(FinishTurn(0));
        }
    }
    IEnumerator FinishTurn(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        characterIndex = 0;
        playerCamera.SwitchCamera(characterIndex);
        inputOptions = InputStates.INACTIVE;
        battleMenu.FinishMenu();
        StartCoroutine(NextCharacter(5f));
    }
}
