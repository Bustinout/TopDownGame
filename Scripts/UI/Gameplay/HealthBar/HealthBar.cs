using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class HealthBar : MonoBehaviour
{
    void Start()
    {
        StaticManager.healthbar = this;
        
    }

    private Heart[] hearts;

    // if current hp is greater than heart index, show black heart instead.
    public void initializeHearts()
    {
        //instantiate heart objects, assign an index
        for (int x = 0; x < StaticManager.player.GetComponent<Health>().CurrentHealth; x++)
        {
            var go = Instantiate(StaticManager.prefabmanager.heartPrefab, transform);
            go.GetComponent<Heart>().heartIndex = (x + 1);
        }

        hearts = GetComponentsInChildren<Heart>();
        refresh();
    }

    public void refresh()
    { 
        foreach (Heart x in hearts)
        {
            x.refresh();
        }
    }
}
