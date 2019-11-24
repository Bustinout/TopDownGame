using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StaticManager.SpecialEffectsParent = this.gameObject;
    }

}
