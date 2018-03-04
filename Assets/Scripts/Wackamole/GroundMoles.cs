using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoles : MonoBehaviour {

	public GameObject [] moles;
	public int maxMoles = 3;
	private int currentMoles;
	private List<int> molesSurfaced;

	void Start () {
		currentMoles = 0;
		molesSurfaced = new List<int> ();
	}

	void Update () {
		if (currentMoles <= maxMoles) {
			int getMole = Random.Range (0, moles.Length);
			//TODO
		}
	}
}
