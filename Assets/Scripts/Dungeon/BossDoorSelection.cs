using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorSelection : MonoBehaviour {

	private static List <GameObject> doors = new List<GameObject> ();

	public void pickDoor () {
		GameObject door;
		door = ((GameObject) doors [Random.Range (0, doors.Count)]);
		door.SetActive (true);
	}

	public static void addDoor (GameObject door) {
		doors.Add (door);
	}
}
