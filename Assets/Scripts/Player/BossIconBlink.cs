using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIconBlink : MonoBehaviour {

	[SerializeField]
	private float blinkDelay;

	private bool isOn;
	private float timer;
	private Color color;

	void Start () {
		isOn = true;
		timer = 0f;
		color = GetComponent <Renderer> ().material.color;
	}

	void Update () {

		timer += Time.deltaTime;

		if (timer > blinkDelay) {
			blink ();
			timer = 0f;
		}
	}

	private void blink () {
		Material mat = GetComponent <Renderer> ().material;

		if (isOn) {
			mat.color = Color.black;
		} else {
			mat.color = color;
		}

		isOn = !isOn;
	}
}
