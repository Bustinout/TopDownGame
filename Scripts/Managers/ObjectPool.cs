using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    int objectCount;
    int maxObjectCount = 10; //when to start deleting
    private void Start()
    {
        StaticManager.ObjectPool = this;
        objectCount = 0;
    }



    public void addObject()
    {
        objectCount++;
        if (objectCount > maxObjectCount)
        {
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeInHierarchy)
                {
                    Destroy(child.gameObject);
                    objectCount--;
                }
            }
        }
    }
}
