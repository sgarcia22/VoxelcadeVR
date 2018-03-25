using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroundMoles : MonoBehaviour {

	public GameObject [] moles;
	public int maxMoles = 3;
	public int currentMoles;
	//Keep a list of all of the surfaced moles
	public List<GameObject> molesSurfaced;
	public List<int> trackingMoles;
	private float currentTime;
	private bool gotMoleIndex;
	float random;
	private float totalTime;
	public float maxTime = 60f;
	public float molesHit = 0;
	public Text final;

	void Start () {
		currentMoles = 0;
		molesSurfaced = new List<GameObject> ();
		trackingMoles = new List<int> ();
		currentTime = 0f;
		gotMoleIndex = false;
	}

	void Update () {

		//End the boss fight
		if (totalTime >= maxTime) {
			final.text = "Score: " + molesHit + "\nPress Any Button to Restart.\n";
			if (OVRInput.Get (OVRInput.Button.One) || OVRInput.Get (OVRInput.Button.Two) || OVRInput.Get (OVRInput.Button.Three) || OVRInput.Get (OVRInput.Button.Four))
				SceneManager.LoadScene (0);
		}
		else {	
			if (currentMoles <= maxMoles && !gotMoleIndex) {
				random = Random.Range (0, 10);
				if (random <= currentTime) {
					int getMole = GetMole ();
					if (gotMoleIndex) {
						trackingMoles.Add (getMole);
						ActivateMole (getMole);
						gotMoleIndex = false;
					}
				}
			}
			//Keep track of the time
			currentTime += Time.deltaTime;
			totalTime += Time.deltaTime;
			if (trackingMoles.Count > 10) {
				//Pop the front value
				trackingMoles.RemoveAt (0);
			}
			//Debug.Log (molesSurfaced.Count);
		}
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
		int count = trackingMoles.Count;
		//If the same side has been chosen for too long, choose another side
		if (count >= 5) {
			//Get the average of the last five moles
			float average = (trackingMoles [count - 1] + trackingMoles [count - 2] + trackingMoles [count - 3] +
			                trackingMoles [count - 4] + trackingMoles [count - 5]) / 5;
			//Calculate the standard deviation
			float variance = (Mathf.Pow ((trackingMoles [count - 1] - average), 2) + Mathf.Pow ((trackingMoles [count - 2] - average), 2)
			                 + Mathf.Pow ((trackingMoles [count - 3] - average), 2) + Mathf.Pow ((trackingMoles [count - 4] - average), 2) +
			                 Mathf.Pow ((trackingMoles [count - 5] - average), 2)) / 5;
			float standardDeviation = Mathf.Sqrt (variance);
			//Debug.Log (standardDeviation);
			while (standardDeviation < 1.5 && count >= 5) {
				//Choose a random mole
				temp = Random.Range (0, moles.Length);
				//If the mole has already been chosen pick another
				while (moles [temp].GetComponent<Moles>().state == Moles.State.GOING_UP ||
					moles [temp].GetComponent<Moles>().state == Moles.State.UP) {
					temp = Random.Range (0, moles.Length);
				}
				//Get the average of the last five moles
				average = (trackingMoles [count - 1] + trackingMoles [count - 2] + trackingMoles [count - 3] +
					trackingMoles [count - 4] + temp) / 5;
				//Calculate the standard deviation
				variance = (Mathf.Pow ((trackingMoles [count - 1] - average), 2) + Mathf.Pow ((trackingMoles [count - 2] - average), 2)
					+ Mathf.Pow ((trackingMoles [count - 3] - average), 2) + Mathf.Pow ((trackingMoles [count - 4] - average), 2) +
					Mathf.Pow ((temp - average), 2)) / 5;
				standardDeviation = Mathf.Sqrt (variance);
				//Debug.Log (standardDeviation);
			}
		}
		//Apply Filtered Randomness
		//If there will be three in a row of the same mole, change the third one
		if (count >= 2) {
			if (trackingMoles [count - 1] == temp && trackingMoles [count - 2] == temp) {
				int same = temp;
				//Debug.Log ("Same");
				while (same == temp && (moles [temp].GetComponent<Moles>().state == Moles.State.GOING_UP ||
					moles [temp].GetComponent<Moles>().state == Moles.State.UP)) {
					temp = Random.Range (0, moles.Length);
				}
			}
		}
		gotMoleIndex = true;
		//Debug.Log (gotMoleIndex);
		return temp;
	}
}
