using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public GameObject player;
	private GameObject rightHand;
	private GameObject leftHand;
	private GameObject camera;
	private GameObject map;
	public float speed = 2f;
	private float timeTillPress = .5f;
	private float currentTime = 0f;
	private bool pressMap = false;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		rightHand = GameObject.FindGameObjectWithTag ("RightHand");
		leftHand = GameObject.FindGameObjectWithTag ("LeftHand");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		map = GameObject.FindGameObjectWithTag ("Map");
		map.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTime >= timeTillPress)
			pressMap = false;
		//Movement
		if (OVRInput.Get (OVRInput.Button.Two)) {
			Vector3 temp = camera.transform.forward;
			temp.y = 0f;
			player.transform.position += temp * Time.deltaTime * speed;
		}
		if (OVRInput.Get (OVRInput.Button.Four) && !pressMap) {
			if (map.activeSelf == false)
				map.SetActive (true);
			else
				map.SetActive (false);
			pressMap = true;
			currentTime = 0f;
		}
		currentTime += Time.deltaTime;
	}

}
