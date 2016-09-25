using UnityEngine;
using System.Collections;

public class CursorSelect : MonoBehaviour
{
    public Camera userCamera;
    public Transform objectHit;

    private bool _selectFlag = false;
    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (_selectFlag)
        {
            float scale = objectHit.position.z / transform.position.z;
            objectHit.position = new Vector3(transform.position.x*scale, transform.position.y*scale, objectHit.position.z);
        }
    }

    public bool SelectObject()
    {
        RaycastHit hit;
        if (Physics.Linecast(userCamera.transform.position, transform.position,out hit))
        {
            objectHit = hit.transform;
            _selectFlag = true;
        }
        else
        {
            _selectFlag = false;
        }
        return _selectFlag;
    }

    public void DeSelectObject()
    {
        _selectFlag = false;
        objectHit = null;
    }
}
