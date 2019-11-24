using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customCursor : MonoBehaviour
{
    public bool AlwaysOn;
    // Start is called before the first frame update
    void Start()
    {
        StaticManager.customcursor = this;

        Cursor.visible = false;
        transform.gameObject.SetActive(AlwaysOn);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
