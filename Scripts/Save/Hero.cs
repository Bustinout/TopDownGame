using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hero
{
    public string heroName;
    public int deathYear;

    public Hero()
    {
        heroName = Library.randomizeHeroName();
    }

    public List<Item> inventory = new List<Item>();
    //basic stats
    public int primaryDamage = 0;
    public float primartyAtkSped = 0;
    public int secondaryDamage = 0;
    public float secondaryCD = 0;
    public int hpUP = 0;
    public int hpDown = 0;
    public float critChance = 0;
    public float invulnTime = 2f;

    public void pickUpItem(Item x)
    {
        bool found = false;
        foreach (Item y in inventory)
        {
            if (x.itemName == y.itemName)
            {
                found = true;
                y.count += 1;
            }
        }
        if (!found)//should include first add 
        {
            inventory.Add(x);
        }

        sortInventory();
        //recalc stats (without applying midway ex/ reseting 0hp and dying)
    }

    public void sortInventory()
    {
        List<Item> newList = new List<Item>();

        if (inventory.Count > 0)
        {
            newList.Add(inventory[0]);
            for (int x = 1; x < inventory.Count; x++)
            {
                bool added = false;
                for (int y = 0; y < newList.Count; y++)
                {
                    if (inventory[x].rarity > newList[y].rarity)
                    {
                        newList.Insert(y, inventory[x]);
                        added = true;
                        break;
                    }
                    else if (inventory[x].rarity == newList[y].rarity)
                    {
                        if (inventory[x].itemName.CompareTo(newList[y].itemName) < 0)
                        {
                            newList.Insert(y, inventory[x]);
                        }
                    }
                }
                if (!added)
                {
                    newList.Add(inventory[x]);
                }
            }
        }

        inventory = newList;
    }


    public void calculateStatsFromInventory()
    {
        resetStats();

        foreach (Item x in inventory)
        {
            addItemStats(x);
        }
    }

    public void resetStats()
    {
        primaryDamage = 0;
        primartyAtkSped = 0;
        secondaryDamage = 0;
        secondaryCD = 0;
        hpUP = 0;
        hpDown = 0;
        critChance = 0;
    }

    public void addItemStats(Item x)
    {
        primaryDamage += x.primaryDamage * x.count;
        primartyAtkSped += x.primartyAtkSped * x.count;
        secondaryDamage += x.secondaryDamage * x.count;
        secondaryCD += x.secondaryCD * x.count;
        hpUP += x.hpUP * x.count;
        hpDown += x.hpDown * x.count;
        critChance += x.critChance * x.count;
    }


    public string[] equippedAbilitiesIcons = new string[] { "Sprites/SpellIcons/Eggman/bleedattack", "Sprites/SpellIcons/Eggman/lightningstrikes", "Sprites/SpellIcons/Eggman/rejuvenatinganger" };
    public string[] abilityNames = new string[] { "Blast Em", "Whip Move", "Dash Move" };
    public string[] abilityDescription = new string[] { "Shoot your gun.", "Enemies hit at the end are stunned.", "Do the thing." };


    public int[] equippedWeapons = new int[] { 0, 0, 0, 0, 0, 0 };
    //^ which weapon is equipped for each ability
    //Weapons
    // 0 - gun
    // 1 - fire
    // 2 - bow
    // 3 - sword
    // possible more weapons - ice, arcane, throwing
    public int[] weaponSkills = new int[] { 0, 0, 0, 0 };

    //Weapon Bias
    // 0 - -
    // 1 - normal
    // 2 - +
    // 3 - ++
    public int[] weaponBias = new int[] { 1, 1, 1, 1 };

}
