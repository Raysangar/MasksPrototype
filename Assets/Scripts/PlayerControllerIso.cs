using UnityEngine;
using System.Collections;

public class PlayerControllerIso : MonoBehaviour {


	private Animator myAnimator;
	private Rigidbody myRigidbody;
	public int speed;
	// Use this for initialization
	void Start () {
	
		myAnimator = this.GetComponent<Animator> ();
		myRigidbody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		if (h != 0 || v != 0) {
			myAnimator.SetBool("Run",true);
			this.transform.right = new Vector3 (v, 0, -h).normalized;

		}
		else{
			myAnimator.SetBool("Run",false);

		}
		myRigidbody.velocity = new Vector3 (h, 0, v) * speed;
	
	
	}
}
