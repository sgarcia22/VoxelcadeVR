using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControls : MonoBehaviour {

	public GameObject playerCamera;
	private Rigidbody hand;
	private bool goingFast;

	private float counter;
	private Vector3 previousPosition;
	public float speed = 0.5f;

	void Start () {
		hand = gameObject.GetComponent<Rigidbody> ();
		Debug.Log (hand);
		goingFast = false;
		previousPosition = gameObject.transform.position;
	}

	void Update () {
		//Debug.Log (gameObject.name + "   " + hand.velocity.magnitude);
	}

	void OnCollisionEnter (Collision col) {
		///TODO: Left Hand not doing it correctly all of the times
		if (col.collider.tag == "Enemy") {
			//Left Hand
			if (gameObject.name == "hand_left" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= speed) {
				Debug.Log ("Destroy");
				//Destroy the enemy
				//Destroy (col.gameObject);
			}
			//Right Hand
			else if (gameObject.name == "hand_right" && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= speed) {
				Debug.Log ("Destroy");
				//Destroy the enemy
				//Destroy (col.gameObject);
			}
		}
	}
}
