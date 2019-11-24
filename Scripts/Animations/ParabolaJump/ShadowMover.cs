using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMover : MonoBehaviour
{
    public Transform cubeTransform;

    public Transform startPoint;
    public Transform endPoint;

    public GameObject tooltip;

    public ParabolaController pc;

    public float slope;
    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        tooltip.GetComponent<BoxCollider2D>().enabled = false;
        moveCompleted = false;
        slope = (endPoint.position.y - startPoint.position.y) / (endPoint.position.x - startPoint.position.x);
    }

    private bool moveCompleted;
    // Update is called once per frame
    void Update()
    {
        if (!moveCompleted)
        {
            if (pc.Animation)
            {
                float y = ((cubeTransform.position.x - startPoint.position.x) * slope) + startPoint.position.y;
                transform.position = new Vector3(cubeTransform.position.x, y, transform.position.z);
                tooltip.transform.position = transform.position;
            }
            else
            {
                transform.parent.GetComponentInChildren<ZPositionChanger>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
                tooltip.GetComponent<BoxCollider2D>().enabled = true;
                moveCompleted = true;
            }
        }
        
    }
}
