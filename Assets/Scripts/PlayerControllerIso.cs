using UnityEngine;
using System.Collections;

public class PlayerControllerIso : MonoBehaviour {


	private Animator myAnimator;
	private Rigidbody myRigidbody;
	private int comboCounter;

	private bool attack;
	private bool isAttacking;

	public int speed;
	// Use this for initialization
	void Start () {
	
		myAnimator = this.GetComponent<Animator> ();
		myRigidbody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Movement ();

		attack = Input.GetKeyDown (KeyCode.Space);
		Attack ();


		
	
	}
	void Movement()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if ((h != 0f || v != 0f) && !isAttacking) {
			myAnimator.SetBool("Run",true);
			this.transform.right = new Vector3 (v, 0f, -h).normalized;
			myRigidbody.velocity = new Vector3 (h, 0f, v) * speed;

		}
		else if(h==0f && v==0f){
			myAnimator.SetBool("Run",false);
			myRigidbody.velocity=Vector3.zero;
			
		}
	}

	void Attack()
	{
		if (attack) {
			myAnimator.SetTrigger("Jab");
			myAnimator.SetBool("Run",false);

		}
	}


	void ImAttacking(bool value)
	{
		isAttacking = value;
	}

	void ColliderHit(Collider other)
	{
		if (isAttacking) {
			Debug.Log("I Hit You");		
		}
	}




}
