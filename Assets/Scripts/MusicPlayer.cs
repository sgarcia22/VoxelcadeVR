using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {


	public AudioClip[] soundClips = new AudioClip[4];
	public AudioSource soundSource;
	public int track;
	public float pauseTimer;
	public bool musicPlay;
 
	// Use this for initialization
	void Start () {
		soundSource = GetComponent<AudioSource>();
		track = 0;
		pauseTimer = 10.0f;
		musicPlay = true;
	}
	
	// Play default sound
	void Update ()
	{
		if(!soundSource.isPlaying && musicPlay)
		{
			//Play sound
			soundSource.PlayOneShot(soundClips[track], 0.3F);
			track++;
			musicPlay = false;
			if(track > soundClips.Length)
			{
				track = 0;
			}
		}
		else if(!soundSource.isPlaying)
		{
			pauseTimer -= Time.deltaTime;
			if(pauseTimer < 5.0f)
			{
				musicPlay = true;
				pauseTimer = 10.0f;
			}
		}
		
	}
	
	void PlayTheNextMusic() {
		// I'm not sure if it is Length or length here...
	}
}
