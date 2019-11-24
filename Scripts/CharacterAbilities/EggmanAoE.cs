using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class EggmanAoE : Ability
{
    public GameObject AoE;
    public float delay = 0.25f;

    public CharacterHandleWeapon handle;


    public void useAbility()
    {
        Debug.Log("used");
        StartCoroutine("delayBeforeSpawn");
    }

    public void spawnAoE()
    {
        Vector3 target = handle.WeaponAttachment.GetComponent<locateTransform>().locatetransform.position;
        Instantiate(AoE, target, Quaternion.identity, StaticManager.ObjectPool.transform);
    }

    IEnumerator delayBeforeSpawn()
    {
        yield return new WaitForSeconds(delay);
        spawnAoE();
    }
}
