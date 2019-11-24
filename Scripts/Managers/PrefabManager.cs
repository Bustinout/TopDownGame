using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class PrefabManager : MonoBehaviour
{
    private void Start()
    {
        StaticManager.prefabmanager = this;
    }

    public GameObject heartPrefab;
    public GameObject damageTextPrefab;
    public GameObject critTextPrefab;
    public GameObject stunTextPrefab;
    public GameObject burnTextPrefab;
    public GameObject poisonTextPrefab;

    public GameObject[] itemPrefabs;


    public GameObject randomItem() //temporary uniform equal choosing of item in array
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)];
    }


    //Weapons Prefabs
    public Weapon[] primaryWeapons = new Weapon[4];

}
