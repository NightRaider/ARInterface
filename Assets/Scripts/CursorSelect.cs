using UnityEngine;
using System.Collections;

public class CursorSelect : MonoBehaviour
{
    public Camera userCamera;
    public Transform objectHit;
    public Transform joint;

    private static bool _moveObjectFlag = false;
    private RaycastHit _hit;
    private Vector3 _hitRelativePosition;
    // Use this for initialization
    void Start()
    {
        DefaultCursorBehaviour();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void DefaultCursorBehaviour()
    {
        CursorStateHandler.OnMakeFistDown += CastRay;
        CursorStateHandler.OnMakeFistUp += DeMoveObject;
        CursorStateHandler.OnMakeFistClick += SelectObject;
        CursorStateHandler.OnMakeFistDrag += MoveObject;
        CursorStateHandler.OnFingerSpreadClick += DeSelectObject;
        CursorStateHandler.OnFingerSpreadDrag += OpenProperty;
    }

    public void CastRay()
    {
        if (Physics.Linecast(userCamera.transform.position, transform.position, out _hit))
        {
            objectHit = _hit.transform;
            _hitRelativePosition = _hit.point - objectHit.position;
            _moveObjectFlag = true;
        }
    }

    public void SelectObject()
    {

    }

    public void DeSelectObject()
    {

    }

    public void OpenProperty()
    {

    }

    public void MoveObject()
    {
        if(_moveObjectFlag == true)
        {
            float scale = _hit.point.z / transform.position.z;
            objectHit.position = new Vector3(transform.position.x * scale 
                - _hitRelativePosition.x, transform.position.y * scale 
                - _hitRelativePosition.y, objectHit.position.z);
        }
    }

    public void DeMoveObject()
    {
        _moveObjectFlag = false;
        objectHit = null;
    }
}
