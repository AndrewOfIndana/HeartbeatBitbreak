using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public ItemInstance[] items;
    public TextMeshProUGUI[] itemNamesTxt; 

    void Awake()
    {
        ResetItemName();
    }

    public ItemInstance SelectItem(int select)
    {
        return items[select];
    }

    public void EmptyItemName(int select)
    {
        //itemNamesTxt[select].text = "empty";
    }

    void ResetItemName()
    {
        for(int i = 0; i < itemNamesTxt.Length; i++)
        {
            itemNamesTxt[i].text = items[i].itemStats.itemName;
        }
    }
}
