using System;
using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

/*************************************************************
Check Pose for armband. Make sure execute before CursorMove and
other scripts that needs Poses
    **This is essentailly a state machine

This sceipt also work as EventManager, invoke events like 
OnMakeFistClick()... The behaviour will be implemented in 
other scripts.
*************************************************************/



public class CursorStateHandler : MonoBehaviour
{
    public enum MyoPoses { Idle, MakeFist, FingerSpread, WaveIn, WaveOut }

    public GameObject myo = null;
    public CursorMaterialHandler materialHandler;
    public CursorMove cursorMove;
    public CursorSelect cursorSelect;

    private Pose _lastPose = Pose.Unknown;

    // Events for poses
    public delegate void ClickAction();
    public static event ClickAction OnMakeFistDown;
    public static event ClickAction OnMakeFistUp;
    public static event ClickAction OnMakeFistClick;
    public static event ClickAction OnFingerSpreadDown;
    public static event ClickAction OnFingerSpreadUp;
    public static event ClickAction OnFingerSpreadClick;

    public delegate void DragAction();
    public static event DragAction OnMakeFistDrag;
    public static event DragAction OnFingerSpreadDrag;

    // Time limit for click and drag
    private float clickTimerLimit = .5f;
    private float _timer = 0f;
    ThalmicMyo thalmicMyo;



    void Start()
    {
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
        // Set MoveInPlane objects constraints
        GameObject[] designSpaceObjects = GameObject.FindGameObjectsWithTag("MoveInPlane");
        foreach (GameObject obj in designSpaceObjects)
        {
            Debug.Log(obj);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        GameObject[] designSpaceEnvironment = GameObject.FindGameObjectsWithTag("DesignSpaceEnvironment");
        foreach (GameObject obj in designSpaceEnvironment)
        {
            Debug.Log(obj);
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (thalmicMyo.pose != _lastPose)
        {
            if (thalmicMyo.pose == Pose.Fist)
            {
                materialHandler.SetMaterial((int)MyoPoses.MakeFist);
                _timer = 0;
                if (OnMakeFistDown != null)
                {
                    OnMakeFistDown();
                }

                cursorMove.CursorMoveActive = false;

            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                materialHandler.SetMaterial((int)MyoPoses.FingerSpread);
                _timer = 0;
                cursorMove.CursorMoveActive = false;
            }
            else if (thalmicMyo.pose == Pose.WaveIn)
            {
                materialHandler.SetMaterial((int)MyoPoses.WaveIn);
                cursorMove.CursorMoveActive = false;
            }
            else if (thalmicMyo.pose == Pose.WaveOut)
            {
                materialHandler.SetMaterial((int)MyoPoses.WaveOut);
                cursorMove.CursorMoveActive = false;
            }
            else if (thalmicMyo.pose == Pose.DoubleTap)
            {

            }
            else
            {
                materialHandler.SetMaterial((int)MyoPoses.Idle);
                if (_lastPose == Pose.FingersSpread)
                {
                    if (isClick())
                        if (OnFingerSpreadClick != null)
                            OnFingerSpreadClick();
                }
                cursorMove.CursorMoveActive = false;

            }
            _lastPose = thalmicMyo.pose;

        }

        else if (thalmicMyo.pose == _lastPose)
        {

            if (thalmicMyo.pose == Pose.Fist)
            {
                _timer += Time.deltaTime;
                cursorMove.CursorMoveActive = true;
                // This would cause the object to jump
                if (!isClick())
                {
                    if (OnMakeFistDrag != null)
                    {
                        OnMakeFistDrag();
                        Debug.Log(OnMakeFistDrag);
                    }
                }
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                _timer += Time.deltaTime;
                cursorMove.CursorMoveActive = true;
                if (OnFingerSpreadDrag != null)
                    OnFingerSpreadDrag();
            }
            else if (thalmicMyo.pose == Pose.WaveIn)
            {
                materialHandler.SetMaterial((int)MyoPoses.WaveIn);
                cursorMove.CursorMoveActive = false;
                //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.WaveOut)
            {
                materialHandler.SetMaterial((int)MyoPoses.WaveOut);
                cursorMove.CursorMoveActive = false;
                //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.DoubleTap)
            {

            }
            else
            {
                materialHandler.SetMaterial((int)MyoPoses.Idle);
                cursorMove.CursorMoveActive = true;
                if (isClick())
                {
                    if (OnMakeFistClick != null)
                        OnMakeFistClick();

                }
                else
                {
                    if (OnMakeFistUp != null)
                        OnMakeFistUp();
                }

            }
        }
    }


    private bool isClick()
    {
        return _timer < clickTimerLimit;
    }
}
