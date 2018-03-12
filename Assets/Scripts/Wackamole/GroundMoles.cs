using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMoles : MonoBehaviour {

	public GameObject [] moles;
	public int maxMoles = 3;
	public int currentMoles;
	//Keep a list of all of the surfaced moles
	public List<GameObject> molesSurfaced;
	private float currentTime;

	void Start () {
		currentMoles = 0;
		molesSurfaced = new List<GameObject> ();
		currentTime = 0f;
	}

	void Update () {
		float random = Random.Range (0, 10);
		if (currentMoles <= maxMoles && random <= currentTime) {
			int getMole = GetMole ();
			ActivateMole (getMole);
		}
		currentTime += Time.deltaTime;
	}

	//Activate a mole to send it upwards
	private void ActivateMole(int index) {
		moles [index].GetComponent<Moles> ().state = Moles.State.GOING_UP; 
		molesSurfaced.Add (moles [index]);
		++currentMoles;
		currentTime = 0;
	}

	private int GetMole () {
		//Choose a random mole
		int temp = Random.Range (0, moles.Length);
		//If the mole has already been chosen pick another
		while (moles [temp].GetComponent<Moles>().state == Moles.State.GOING_UP ||
			moles [temp].GetComponent<Moles>().state == Moles.State.UP) {
			temp = Random.Range (0, moles.Length);
		}
		return temp;
	}
}
