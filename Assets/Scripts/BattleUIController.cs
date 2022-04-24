using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    [Header("Object References")]
    public CanvasGroup battleUI;
    public CanvasGroup partyUI;
    private List<PlayerUnit> characters = new List<PlayerUnit>();
    private List<EnemyUnit> enemies = new List<EnemyUnit>();

    [Header("Prefabs")]
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [System.Serializable]
    public class status 
    {
        public Image[] heartNotes;
    }

    [Header("CharacterStatus")]
    public status[] characterStatus;

    void Awake()
    {
        battleUI.alpha = 0;
        partyUI.alpha = 0;
    }
    public void SetUnitHealth(List<PlayerUnit> pUnits, List<EnemyUnit> eUnits) 
    {
        characters = pUnits;
        enemies = eUnits;
    }
    public void SwitchUI(bool isInput)
    {        
        if(isInput)
        {
            battleUI.alpha = 1f;
            partyUI.alpha = 0;
        }
        if(!isInput)
        {
            battleUI.alpha = 0;
            partyUI.alpha = 1f;
        }
    }
    public void UpdateHealth()
    {
        for(int cs = 0; cs < characterStatus.Length; cs++)
        {
            for(int i = 0; i < characterStatus[cs].heartNotes.Length*2; i++)
            {
                if((i-1) < characters[cs].healthStat)
                {
                    if(i == characters[cs].healthStat)
                    {
                        characterStatus[cs].heartNotes[i/2].sprite = halfHeart;
                    }
                    else
                    {
                        characterStatus[cs].heartNotes[i/2].sprite = fullHeart;
                    }
                }
                else
                {
                    characterStatus[cs].heartNotes[i/2].sprite = emptyHeart;
                }
            }
        }
    }
    public void UpdateEnemyHealth()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].healthBar.fillAmount = (((float)(enemies[i].healthStat)) / ((float)(enemies[i].enemyStats.health)));
        }
    }

}
