using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hammer : MonoBehaviour {
	
	private Rigidbody hammer;
	private bool goingFast;

	private float counter;
	private Vector3 previousPosition;
	public float speed = 0.5f;
	private GroundMoles molesScript;
	private Haptics haptics;
	public GameObject temp;

	void Start () {
		temp = null;
		hammer = gameObject.GetComponent<Rigidbody> ();
		goingFast = false;
		previousPosition = gameObject.transform.position;
		if (SceneManager.GetActiveScene ().name == "WackAMole") {
			molesScript = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GroundMoles> ();
		}
		haptics = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Haptics> ();
	}

	void OnCollisionEnter (Collision col) {
		Debug.Log (col.collider.tag);
		if (col.collider.tag == "Mole") {
			Moles mole = col.gameObject.GetComponent<Moles>();
			if (col.collider.tag == "Mole" && (mole.state == Moles.State.GOING_UP) || (mole.state == Moles.State.UP)) {
				Debug.Log ("Destroy");
				mole.state = Moles.State.GOING_DOWN;
				if (col.gameObject != temp)
					temp = null;
				if (temp == null) {
					molesScript.molesHit += 1;
				}
				temp = col.gameObject;
				if (gameObject.name == "Left")
					haptics.vibrate (true);
				else
					haptics.vibrate (false);
			}
		}
		if (col.collider.tag == "Player") {
			Physics.IgnoreCollision (col.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}

	}
}
