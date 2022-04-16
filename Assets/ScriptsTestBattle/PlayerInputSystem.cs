using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    private PlayerParty party;

    void Awake() 
    {
        party = GetComponent<PlayerParty>();
    }

    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            party.CurrentInput('w');
        }
        else if(Input.GetKeyDown("a"))
        {
            party.CurrentInput('a');
        }
        else if(Input.GetKeyDown("s"))
        {
            party.CurrentInput('s');
        }
        else if(Input.GetKeyDown("d"))
        {
            party.CurrentInput('d');
        }
    }
}
