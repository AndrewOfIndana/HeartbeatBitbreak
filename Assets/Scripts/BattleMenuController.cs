using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuController : MonoBehaviour
{   
    //THIS SCRIPT MANAGES THE MENU SYSTEM FOR BATTLE, FOR EACH CHARACTER
    private Conductor conducter; //Needed for the visual element of a heart beat
    
    [Header("UI References")]
    public GameObject basicUIelement; //UI elements for base layer
    public GameObject selectUIelement; //UI elements for selection layer
    public GameObject itemSelectUIelement; //UI elements for item layer

    [Header("Audio References")]
    public AudioSource selectSFX; //SFX for selecting a button
    public AudioSource selectFinishSFX; //SFX for selecting a finishing button;

    [Header("Image References")]
    public MenuLevel baseButtons; //buttons for the base level
    public MenuLevel selectionButtons; //buttons for enemy selesctions
    public MenuLevel itemButtons; //buttons for the items level

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
        conducter = GetComponent<Conductor>(); //from the start get the conductor script
    }

    IEnumerator MenuLevels(Color player, Color selection, Color item, bool baseUI, bool selectUI, bool itemUI, float delayTime) //determine which button layers are active and sets the levelBase int
    {
        yield return new WaitForSeconds(delayTime);
        basicUIelement.SetActive(baseUI); //set base ui elements true or false
        selectUIelement.SetActive(selectUI); //set enemy select ui elements true or false
        itemSelectUIelement.SetActive(itemUI); //set item ui elements true or false

        for(int i = 0; i < 4; i++)
        {
            baseButtons.buttons[i].color = player; //either sets base buttons transparent or apparent
            itemButtons.buttons[i].color = item; //either sets item buttons transparent or apparent
            selectionButtons.buttons[i].color = selection; //either sets enemy selection buttons transparent or apparent
        }
    }
    public void BasicOptionsMenu(int click)
    {
        baseButtons.buttons[click].color = clicked; //chosen button is grayed
            
        if(click == 0 || click == 3)  //if click is [w] or [d] move to enemy selection by default
        {
            selectSFX.Play(); //Play select SFX
            StartCoroutine(MenuLevels(transparent, unclicked, transparent, false, true, false, .1f)); //transition to the enemy select layer 
        }
        else if(click == 1) //if click is [a] move to item selection
        {
            selectSFX.Play(); //Play select SFX
            StartCoroutine(MenuLevels(transparent, transparent, unclicked, false, false, true, .1f)); //transition to the item select layer 
        }
        else if(click == 2) //if click is [s] finish turn
        {
            selectFinishSFX.Play(); //Play finish select SFX
            StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, .1f)); //transition finished turn
        }
    }

    public void SelectionOptionMenu(int click)
    {
        selectionButtons.buttons[click].color = clicked; //chosen button is grayed
        selectFinishSFX.Play(); //Play finish select SFX
        StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, .1f));  //transition finished turn
    }

    public void ItemOptionMenu(int click)
    {
        itemButtons.buttons[click].color = clicked; //chosen button is grayed
        selectFinishSFX.Play(); //Play finish select SFX
        StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, .1f));  //transition finished turn
    }

    public void ResetMenu() //Resets button layer to next character
    {
        StartCoroutine(MenuLevels(unclicked, transparent, transparent, true, false, false, 0f)); //sets button layer to 0
    }
    public void FinishMenu() //Finishes everything
    {
        StartCoroutine(MenuLevels(transparent, transparent, transparent, false, false, false, 0f)); //sets button layer to 0
    }
}
