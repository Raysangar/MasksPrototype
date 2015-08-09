using UnityEngine;
using System.Collections;

public class ControllerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        string[] controllerNames = Input.GetJoystickNames();
        if (controllerNames.Length > 0)
        {
            Debug.Log(Input.GetButton("WeakAttack"));
        }
        else
            Debug.Log("Disconnected");
	}
}
