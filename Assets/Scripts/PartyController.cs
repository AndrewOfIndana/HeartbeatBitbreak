using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public BattleMenuController battleMenu;
    public Conductor conducter;
    public PlayerCameras playerCamera; //Needed for the camera to move to the next player


    public enum InputStates {INACTIVE, BASIC, SELECTING, ITEMSELECTION} //A enum containing each and every input option based on states

    public InputStates inputOptions; //decleration of the current input option avaliable

    void Awake()
    {
        inputOptions = InputStates.BASIC;//sets inputoption to basic by default
    }

    public void PlayerInput(char playerIn)
    {
        if(inputOptions == InputStates.BASIC)
        {
            if(playerIn == 'w')
            {
                battleMenu.ButtonsClicked(0);
                inputOptions = InputStates.SELECTING;
            }
            else if(playerIn == 'a')
            {
                battleMenu.ButtonsClicked(1);
                inputOptions = InputStates.ITEMSELECTION;
            }
            else if(playerIn == 's')
            {
                battleMenu.ButtonsClicked(2);
                inputOptions = InputStates.INACTIVE;
            }
            else if(playerIn == 'd')
            {
                battleMenu.ButtonsClicked(3);
                inputOptions = InputStates.SELECTING;
            }
        }
        else if(inputOptions == InputStates.SELECTING)
        {
            if(playerIn == 'a')
            {
                battleMenu.ButtonsClicked(0);
                inputOptions = InputStates.INACTIVE;
                PlayerAttack(0);
            }
            else if(playerIn == 's')
            {
                battleMenu.ButtonsClicked(1);
                inputOptions = InputStates.INACTIVE;
                PlayerAttack(1);
            }
            else if(playerIn == 'd')
            {
                battleMenu.ButtonsClicked(2);
                inputOptions = InputStates.INACTIVE;
                PlayerAttack(2);
            }
            else if(playerIn == 'f')
            {
                battleMenu.ButtonsClicked(3);
                inputOptions = InputStates.INACTIVE;
                PlayerAttack(3);
            }
        }
        else if(inputOptions == InputStates.ITEMSELECTION)
        {
            if(playerIn == 'a')
            {
                battleMenu.ButtonsClicked(0);
                inputOptions = InputStates.INACTIVE;
                PlayerItem(0);
            }
            else if(playerIn == 's')
            {
                battleMenu.ButtonsClicked(1);
                inputOptions = InputStates.INACTIVE;
                PlayerItem(1);
            }
            else if(playerIn == 'd')
            {
                battleMenu.ButtonsClicked(2);
                inputOptions = InputStates.INACTIVE;
                PlayerItem(2);
            }
            else if(playerIn == 'f')
            {
                battleMenu.ButtonsClicked(3);    
                inputOptions = InputStates.INACTIVE;            
                PlayerItem(3);
            }
        }
    }

    public void PlayerAttack(int s)
    {
        Debug.Log("You attacked ememy " + (s+1));
        StartCoroutine(MoveCharacter(.2f));
    }
    public void PlayerItem(int s)
    {
        Debug.Log("You used item number " + (s+1));
        StartCoroutine(MoveCharacter(.2f));
    }

    IEnumerator MoveCharacter(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        inputOptions = InputStates.BASIC; 
        playerCamera.SwitchCamera();
        battleMenu.ResetMenu();
    }
}
