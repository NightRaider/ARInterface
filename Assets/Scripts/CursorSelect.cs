using UnityEngine;
using System.Collections;

public class CursorSelect : MonoBehaviour
{
    public Camera userCamera;
    public Transform objectHit;

    private bool _selectFlag = false;
    private RaycastHit _hit;
    private Vector3 _hitRelativePosition;
    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (_selectFlag)
        {
            
            float scale = _hit.point.z / transform.position.z;
            objectHit.position = new Vector3(transform.position.x*scale-_hitRelativePosition.x, transform.position.y*scale-_hitRelativePosition.y, objectHit.position.z);
        }
    }

    public bool SelectObject()
    {
        
        if (Physics.Linecast(userCamera.transform.position, transform.position,out _hit))
        {
            objectHit = _hit.transform;
            _hitRelativePosition = _hit.point - objectHit.position;
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
