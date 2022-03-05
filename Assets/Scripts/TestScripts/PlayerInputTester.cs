using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTester : MonoBehaviour
{
    // READ FIRST
    //This is a script to test the battle menu controller, delete this and rework the battle menu controller when actaully working
    
    public BattleMenuController bmController;

    void Update() 
    {
        if(Input.GetKeyDown("w"))
        {
            bmController.ButtonsClicked(0);
        }
        else if(Input.GetKeyDown("d"))
        {
            bmController.ButtonsClicked(1);
        }
        else if(Input.GetKeyDown("s"))
        {
            bmController.ButtonsClicked(2);
        }
        else if(Input.GetKeyDown("a"))
        {
            bmController.ButtonsClicked(3);
        }
        else if(Input.GetKeyDown("space"))
        {
            bmController.ResetMenu();
        }
    } 
}
