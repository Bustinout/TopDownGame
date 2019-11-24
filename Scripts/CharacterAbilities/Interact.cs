using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(SaveLoad.current.pcInput[5]))
        {
            if (GetComponent<Status>().checkMovable())
            {
                if (targetedInteractable != null)
                {
                    targetedInteractable.use();
                }
            }
                
        }
    }

    static Interactable targetedInteractable;
    public int targetedInteractableID;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            if (targetedInteractable != null)
            {
                targetedInteractable.hideInteractButton();
            }
            targetedInteractable = other.GetComponent<Interactable>();
            targetedInteractableID = other.GetComponent<Interactable>().interactableID;
            other.gameObject.GetComponent<Interactable>().showInteractButton();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Interactable>() != null)
        {
            if (other.gameObject.GetComponent<Interactable>().interactableID == targetedInteractableID)
            {
                targetedInteractable = null;
                other.gameObject.GetComponent<Interactable>().hideInteractButton();
            }
        }
    }

}
