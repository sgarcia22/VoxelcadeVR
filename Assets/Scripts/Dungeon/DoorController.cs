using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag.Equals ("Corridor")) {
			gameObject.SetActive (false);
		}
	}
}
