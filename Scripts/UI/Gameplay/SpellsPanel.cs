using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;


public class SpellsPanel : MonoBehaviour
{
    public AbilityIcon [] abilities;
    public GameObject abilityIconPrefab;

    private void Start()
    {
        StaticManager.spellsPanel = this;
        initializeAbilityIcons();
    }

    public void initializeAbilityIcons()
    {
        for (int x = 0; x < 3; x++)
        {
            var go = Instantiate(abilityIconPrefab, transform);
            go.GetComponent<AbilityIcon>().abilityIndex = x;
            abilities[x] = go.GetComponent<AbilityIcon>();
        }
    }
}
