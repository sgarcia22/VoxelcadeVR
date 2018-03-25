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
		if (Vector3.Distance (playerPosition.position, transform.position) < 5f) {
			found (new Vector3 (transform.position.x, 4f, transform.position.z));
		}
	}

	void onTriggerEnter (Collider c) {
		Debug.Log ("Hit SomeThing");
		if (c.tag.Equals ("Player")) {
			Debug.Log ("Hit Player");
			SceneManager.LoadScene ("WackAMole");
		}
	}
}
