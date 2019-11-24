using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaRoot : MonoBehaviour
{
    //automatically sets the parabola root points
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;

    private float[] posneg = new float[] { 1f, -1f };

    public Vector3 generateEndPoint()
    {
        float xOffset = (Random.Range(3f, 5f)) * posneg[Random.Range(0, 2)];
        float yOffset = (Random.Range(3f, 4f));

        Vector3 temp = new Vector3(transform.position.x + xOffset, transform.position.y - yOffset, transform.position.z);



        if (temp.y < StaticManager.currentRoom.botwall.transform.position.y)//if below bot wall, flip up
        {
            temp.y += (yOffset * 2f);
        }
        //case of above top wall should never happen

        if (temp.x > StaticManager.currentRoom.rightwall.transform.position.x || temp.x < StaticManager.currentRoom.leftwall.transform.position.x)
        {
            temp.x += (xOffset * -2f);
        }


        return temp;
    }

    private void Start()
    {
        point1.transform.position = transform.position;
        point3.transform.position = generateEndPoint();

        point2.transform.position = new Vector3( point1.transform.position.x + ((point3.transform.position.x - point1.transform.position.x)/2f) , point3.transform.position.y + 3f, transform.position.z);

    }


}
