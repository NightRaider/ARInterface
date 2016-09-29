using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusUIManager : MonoBehaviour {
    Text text;
    private static string _appStatus="Cursor Moving";
    public static string AppStatus
    {
        get
        {
            return _appStatus;
        }
        set
        {
            _appStatus = value;
        }
    }
	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = _appStatus;
    }

}
