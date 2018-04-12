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

	public State state;
	private Vector3 distanceToMove;
	private Vector3 originalPosition;
	public float handSpeed = 0.5f;
	private float timeToGoDown = 1f;
	private float currentTime = 0f;
	private float distance = .7f;
	private float moleSpeed = 2f;
	private bool activated = false;
	private bool moving = false;
	private GameObject gameManager;
	private GroundMoles manager;
	public bool hit = false;
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		manager = gameManager.GetComponent<GroundMoles> ();
		state = State.DOWN;
		originalPosition = gameObject.transform.position;
		distanceToMove = gameObject.transform.position;
		distanceToMove.y += distance;
	}

	void Update () {
		float random = Random.Range (7, 15);
		if (currentTime >= random && state == State.UP) {
			//Debug.Log ("ENTERING");
			state = State.GOING_DOWN;
			currentTime = 0;
		}
		switch (state) {
			case State.GOING_UP:
				if (gameObject.transform.position.y != distanceToMove.y && !activated) {
					activated = true;
					moving = true;
				} 
				else if (gameObject.transform.position.y == distanceToMove.y) {
					moving = false;
					activated = false;
					state = State.UP;
				}
				if (moving){
					float speed = Time.deltaTime * moleSpeed;
					transform.position = Vector3.MoveTowards(gameObject.transform.position, distanceToMove, speed);
				}
				break;
			case State.GOING_DOWN:
				if (gameObject.transform.position == distanceToMove && !activated) {
					activated = true;
					moving = true;
				} 
				else if (gameObject.transform.position == originalPosition) {
					moving = false;
					activated = false;
					for (int i = 0; i < manager.molesSurfaced.Count; ++i) {
						if (gameObject == manager.molesSurfaced [i]) {
							manager.molesSurfaced.RemoveAt (i);
							//Debug.Log ("Removing");
							break;
						}	
					}
					manager.currentMoles--;
					if (!hit) {
						hit = true;
						manager.molesHit += 1;
					}
					state = State.DOWN;
				}
				if (moving){
					float speed = Time.deltaTime * moleSpeed;
					transform.position = Vector3.MoveTowards(gameObject.transform.position, originalPosition, speed);
				}
				break;
			case State.UP:
				break;
		case State.DOWN:
			//if (GameObject.FindGameObjectWithTag ("Player").GetComponent<HandControls> ().temp == gameObject)
			//	GameObject.FindGameObjectWithTag ("Player").GetComponent<HandControls> ().temp = null;
			break;
		}
		currentTime += Time.deltaTime;
	}
	/*
	void OnCollisionEnter (Collision col) {
		//Make sure the player is making the correct movements then call GoDown()
		if (col.collider.tag == "Player" && (state == State.UP || state == State.GOING_UP)) {
			Rigidbody hand = col.collider.gameObject.GetComponent<Rigidbody> ();
			if (col.collider.gameObject.name == "hand_left" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= handSpeed) {
				state = State.GOING_DOWN;
			}
			//Right Hand
			else if (col.collider.gameObject.name == "hand_right" && OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.25 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.25 && hand.velocity.magnitude >= handSpeed) {
				state = State.GOING_DOWN;
			}
		}
	}*/
}
