﻿using System.Collections;
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
	public GameObject temp;

	void Start () {
		temp = null;
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
		///TODO: Left Hand not doing it correctly all of the times
		if (col.collider.tag == "Enemy" || col.collider.tag == "Mole") {
			//Left Hand
			if (gameObject.name == "hand_left" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.1 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.1 /*&& hand.velocity.magnitude >= speed*/) {
				if (col.collider.tag == "Mole") {
					Debug.Log ("Destroy");
					Moles mole = col.gameObject.GetComponent<Moles> ();
					mole.state = Moles.State.GOING_DOWN;
					if (col.gameObject != temp)
						temp = null;
					if (temp == null) {
						molesScript.molesHit += 1;
					}
					temp = col.gameObject;
					haptics.vibrate (true);
				} else {
					Destroy (col.gameObject);
					haptics.vibrate (true);
				}
				//Destroy the enemy
				//Destroy (col.gameObject);
			}
			//Right Hand
			else if (gameObject.name == "hand_right" && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.1 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.1 /*&& hand.velocity.magnitude >= speed*/) {
				if (col.collider.tag == "Mole") {
					Moles mole = col.gameObject.GetComponent<Moles>();
					mole.state = Moles.State.GOING_DOWN;
					if (col.gameObject != temp)
						temp = null;
					if (temp == null) {
						molesScript.molesHit += 1;
					}
					temp = col.gameObject;
					haptics.vibrate (false);
				} else {
					Destroy (col.gameObject);
					haptics.vibrate (false);	
				}
				//Destroy the enemy
				//Destroy (col.gameObject);
			}
		}

		if (col.collider.tag == "Player") {
			Physics.IgnoreCollision (col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}

	}
}
