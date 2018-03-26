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
		if (Vector3.Distance (GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 5f) {
			//found (new Vector3 (transform.position.x, 4f, transform.position.z));
		}
	}

	void OnCollisionEnter (Collision col) {
		Debug.Log ("Hit SomeThing");
		if (col.collider.tag ==  "Player") {
			Debug.Log ("Hit Player");
			SceneManager.LoadScene ("WackAMole");
		}
	}
}

