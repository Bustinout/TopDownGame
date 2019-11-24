using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public int interactableID;

    private void Start()
    {
        interactableID = Random.Range(100000, 999999);
    }

    public virtual void use()
    {

    }


    public GameObject interactButton;
    public void showInteractButton()
    {
        interactButton.SetActive(true);
    }

    public void hideInteractButton()
    {
        interactButton.SetActive(false);
    }

}
