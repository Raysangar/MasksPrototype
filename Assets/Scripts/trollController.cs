using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class trollController : MonoBehaviour {

	private Animator myAnimator;
	private bool isAttacking;
	private Rigidbody myRigidbody;
	private float currentLife;

	public int speed;
	public GameObject player;
	public Slider hpBar;
	public float totalLife=100f;

	void Start()
	{
		currentLife = totalLife;
		myAnimator = this.GetComponent<Animator> ();
		myRigidbody = this.GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		Vector3 direction = player.transform.position-this.transform.position;

		if (!isAttacking && direction.magnitude>0.5f) {
			this.transform.LookAt (player.transform.position);
			this.transform.rotation=Quaternion.Euler(0f,this.transform.rotation.eulerAngles.y,0f);
			myRigidbody.velocity=speed*this.transform.forward;
			myAnimator.SetBool("Move",true);
		}
        else {
            myRigidbody.velocity = Vector3.zero;
            myAnimator.SetBool("Move", false);
        }

	}
	void PlayerDetected(Collider other)
	{
		Attack ();
	}

	void ColliderHit(Collider other)
	{
		if (isAttacking) {

			other.SendMessage("ReceiveDamage",30);
			Debug.Log ("Player Hit");
		}
	}

	void Attack()
	{
		myAnimator.SetTrigger ("Attack");
		this.transform.position = new Vector3 (this.transform.position.x, 0.13f, this.transform.position.z);
	}

	void ImAttacking(bool state)
	{
		isAttacking = state;
	}

	void Dead()
	{
		Destroy (this.gameObject);

	}

	void ReceiveDamage(int damage)
	{
		currentLife -= damage;
		hpBar.value = currentLife / totalLife;
		if (currentLife <=0) {
			myAnimator.SetTrigger("Die");
		
		}
	}

}

