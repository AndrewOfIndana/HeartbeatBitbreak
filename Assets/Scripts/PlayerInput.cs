using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PartyController party;

    void Awake() 
    {
        party = GetComponent<PartyController>();
    }
    void Update()
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
}
