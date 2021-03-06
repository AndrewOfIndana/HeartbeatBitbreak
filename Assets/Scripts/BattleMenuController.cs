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
    public CanvasGroup[] buttons; //image reference for each of the four buttons
    public GameObject feedback; //image reference for each of the four buttons

    private const float unclicked = .5f;
    private const float clicked = 1f;
    private const float transparent = 0; //color of a transparent button

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
    public void UpdateEnemyNames(List<EnemyUnit> eUnits)
    {
        for(int i = 0; i < enemyNamesTxt.Length; i++)
        {
            if(eUnits[i].isAlive == false)
            {
                enemyNamesTxt[i].text = "Slain";
            }
        }
    }

    public void MenuOptionOneBeat(int click)
    {
        buttons[click].alpha = clicked; //chosen button is grayed
            
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
        buttons[click].alpha = clicked; //chosen button is grayed
        Instantiate(feedback, buttons[click].transform);
        selectFinishSFX.Play(); //Play finish select SFX
        StartCoroutine(MenuChange(transparent, false, false, false, .1f));  //transition finished turn
    }

    IEnumerator MenuChange(float btnColor, bool baseUI, bool selectUI, bool itemUI, float delayTime) //determine which button layers are active and sets the levelBase int
    {
        yield return new WaitForSeconds(delayTime);
        for(int i = 0; i < 4; i++)
        {
            buttons[i].alpha = btnColor; //either sets buttons transparent or apparent
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
