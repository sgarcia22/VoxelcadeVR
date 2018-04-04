using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorSelection : MonoBehaviour {

	private static Dictionary <int, GameObject> doors = new Dictionary <int, GameObject> ();

	public int pickDoor () {
		int [] keys = new int[doors.Count];
		doors.Keys.CopyTo (keys, 0);
		int choice = Random.Range (0, keys.Length);
		doors [keys [choice]].SetActive (true);
		return choice;
	}

	public static void addDoor (int ID, GameObject door) {
		if (!doors.ContainsKey (ID)) {
			doors.Add (ID, door);
		}
	}
}
