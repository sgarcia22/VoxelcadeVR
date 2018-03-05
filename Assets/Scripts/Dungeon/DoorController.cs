using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	void OnCollisionEnter (Collision c) {
		Debug.Log ("I was Called" + c.gameObject.tag);
		if (c.gameObject.tag.Equals ("Corridor")) {
			gameObject.SetActive (false);
		}
	}
}
