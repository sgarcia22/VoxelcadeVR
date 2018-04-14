using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerTime : MonoBehaviour {

	public delegate void HammerRetrieved ();
	public static event HammerRetrieved gotHammer;

	void OnTriggerEnter (Collider c) {
		if (c.tag.Equals ("Player")) {
			if (gotHammer != null) {
				gotHammer ();
				gameObject.SetActive (false);
				Destroy (gameObject);
			}
		}
	}

}
