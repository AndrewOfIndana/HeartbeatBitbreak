using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public ItemData itemStats;

    public bool ValidateItem()
    {
        if(!(itemStats == null))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ConsumeItem()
    {
        itemStats = null;
    }
}
