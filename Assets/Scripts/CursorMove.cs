using UnityEngine;
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

    private bool _cursorMoveActive;
    private Vector3 _lastposition=Vector3.zero;

    public bool CursorMoveActive
    {
        get { return _cursorMoveActive; }
        set { _cursorMoveActive = value; }
    }

    public void ToggleActive()
    {
        _cursorMoveActive = !_cursorMoveActive;
    }

    void Start()
    {
        CursorMoveActive = false;
    }

    void Update()
    {
        if (_cursorMoveActive)
        {
            Vector3 projXY = new Vector3(stick.position.x, stick.position.y, 0);
            Vector3 targetZ = new Vector3(0, 0, startZ);
            if (Vector3.Distance(transform.position, screenScale * projXY + targetZ) > 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, _lastposition+screenScale * projXY + targetZ, smoothing * Time.deltaTime);
            }
        }
        else
        {
            _lastposition = new Vector3(transform.position.x,transform.position.y,0);
            Debug.Log(_lastposition);
        }
    }

}
