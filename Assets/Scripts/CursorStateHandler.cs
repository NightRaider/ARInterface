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
*************************************************************/



public class CursorStateHandler : MonoBehaviour
{
    public enum MyoPoses { Idle, MakeFist, FingerSpread, WaveIn, WaveOut }

    public GameObject myo = null;
    public CursorMaterialHandler materialHandler;
    public CursorMove cursorMove;
    public CursorSelect cursorSelect;

    private Pose _lastPose = Pose.Unknown;

    // Update is called once per frame
    void Update()
    {
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        if (thalmicMyo.pose != _lastPose)
        {
            
            if (thalmicMyo.pose == Pose.Fist)
            {
                materialHandler.SetMaterial((int)MyoPoses.MakeFist);
                cursorMove.CursorMoveActive = false;
                cursorSelect.SelectObject();
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                materialHandler.SetMaterial((int)MyoPoses.FingerSpread);
                cursorMove.CursorMoveActive = false;
                //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
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
                Debug.Log("waveout");
                cursorMove.CursorMoveActive = false;
                //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.DoubleTap)
            {
                //    GetComponent<Renderer>().material = doubleTapMaterial;

                //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else
            {
                materialHandler.SetMaterial((int)MyoPoses.Idle);
                cursorMove.CursorMoveActive = false;
                cursorSelect.DeSelectObject();
                
            }
            _lastPose = thalmicMyo.pose;

        }

        else if (thalmicMyo.pose == _lastPose)
        {
            if (thalmicMyo.pose == Pose.Fist)
            {
                
                cursorMove.CursorMoveActive = true;
            }
            else if (thalmicMyo.pose == Pose.FingersSpread)
            {
                
                cursorMove.CursorMoveActive = true;
            //    //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
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
                Debug.Log("waveout");
                cursorMove.CursorMoveActive = false;
                //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.DoubleTap)
            {
                //    GetComponent<Renderer>().material = doubleTapMaterial;

                //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else
            {
                materialHandler.SetMaterial((int)MyoPoses.Idle);
                cursorMove.CursorMoveActive = true;
            }

        }

        //private IEnumerator GearOn(IEnumerator coroutine)
        //{

        //}
    }
}
