using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePlayerChestItems : MonoBehaviour {

	bool hasHealthItem;
	bool hasMaxHealthItem;
	bool hasGemItem;
	bool hasCoinItem;


	void Start () {
		int rand = Random.Range (0, 2);

		if (rand == 0) {
			hasHealthItem = true;
		}

		rand = Random.Range (0, 2);

		if (rand == 0) {
			hasMaxHealthItem = true;
		}

		rand = Random.Range (0, 2);

		if (rand == 0) {
			hasGemItem = true;
		}

		rand = Random.Range (0, 2);

		if (rand == 0) {
			hasCoinItem = true;
		}

		
	}

	void OnTriggerEnter (Collider c) {
		
		if (c.tag.Equals ("Player")) {
			PlayerStats ps = c.GetComponent <PlayerStats> ();
			PlayerCollectables pc = c.GetComponent <PlayerCollectables> ();

			if (hasHealthItem) {
				ps.updateHealth (10);
			}

			if (hasMaxHealthItem) {
				ps.addToMaxHealth (5);
				ps.updateHealth (10);
			}

			if (hasGemItem) {
				pc.updateGems (5);
			}

			if (hasCoinItem) {
				pc.updateCoins (5);
			}

			gameObject.SetActive (false);
			Destroy (gameObject);
		}

	}
}
