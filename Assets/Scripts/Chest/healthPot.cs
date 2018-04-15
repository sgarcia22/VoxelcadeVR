using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPot : MonoBehaviour {

	private float healAmount;
	public AudioClip soundEffect;
	public AudioSource soundSource;

	void Start () {
		healAmount = Random.Range (10, 90);
		soundSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter (Collider c) {

		if (c.tag.Equals ("Player")) {
			if(!soundSource.isPlaying)
			{
				//Play sound
				soundSource.PlayOneShot(soundEffect, 0.5F);
			}
			c.GetComponent <PlayerStats> ().updateHealth (healAmount);
			gameObject.SetActive (false);
			Destroy (gameObject);

		}
	}
}
