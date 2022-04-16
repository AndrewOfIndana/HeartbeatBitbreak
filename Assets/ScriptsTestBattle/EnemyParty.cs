using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParty : MonoBehaviour
{
    public List<EnemyUnit> enemies = new List<EnemyUnit>();

    public void SetPartyMembers(List<EnemyUnit> eUnits) 
    {
        enemies = eUnits;
    }
}
