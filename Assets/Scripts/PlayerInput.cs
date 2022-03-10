using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameManager gameManager;
    private PartyController party;

    void Awake() 
    {
        party = GetComponent<PartyController>();
    }

    void Update()
    {
        if(gameManager.battleState == GameManager.GameStates.INPUT)
        {
            if(party.inputOptions == PartyController.InputStates.BASIC) 
            {
                if(Input.GetKeyDown("w"))
                {
                    party.PlayerInput('w');
                }
                else if(Input.GetKeyDown("a"))
                {
                    party.PlayerInput('a');
                }
                else if(Input.GetKeyDown("s"))
                {
                    party.PlayerInput('s');
                }
                else if(Input.GetKeyDown("d"))
                {
                    party.PlayerInput('d');
                }
            }
            else if(party.inputOptions == PartyController.InputStates.SELECTING || party.inputOptions == PartyController.InputStates.ITEMSELECTION || party.inputOptions == PartyController.InputStates.SKILLS) 
            {
                if(Input.GetKeyDown("a"))
                {
                    party.PlayerInput('a');
                }
                else if(Input.GetKeyDown("s"))
                {
                    party.PlayerInput('s');
                }
                else if(Input.GetKeyDown("d"))
                {
                    party.PlayerInput('d');
                }
                else if(Input.GetKeyDown("f"))
                {
                    party.PlayerInput('f');
                }
            }
        }
    }
}
