using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDeath : MonoBehaviour {

	void OnTriggerEnter (Collider c) {
		Debug.Log ("I is Burning");
		if (c.tag.Equals ("Player")) {
			PlayerStats ps = c.GetComponent <PlayerStats> ();

			ps.updateHealth (-2 * ps.getHealth ());

		}
	}
}
