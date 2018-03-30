using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollowPlayer : MonoBehaviour {

	[SerializeField]
	private Transform player;
	[SerializeField]
	private Text text;
	[SerializeField]
	private GameObject bossDoorIcon;
	private float distance;
	private float oldYRot;
	private Vector3 oldPos;

	void Start () {
		oldYRot = player.eulerAngles.y;
		oldPos = player.transform.position;
		distance = (player.position - transform.position).magnitude;

		if (oldPos.y < 1) {
			text.text = "1";
		} else if (oldPos.y < 2) {
			text.text = "2";
		} else {
			text.text = "3";
		}
	}

	void Update () {

		float currentYRot = player.eulerAngles.y;
		float angle = currentYRot - oldYRot;
		transform.RotateAround (player.position, Vector3.up, angle);
		oldYRot = currentYRot;

		Vector3 currentPos = player.transform.position;
		if ((currentPos - oldPos).magnitude > 0.1f) {
			
			Vector3 newPos = oldPos - currentPos;
			transform.position -= newPos;
			oldPos = currentPos;

		}

	}

	void FixedUpdate () {
		if (oldPos.y < 1) {
			text.text = "1";
		} else if (oldPos.y < 2) {
			text.text = "2";
		} else {
			text.text = "3";
		}
	}

	private void activateBossIcon (Vector3 pos) {
		bossDoorIcon.transform.position = pos;
		bossDoorIcon.SetActive (true);
	}

	void OnEnable () {
		FoundBossDoor.found += activateBossIcon;
	}

	void OnDisable () {
		FoundBossDoor.found -= activateBossIcon;
	}
}
