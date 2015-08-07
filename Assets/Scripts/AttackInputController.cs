using UnityEngine;
using System.Collections;

public class AttackInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Alpha1))
            SendMessage("Attack", AttackType.Circle);
	    
	    if (Input.GetKeyDown(KeyCode.Alpha2))
            SendMessage("Attack", AttackType.Square);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SendMessage("Attack", AttackType.Triangle);
    }
}
