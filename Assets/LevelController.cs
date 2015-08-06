using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Application.LoadLevel (0);
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
		
			Application.LoadLevel (1);
		}
		else if (Input.GetKeyDown (KeyCode.Escape)) {
			
			Application.Quit();
		}
	
	}
}
