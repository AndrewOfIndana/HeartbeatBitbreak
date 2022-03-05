using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuController : MonoBehaviour
{   
    //THIS SCRIPT MANAGES THE MENU SYSTEM FOR BATTLE, FOR EACH CHARACTER
    [Header("Framework")]
    public Conductor conducter; //Needed for the visual element of a heart beat
    public PlayerCameras playerCamera; //Needed for the camera to move to the next player
    
    [Header("UI References")]
    public GameObject baseUIelement; //UI elements for base layer
    public GameObject itemUIelement; //UI elements for item layer
    public GameObject selectUIelement; //UI elements for selection layer

    [Header("Audio References")]
    public AudioSource selectSFX; //SFX for selecting a button
    public AudioSource selectFinishSFX; //SFX for selecting a finishing button;

    [Header("Image References")]
    public MenuLevel baseButtons; //buttons for the base level
    public MenuLevel itemButtons; //buttons for the items level
    public MenuLevel selectionButtons; //buttons for enemy selesctions

    [System.Serializable]
    public class MenuLevel //class for each button layer
    {
        public Image[] buttons; //image reference for each of the four buttons
    }

    private int levelBase; //the int for which button layer is active
    private Color transparent = new Color(255f,255f,255f,0f); //color of a transparent button
    private Color unclicked = new Color(255f,255f,255f,1f); //color of an unclicked button
    private Color clicked = new Color32(190,190,190,255); //color of a clicked button

    void Awake() 
    {
        StartCoroutine(MenuLevels(unclicked, transparent, transparent, true, false, false, 0, 0f)); //sets button layer to 0
    }

    IEnumerator MenuLevels(Color player, Color item, Color selection, bool baseUI, bool itemUI, bool selectUI,  int lbase, float delayTime) //determine which button layers are active and sets the levelBase int
    {
        yield return new WaitForSeconds(delayTime);
        levelBase = lbase; //sets the the button layer int
        baseUIelement.SetActive(baseUI); //set base ui elements true or false
        itemUIelement.SetActive(itemUI); //set item ui elements true or false
        selectUIelement.SetActive(selectUI); //set enemy select ui elements true or false

        for(int i = 0; i < 4; i++)
        {
            baseButtons.buttons[i].color = player; //either sets base buttons transparent or apparent
            itemButtons.buttons[i].color = item; //either sets item buttons transparent or apparent
            selectionButtons.buttons[i].color = selection; //either sets enemy selection buttons transparent or apparent
        }
    }

    public void ButtonsClicked(int btnInput) //Determine which buttons are clicked and takes a input from the player
    {
        if(levelBase == 0) //if the button level is 0 then the base player buttons are active and interactable
        {
            baseButtons.buttons[btnInput].color = clicked; //chosen button is grayed

            if(btnInput == 0 || btnInput == 1)  //if the player choses skills or attack
            {
                selectSFX.Play(); //Play select SFX
                StartCoroutine(MenuLevels(transparent, transparent, unclicked, false, false, true, 1, .1f)); //transition to the enemy select layer 
            }
            else if(btnInput == 2) //if the player choses defend
            {
                selectFinishSFX.Play(); //Play finish select SFX
                StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, 3, .1f)); //transition finished turn
            }
            else if(btnInput == 3) //if the player choses items
            {
                selectSFX.Play(); //Play select SFX
                StartCoroutine(MenuLevels(transparent, unclicked, transparent, false, true, false, 2, .1f)); //transition to the item select layer 
            }
        }
        else if(levelBase == 1) //if the button level is 1 then the enemy select buttons are active and interactable
        {
            selectionButtons.buttons[btnInput].color = clicked; //chosen button is grayed
            selectFinishSFX.Play(); //Play finish select SFX
            StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, 3, .1f));  //transition finished turn
        }
        else if(levelBase == 2)  //if the button level is 2 then the item select buttons are active and interactable
        {
            itemButtons.buttons[btnInput].color = clicked; //chosen button is grayed
            selectFinishSFX.Play(); //Play finish select SFX
            StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, 3, .1f));  //transition finished turn
        }
    }

    public void ResetMenu() //Resets button layer to next character
    {
        playerCamera.SwitchCamera();
        StartCoroutine(MenuLevels(unclicked, transparent, transparent, true, false, false, 0, 0f)); //sets button layer to 0
    }
    public void FinishMenu() //Finishes everything
    {
        playerCamera.SwitchCamera();
        StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, 0, 0f)); //sets button layer to 0
    }
}
