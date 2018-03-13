using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	[SerializeField]
	private int health;
	private int level;

	void Start () {
		level = 0;
	}

	public void updateHealth (int amount) {
		health += amount;

		if (health <= 0) {
			//cause game reset
		}
	}

	public int getHealth () {
		return health;
	}

	public void gainLevel () {
		++level;
	}

	public int getLevel () {
		return level;
	}
}
