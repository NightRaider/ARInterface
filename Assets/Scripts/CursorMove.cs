﻿using UnityEngine;
using System.Collections;

/**************************************************
    Attach to cursor object
**************************************************/

public class CursorMove : MonoBehaviour
{

    public Transform stick;
    public float screenScale = 1.0f;
    public float smoothing = 1.0f;
    public int startZ = 1999;

    private bool cursorMoveActive;

    public bool CursorMoveActive
    {
        get { return cursorMoveActive; }
        set { cursorMoveActive = value; }
    }

    public void ToggleActive()
    {
        cursorMoveActive = !cursorMoveActive;
    }

    void Start()
    {
        CursorMoveActive = true;
    }

    void Update()
    {
        if (cursorMoveActive)
        {
            Vector3 projXY = new Vector3(stick.position.x, stick.position.y, 0);
            Vector3 targetZ = new Vector3(0, 0, startZ);
            if (Vector3.Distance(transform.position, screenScale * projXY + targetZ) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, screenScale * projXY + targetZ, smoothing * Time.deltaTime);
            }
        }
    }

}
