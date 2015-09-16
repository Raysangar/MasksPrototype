using UnityEngine;
using System.Collections;

public class detectPlayer : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        this.SendMessageUpwards("PlayerDetected", other);
    }

	void OnTriggerStay(Collider other)
	{
		this.SendMessageUpwards ("PlayerDetected", other);
	}
}
