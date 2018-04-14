using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectables : MonoBehaviour {

	private int coins;
	private int gems;

	void Start () {
		coins = 5;
		gems = 0;
	}

	public void updateCoins (int numCoins) {
		coins += numCoins;
	}

	public void updateGems (int numGems) {
		gems = numGems;
	}

}
