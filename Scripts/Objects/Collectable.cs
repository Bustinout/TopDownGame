using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    public Item item;
    private GameObject parent;
    public GameObject pickUpEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SaveLoad.current.currentHero.pickUpItem(item);
            StaticManager.inventorydisplay.refreshItems();

            if (pickUpEffect != null)
            {
                Instantiate(pickUpEffect, this.transform.position, Quaternion.identity, StaticManager.SpecialEffectsParent.transform);
            }

            despawn();
        }
    }

    public void despawn()
    {

        Destroy(parent);
        //for now
        //destroy parent
    }
}
