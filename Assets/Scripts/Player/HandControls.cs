using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControls : MonoBehaviour {

	public GameObject playerCamera;
	private Rigidbody hand;
	private bool goingFast;

	private float counter;
	private Vector3 previousPosition;

	// Use this for initialization
	void Start () {
		hand = gameObject.GetComponent<Rigidbody> ();
		goingFast = false;
		previousPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (counter >= 25 && gameObject.name == "hand_right") {
			Debug.Log (Vector3.Distance (gameObject.transform.position, previousPosition));
			if (Vector3.Distance (gameObject.transform.position, previousPosition) >= 0.5f) {
				Debug.Log ("Going FASTTTT");
				goingFast = true;
			} else {
				goingFast = false;
			}
			counter = 0;
			previousPosition = gameObject.transform.position;
		}
		counter++;

		/*if (gameObject.name == "hand_right")
			Debug.Log (hand.velocity.z);
		if (hand.velocity.z > 1) {
			//Debug.Log ("SUPER FAST");
			goingFast = true;
		} else {
			goingFast = false;
		}*/
	}

	void OnCollisionEnter (Collision col) {
		if (col.collider.tag == "Enemy") {
			//Left Hand
			if (gameObject.name == "hand_left" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.25 && goingFast) {
				//Destroy the enemy
				Destroy (col.gameObject);
			}
			//Right Hand
			else if (gameObject.name == "hand_right" && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.25 && goingFast) {
				Debug.Log ("Entering");
				//Destroy the enemy
				Destroy (col.gameObject);
			}
		}
	}
}
