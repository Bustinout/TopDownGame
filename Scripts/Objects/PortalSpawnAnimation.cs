using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawnAnimation : MonoBehaviour
{
    public Transform quadTransform;
    public Transform shadowTransform;

    public BoxCollider2D shadowCollider;

    private Vector2 originalScaleQuad;
    private Vector2 originalScaleShadow;


    private void OnEnable()
    {
        portalSpawnAnimation();
    }

    public void portalSpawnAnimation()
    {
        originalScaleQuad = new Vector2(quadTransform.localScale.x, quadTransform.localScale.y);
        originalScaleShadow = new Vector2(shadowTransform.localScale.x, shadowTransform.localScale.y);

        quadTransform.localScale = new Vector3(0, 0, quadTransform.localScale.z);
        shadowTransform.localScale = new Vector3(0, 0, shadowTransform.localScale.z);

        counter = 1;
        StartCoroutine("scaleUp");
        //play sound
    }


    private int counter = 1;
    private float speed = 20f;
    IEnumerator scaleUp()
    {
        while (counter <= speed)
        {
            quadTransform.localScale = new Vector3((originalScaleQuad.x/speed)*counter, (originalScaleQuad.y/speed)*counter, quadTransform.localScale.z);
            shadowTransform.localScale = new Vector3((originalScaleShadow.x/speed)*counter, (originalScaleShadow.y/speed)*counter, shadowTransform.localScale.z);


            counter++;
            yield return new WaitForSeconds(0.01f);
        }
        shadowCollider.enabled = true;
        //play sound
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
