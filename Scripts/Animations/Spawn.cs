using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject model;
    public SpriteRenderer spawner;
    public bool floatFromGround;
    public float yPosStart;

    public bool fadeIn;

    private bool spawning;
    private float yPosEnd;
    private float deltaY;

    // Start is called before the first frame update
    void Start()
    {
        setSpawnerInvisible();

        if (floatFromGround)
        {
            yPosEnd = model.transform.localPosition.y;
            deltaY = (yPosEnd - yPosStart) / 10;


            model.transform.localPosition = new Vector3(model.transform.localPosition.x, yPosStart, model.transform.localPosition.z);
            StartCoroutine("fromGround");
        }
        if (fadeIn)
        {

        }
    }

    public void setSpawnerInvisible()
    {
        Color temp = spawner.color;
        temp.a = 0;
        spawner.color = temp;
        StartCoroutine("spawnerAppear");

    }


    IEnumerator fromGround()
    {
        while (model.transform.localPosition.y < yPosEnd - 0.02) //small float for inaccuracy of division in deltaY
        {
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + deltaY, model.transform.localPosition.z);
            yield return new WaitForSeconds(.1f);
        }
        gameObject.GetComponentInParent<AIBrain>().enabled = true;
    }

    IEnumerator spawnerAppear()
    {
        while (spawner.color.a < 1f)
        {
            Color temp = spawner.color;
            temp.a += 0.1f;
            spawner.color = temp;
            yield return new WaitForSeconds(.1f);
        }
        StartCoroutine("spawnerDisappear");
    }
    IEnumerator spawnerDisappear()
    {
        while (spawner.color.a > 0f)
        {
            Color temp = spawner.color;
            temp.a -= 0.2f;
            spawner.color = temp;
            yield return new WaitForSeconds(.1f);
        }
    }
}
