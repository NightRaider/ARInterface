using UnityEngine;
using System.Collections;

public class CursorSelect : MonoBehaviour
{
    public Camera userCamera;
    public Transform objectHit;
    public Transform joint;
    public float zScale = 0.05f;
    public float speed = 10;

    public float smoothing = 10;
    public float rotateScale = 0.05f;


    private static bool _moveObjectFlag = false;
    private static bool _moveObjectInPlaneFlag = false;
    private RaycastHit _hit;
    private Vector3 _hitRelativePosition;
    private Rigidbody _inPlaneRB;
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
            if (objectHit.tag == "Moveable")
            {
                _hitRelativePosition = _hit.point - objectHit.position;
                _moveObjectFlag = true;
            }
            else if (objectHit.tag == "MoveInPlane")
            {
                _inPlaneRB = objectHit.GetComponent<Rigidbody>();
                _hitRelativePosition = _hit.point - objectHit.position;
                _moveObjectInPlaneFlag = true;
            }
            
        }
    }

    public void SelectObject()
    {
        if (objectHit != null&&objectHit.tag=="Moveable")
        {
            float xRotate = degreeToFloat(joint.transform.eulerAngles.x);

            float yRotate = degreeToFloat(joint.transform.eulerAngles.y);

            Debug.Log(joint.transform.eulerAngles.z);
            float zRotate = degreeToFloat(joint.transform.eulerAngles.z);

            Debug.Log("x" + xRotate);
            Debug.Log("y" + yRotate);
            Debug.Log("z" + zRotate);
            objectHit.Rotate(rotateScale * (xRotate==0?0:xRotate-15), 
                rotateScale * (yRotate==0?0:yRotate-15), 
                rotateScale * (zRotate==0?0:zRotate-5),Space.World);
            StatusUIManager.AppStatus = objectHit.name+" Selected";
        }
        else if (objectHit!=null&&objectHit.tag == "MoveInPlane")
        {
            float xRotate = 0;

            float yRotate = degreeToFloat(joint.transform.eulerAngles.y);

            float zRotate = 0;

            objectHit.Rotate(rotateScale * (xRotate == 0 ? 0 : xRotate - 15),
                rotateScale * (yRotate == 0 ? 0 : yRotate - 15),
                rotateScale * (zRotate == 0 ? 0 : zRotate - 5), Space.Self);
            StatusUIManager.AppStatus = objectHit.name + " Selected";
        }

    }

    public void DeSelectObject()
    {
        if (objectHit != null)
            objectHit = null;
        StatusUIManager.AppStatus = "Cursor Moving";
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
            if (zMove < 10 && zMove > -10)
                zMove = 0;
            float scale = _hit.point.z / transform.position.z;
            Vector3 targetposition = new Vector3(transform.position.x * scale 
                - _hitRelativePosition.x, transform.position.y * scale 
                - _hitRelativePosition.y, objectHit.position.z
                - zScale*zMove);
            Debug.Log("Move");
            objectHit.position = Vector3.Lerp(objectHit.position, targetposition, smoothing*Time.deltaTime);
            StatusUIManager.AppStatus = objectHit.name + " Moving";
        }

        else if (_moveObjectInPlaneFlag == true)
        {
            _inPlaneRB.constraints = RigidbodyConstraints.FreezeRotation;
            float zMove = 0;
            float scale = _hit.point.z / transform.position.z;
            Vector3 targetposition = new Vector3(transform.position.x * scale
                - _hitRelativePosition.x, transform.position.y * scale
                - _hitRelativePosition.y, objectHit.position.z
                - zScale * zMove);
            Vector3 relativeMoveInViewFrustum = targetposition - objectHit.position;
            float angle = Vector3.Angle(relativeMoveInViewFrustum, objectHit.up);
            // Calculate angle between movme vector in camera view plane and normal of design space plane. 
            // Use this angle to project to calculate difference vector between original movement
            // and its projection on design plane.
            Vector3 targetOnPlaneDirection=relativeMoveInViewFrustum-(relativeMoveInViewFrustum.magnitude* 
                Mathf.Cos(angle* Mathf.PI / 180))*objectHit.up;
            float targetOnPlaneMagnitude = relativeMoveInViewFrustum.magnitude / Mathf.Sin(angle* Mathf.PI / 180);
            Vector3 targetOnPlaneVector = targetOnPlaneDirection.normalized * targetOnPlaneMagnitude;
            Debug.Log(targetOnPlaneVector);
            //objectHit.position = Vector3.Lerp(objectHit.position, targetOnPlane+objectHit.position, smoothing * Time.deltaTime);
            StatusUIManager.AppStatus = objectHit.name + " Moving";
            //_inPlaneRB.MovePosition(targetOnPlaneVector+objectHit.position);
            // Using velocity has less jittering than MovePosition
            _inPlaneRB.velocity = (targetOnPlaneVector)*speed;
        }
        
    }

    public void DeMoveObject()
    {
        // FreezeAll causes sinking between objects of similar mass. Engine issue
        _inPlaneRB.constraints = RigidbodyConstraints.FreezeAll;
        _moveObjectFlag = false;
        _moveObjectInPlaneFlag = false;
        objectHit = null;
        StatusUIManager.AppStatus ="Cursor Moving";
    }

    // Change euler angle from 0 - 360 to -180 to 180
    private float degreeToFloat(float angle)
    {
        if (angle > 180 && angle < 345)
            return angle - 360;
        else if (angle > 15 && angle < 180)
            return angle;
        else
            return 0;
    }
}
