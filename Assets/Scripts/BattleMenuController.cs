using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleMenuController : MonoBehaviour
{   
    [Header("Object References")]
    public GameObject basicUIelement; //UI elements for base layer
    public GameObject selectUIelement; //UI elements for selection layer
    public GameObject itemSelectUIelement; //UI elements for item layer
    public TextMeshProUGUI[] enemyNamesTxt;

    [Header("Audio References")]
    public AudioSource selectSFX; //SFX for selecting a button
    public AudioSource selectFinishSFX; //SFX for selecting a finishing button;

    [Header("Image References")]
    public Image[] buttons; //image reference for each of the four buttons

    private Color unclicked = new Color(255f,255f,255f,1f); //color of an unclicked button
    private Color clicked = new Color32(190,190,190,255); //color of a clicked button
    private Color transparent = new Color(255f,255f,255f,0f); //color of a transparent button

    void Awake() 
    {
        FinishMenu();
    }

    public void SetEnemyNames(List<EnemyUnit> eUnits)
    {
        for(int i = 0; i < enemyNamesTxt.Length; i++)
        {
            enemyNamesTxt[i].text = eUnits[i].enemyStats.creatureName.ToString();
        }
    }

    public void MenuOptionOneBeat(int click)
    {
        buttons[click].color = clicked; //chosen button is grayed
            
        if(click == 0 || click == 3)  //if click is [w] or [d] move to enemy selection by default
        {
            selectSFX.Play(); //Play select SFX
            StartCoroutine(MenuChange(unclicked, false, true, false, .1f)); //transition to the enemy select layer 
        }
        else if(click == 1) //if click is [a] move to item selection
        {
            selectSFX.Play(); //Play select SFX
            StartCoroutine(MenuChange(unclicked, false, false, true, .1f)); //transition to the item select layer 
        }
        else if(click == 2) //if click is [s] finish turn
        {
            selectFinishSFX.Play(); //Play finish select SFX
            StartCoroutine(MenuChange(transparent, false, false, false, .1f)); //transition finished turn
        }
    }
    public void MenuOptionTwoBeats(int click)
    {
        buttons[click].color = clicked; //chosen button is grayed
        selectFinishSFX.Play(); //Play finish select SFX
        StartCoroutine(MenuChange(transparent, false, false, false, .1f));  //transition finished turn
    }

    IEnumerator MenuChange(Color btnColor, bool baseUI, bool selectUI, bool itemUI, float delayTime) //determine which button layers are active and sets the levelBase int
    {
        yield return new WaitForSeconds(delayTime);
        for(int i = 0; i < 4; i++)
        {
            buttons[i].color = btnColor; //either sets buttons transparent or apparent
        }
        basicUIelement.SetActive(baseUI); //set base ui elements true or false
        selectUIelement.SetActive(selectUI); //set enemy select ui elements true or false
        itemSelectUIelement.SetActive(itemUI); //set item ui elements true or false
    }

    public void ResetMenu() //Resets button layer to next character
    {
        StartCoroutine(MenuChange(unclicked, true, false, false, .1f));  //transition finished turn
    }
    public void FinishMenu() //Finishes everything
    {
        StartCoroutine(MenuChange(transparent, false, false, false, .1f));  //transition finished turn
    }
}
