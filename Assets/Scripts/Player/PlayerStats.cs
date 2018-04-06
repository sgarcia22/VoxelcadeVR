using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	[SerializeField]
	private float health;
	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private Slider slider;
	private int level;

	void Start () {
		level = 0;
		slider.value = (health / maxHealth);
	}

	public void updateHealth (float amount) {
		health += amount;

		if (health > maxHealth) {
			health = maxHealth;
		}

		slider.value = (health / maxHealth);

		if (health <= 0) {
			slider.fillRect.gameObject.SetActive (false);
			//cause game reset
		}
	}

	public void addToMaxHealth (float amount) {
		maxHealth += amount;
	}

	public float getHealth () {
		return health;
	}

	public void gainLevel () {
		++level;
	}

	public int getLevel () {
		return level;
	}
}
