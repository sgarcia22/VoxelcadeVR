using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptics : MonoBehaviour {

	private byte[] haptics;
	public byte vibrationIntensity;
	public float vibrationDuration;

	void Start () {
		haptics = new byte[(int)(320 * vibrationDuration)];
		for (int i = 0; i < haptics.Length; ++i)
			haptics [i] = vibrationIntensity;
	}

	public void vibrate (bool left) {
		//Debug.Log ("Destroy");
		if (left)
			OVRHaptics.LeftChannel.Mix (new OVRHapticsClip (haptics, haptics.Length));
		else
			OVRHaptics.RightChannel.Mix (new OVRHapticsClip (haptics, haptics.Length));
	}
}
