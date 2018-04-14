using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoundBossDoor : MonoBehaviour {

	public delegate void DoorFound (Vector3 position);
	public static event DoorFound found;

	[SerializeField]
	private Transform playerPosition;

	void Update () {
		if (Vector3.Distance (playerPosition.position, transform.position) < 5f && Mathf.Abs (playerPosition.position.y - transform.position.y) < 0.5f) {
			Debug.Log ("Found");
			found (new Vector3 (transform.position.x, 4.75f, transform.position.z));
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.collider.tag ==  "Player") {
			SceneManager.LoadScene ("WackAMole");
		}
	}
}

