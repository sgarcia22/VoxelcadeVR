using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorSelection : MonoBehaviour {

	[SerializeField]
	private List <GameObject> doors;

	public void pickDoor () {
		GameObject door;
		door = ((GameObject) doors [Random.Range (0, doors.Count)]);
		door.SetActive (true);
	}
}
