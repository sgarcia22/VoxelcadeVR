using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicScript : MonoBehaviour {

	private static MusicScript current = null;

	//Singleton that prevents new instances from being created
	public static MusicScript Instance {

		get {
			return current;
		}

	}

	void Start () {

		//If game object exists yet is not the current one then destroy
		if (current != null && current != this) {

			Destroy (this.gameObject);
			return;

		} else {

			current = this;
		}

		//Prevents the audio component from being removed when switching scenes
		DontDestroyOnLoad (this.gameObject);


	}
}
