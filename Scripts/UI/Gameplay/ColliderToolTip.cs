using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderToolTip : MonoBehaviour
{
    public GameObject tooltip;
    public Text itemName;
    public Text itemDescription;


    private void Start()
    {
        tooltip.SetActive(false);
        itemName.text = gameObject.transform.parent.GetComponentInChildren<Collectable>().item.itemName;
        itemDescription.text = gameObject.transform.parent.GetComponentInChildren<Collectable>().item.itemDescription;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tooltip.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tooltip.SetActive(false);
        }
    }

}
