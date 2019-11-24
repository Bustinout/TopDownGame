using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPositionChanger : MonoBehaviour
{
    private bool behind;

    public GameObject trueY;

    private void moveBehind(bool x)
    {
        if (x)
        {
            transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, -0.75f);
            behind = true;
        }
        else
        {
            transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, -5f);
            behind = false;
        }
    }

    private void Update()
    {
        //if (transform.position.y > StaticManager.player.transform.position.y)
        if (trueY.transform.position.y > StaticManager.player.transform.position.y)
        {
            if (!behind)
            {
                moveBehind(true);
            }
        }
        else
        {
            if (behind)
            {
                moveBehind(false);
            }
        }
    }
}
