using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPot : MonoBehaviour {

	private float healAmount;

	void Start () {
		healAmount = Random.Range (10, 90);
	}

	void OnTriggerEnter (Collider c) {

		if (c.tag.Equals ("Player")) {

			c.GetComponent <PlayerStats> ().updateHealth (healAmount);
			gameObject.SetActive (false);
			Destroy (gameObject);

		}
	}
}
