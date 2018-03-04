using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moles : MonoBehaviour {

	public enum State {
		GOING_UP,
		GOING_DOWN,
		UP,
		DOWN
	}

	private State state;
	private Vector3 distanceToMove;
	private Vector3 originalPosition;
	public float handSpeed = 0.5f;

	void Start () {
		state = State.DOWN;
		originalPosition = gameObject.transform.position;
		distanceToMove = gameObject.transform.position;
		distanceToMove.y += 3f;
	}

	void Update () {
		switch (state) {
			case State.GOING_UP:
				GoUp ();
				break;
			case State.GOING_DOWN:
				GoDown ();
				break;
			//TODO::Do Something?
			case State.UP:
				break;
			case State.DOWN:
				break;
		}
	}

	private void GoDown() {
		if (gameObject.transform.position <= originalPosition) {
			gameObject.transform.position += gameObject.transform.up;
		} 
		else {
			state = State.DOWN;
		}
	}

	private void GoUp() {
		if (gameObject.transform.position != distanceToMove) {
			gameObject.transform.position += gameObject.transform.down;
		} 
		else {
			state = State.UP;
		}
	}

	void OnCollisionEnter (Collision col) {
		//Make sure the player is making the correct movements then call GoDown()
		if (col.collider.tag == "Player" && (State.UP || State.GOING_UP)) {
			Rigidbody hand = col.collider.gameObject.GetComponent<Rigidbody> ();
			if (col.collider.gameObject.name == "hand_left" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= handSpeed) {
				state = State.GOING_DOWN;
			}
			//Right Hand
			else if (col.collider.gameObject.name == "hand_right" && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= handSpeed) {
				state = State.GOING_DOWN;
			}
		}
	}
}
