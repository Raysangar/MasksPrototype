using UnityEngine;
using System.Collections;

public class detectPlayer : MonoBehaviour {


	void OnTriggerStay(Collider other)
	{
		this.SendMessageUpwards ("PlayerDetected", other);	
	}
}
