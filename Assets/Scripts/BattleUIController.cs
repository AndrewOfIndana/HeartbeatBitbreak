using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour
{
    public GameObject battleUI;
    public GameObject partyUI;
    public PlayerController[] characters;

    [Header("Prefabs")]
    public Sprite fullHeart;
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

            for(int i = 0; i < characterStatus[cs].heartNotes.Length; i++)
            {
                if(i < characterStatus[cs].currentHealth)
                {
                    characterStatus[cs].heartNotes[i].sprite = fullHeart;
                }
                else
                {
                    characterStatus[cs].heartNotes[i].sprite = emptyHeart;
                }
            }
        }
    }

    void Awake()
    {
        battleUI.SetActive(false);
        partyUI.SetActive(false);
    }

    public void SwitchUI(bool isInput)
    {
        if(isInput)
        {
            battleUI.SetActive(true);
            partyUI.SetActive(false);
        }
        if(!isInput)
        {
            battleUI.SetActive(false);
            partyUI.SetActive(true);
        }
    }
}
