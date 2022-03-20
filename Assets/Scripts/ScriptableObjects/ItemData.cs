using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemDataClass", order = 2)]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemType;
    [TextArea]
    public string itemDescription;
    public int itemNo;
}
