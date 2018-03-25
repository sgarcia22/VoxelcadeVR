using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandControls : MonoBehaviour {

	public GameObject playerCamera;
	private Rigidbody hand;
	private bool goingFast;

	private float counter;
	private Vector3 previousPosition;
	public float speed = 0.5f;
	private GroundMoles molesScript;
	private Haptics haptics;

	void Start () {
		hand = gameObject.GetComponent<Rigidbody> ();
		goingFast = false;
		previousPosition = gameObject.transform.position;
		if (SceneManager.GetActiveScene ().name == "WackAMole") {
			molesScript = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GroundMoles> ();
		}
		haptics = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Haptics> ();
	}

	void Update () {
		//Debug.Log (gameObject.name + "   " + hand.velocity.magnitude);
	}

	void OnCollisionEnter (Collision col) {

		if (col.collider.tag == "Player") {
			Physics.IgnoreCollision (col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}

		///TODO: Left Hand not doing it correctly all of the times
		if (col.collider.tag == "Enemy" || col.collider.tag == "Mole") {
			//Left Hand
			if (gameObject.name == "hand_left" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= speed) {
				if (col.collider.tag == "Mole") {
					Moles mole = col.gameObject.GetComponent<Moles>();
					mole.state = Moles.State.GOING_DOWN;
					molesScript.molesHit += 1;
					haptics.vibrate (true);
				}
				//Destroy the enemy
				//Destroy (col.gameObject);
			}
			//Right Hand
			else if (gameObject.name == "hand_right" && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= speed) {
				if (col.collider.tag == "Mole") {
					Moles mole = col.gameObject.GetComponent<Moles>();
					mole.state = Moles.State.GOING_DOWN;
					molesScript.molesHit += 1;
					haptics.vibrate (false);
				}
				//Destroy the enemy
				//Destroy (col.gameObject);
			}
		}
	}
}
