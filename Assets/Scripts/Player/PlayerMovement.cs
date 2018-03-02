using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private GameObject player;
	private Rigidbody rigidbody;
	private GameObject rightHand;
	private GameObject leftHand;
	public GameObject camera;

	public float speed = 2f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rigidbody = player.GetComponent<Rigidbody> ();
		rightHand = GameObject.FindGameObjectWithTag ("RightHand");
		leftHand = GameObject.FindGameObjectWithTag ("LeftHand");
	}
	
	// Update is called once per frame
	void Update () {
		//Movement
		if (OVRInput.Get (OVRInput.Button.SecondaryThumbstick)) {
			//TODO: .forward()
			Debug.Log ("Entering");
			Vector3 position = player.transform.position;
			float yRotation = camera.transform.rotation.y;
			yRotation += speed * Time.deltaTime;
			position.y = yRotation;
			player.transform.position = position;
		}
	}

}
