using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    public BattleMenuController battleMenu;
    public Conductor conducter;

    public enum InputStates {BASIC, SELECTING, ITEMSELECTION, FINISH} //A enum containing each and every input option based on states

    public InputStates inputOptions; //decleration of the current input option avaliable

    void Awake()
    {
        inputOptions = InputStates.BASIC;//sets inputoption to basic by default
    }

    public void PlayerInput(int p)
    {
        if(inputOptions == InputStates.BASIC) //if inputoptions is in basic then these are the options given
        {
            if(p == 0 || p == 1) //if the input was w or d
            {
                battleMenu.ButtonsClicked(p);
                inputOptions = InputStates.SELECTING;
            }
            else if(p == 2) //if the input was a
            {
                battleMenu.ButtonsClicked(p);
                inputOptions = InputStates.FINISH;
            }
            else if(p == 3) //if the input was s
            {
                battleMenu.ButtonsClicked(p);
                inputOptions = InputStates.ITEMSELECTION;
            }
        }
        else if(inputOptions == InputStates.SELECTING) //if inputoptions is in basic then these are the options given
        {
            if(p == 0) //if the input was w or d
            {
                battleMenu.ButtonsClicked(p);
                inputOptions = InputStates.SELECTING;
            }
            else if(p == 2) //if the input was a
            {
                battleMenu.ButtonsClicked(p);
                inputOptions = InputStates.FINISH;
            }
            else if(p == 3) //if the input was s
            {
                battleMenu.ButtonsClicked(p);
                inputOptions = InputStates.ITEMSELECTION;
            }
        }
    }
}
