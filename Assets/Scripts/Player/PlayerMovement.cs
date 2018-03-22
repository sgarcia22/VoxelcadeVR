using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject player;
	private Rigidbody rigidbody;
	private GameObject rightHand;
	private GameObject leftHand;
	private GameObject camera;

	public float speed = 2f;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		rigidbody = player.GetComponent<Rigidbody> ();
		rightHand = GameObject.FindGameObjectWithTag ("RightHand");
		leftHand = GameObject.FindGameObjectWithTag ("LeftHand");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		//Movement
		if (OVRInput.Get (OVRInput.Button.Two)) {
			Vector3 temp = camera.transform.forward;
			temp.y = 0f;
			player.transform.position += temp * Time.deltaTime * speed;
		}
	}

}
