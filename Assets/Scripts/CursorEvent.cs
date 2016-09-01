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
public class CursorEvent : MonoBehaviour
{
    public GameObject myo = null;

    //enum for all poses, use bitmask to test state in other scrips
    [Flags]
    public enum MyoPoses
    {
        Idle = 0,
        MakeFist = 1,
        FingerSpread = 2,
        WaveIn = 4,
        WaveOut = 8
    }


    private Pose _lastPose = Pose.Unknown;


    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;
            if (thalmicMyo.pose == Pose.Fist)
            {
                
            }
            //else if (thalmicMyo.pose == Pose.WaveIn)
            //{
            //    GetComponent<Renderer>().material = waveInMaterial;

            //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            //}
            //else if (thalmicMyo.pose == Pose.WaveOut)
            //{
            //    GetComponent<Renderer>().material = waveOutMaterial;

            //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            //}
            //else if (thalmicMyo.pose == Pose.DoubleTap)
            //{
            //    GetComponent<Renderer>().material = doubleTapMaterial;

            //    ExtendUnlockAndNotifyUserAction(thalmicMyo);
            //}

        }

        //private IEnumerator GearOn(IEnumerator coroutine)
        //{

        //}
    }
}
