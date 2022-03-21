using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    public CanvasGroup battleUI;
    public CanvasGroup partyUI;
    public PlayerController[] characters;

    [Header("Prefabs")]
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    [System.Serializable]
    public class status 
    {
        public int maxHealth;
        public int currentHealth;
        public Image[] heartNotes;
    }

    [Header("CharacterStatus")]
    public status[] characterStatus;

    public void UpdateHealth()
    {
        for(int cs = 0; cs < characterStatus.Length; cs++)
        {
            characterStatus[cs].maxHealth = characters[cs].character.max_health;
            characterStatus[cs].currentHealth = characters[cs].character.health;

            for(int i = 0; i < characterStatus[cs].heartNotes.Length*2; i++)
            {
                if((i-1) < characterStatus[cs].currentHealth)
                {
                    if(i == characterStatus[cs].currentHealth)
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

    void Awake()
    {
        battleUI.alpha = 0;
        partyUI.alpha = 0;
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
}
