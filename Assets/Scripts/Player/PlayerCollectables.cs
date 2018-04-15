using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectables : MonoBehaviour {

	private int coins;
	private int gems;
	public AudioClip soundEffect;
	public AudioSource soundSource;

	void Start () {
		coins = 5;
		gems = 0;
		soundSource = GetComponent<AudioSource>();
	}

	public void updateCoins (int numCoins) {
		coins += numCoins;
		if(!soundSource.isPlaying)
		{
			//Play sound
			soundSource.PlayOneShot(soundEffect, 0.5F);
		}
	}

	public void updateGems (int numGems) {
		gems = numGems;
		if(!soundSource.isPlaying)
		{
			//Play sound
			soundSource.PlayOneShot(soundEffect, 0.5F);
		}
	}

}
