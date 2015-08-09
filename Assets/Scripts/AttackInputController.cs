using UnityEngine;
using System.Collections;

public class AttackInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("WeakAttack"))
        {
            SendMessage("Attack", AttackType.WeakAttack);
        }

        if (Input.GetButtonDown("StrongAttack"))
        {
            SendMessage("Attack", AttackType.StrongAttack);
        }
    }
}
