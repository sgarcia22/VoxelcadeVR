using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	[SerializeField]
	private GameObject bossDoorCollider;

	private bool hasActivatedDoor = false;

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag.Equals ("Corridor")) {
			gameObject.SetActive (false);
		}
	}

	void FixedUpdate () {
		if (bossDoorCollider != null && !hasActivatedDoor && bossDoorCollider.activeInHierarchy) {
			GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
