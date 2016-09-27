using UnityEngine;
using System.Collections;

public class CursorSelect : MonoBehaviour
{
    public Camera userCamera;
    public Transform objectHit;
    public Transform joint;
    public float zScale = 0.05f;
    public float smoothing = 10;
    public float rotateScale = 0.5f;


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
        float xRotate = joint.transform.eulerAngles.z;
        if (xRotate > 180)
            xRotate -= 360;
        float yRotate = joint.transform.eulerAngles.z;
        if (yRotate > 180)
            yRotate -= 360;

        float zRotate = joint.transform.eulerAngles.z;
        if (zRotate > 180)
            zRotate -= 360;
        objectHit.Rotate(rotateScale * xRotate, rotateScale * yRotate, rotateScale * zRotate);
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
            float zMove = joint.transform.eulerAngles.z;
            if (zMove > 180)
                zMove -= 360;
            float scale = _hit.point.z / transform.position.z;
            Vector3 targetposition = new Vector3(transform.position.x * scale 
                - _hitRelativePosition.x, transform.position.y * scale 
                - _hitRelativePosition.y, objectHit.position.z
                - zScale*zMove);
            Debug.Log(zMove);
            objectHit.position = Vector3.Lerp(objectHit.position, targetposition, smoothing*Time.deltaTime);
        }
    }

    public void DeMoveObject()
    {
        _moveObjectFlag = false;
        objectHit = null;
    }
}
