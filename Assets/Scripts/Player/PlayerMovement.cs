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
			Debug.Log ("Entering");
			Debug.Log (camera.transform.forward);
			Vector3 temp = player.transform.position;
			temp.x = camera.transform.position.x * speed * Time.deltaTime;
			temp.y = camera.transform.position.y * speed * Time.deltaTime;
			player.transform.position = temp;
		}
	}

}
