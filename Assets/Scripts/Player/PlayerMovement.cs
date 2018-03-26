using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public GameObject player;
	private Rigidbody rb;
	private GameObject rightHand;
	private GameObject leftHand;
	private GameObject camera;
	private GameObject map;
	public float speed = 2f;
	private float timeTillPress = .5f;
	private float currentTime = 0f;
	private bool pressMap = false;
	private float gravity = 9.8f;
	private Vector3 tempRigid;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rb = player.GetComponent<Rigidbody> ();
		rightHand = GameObject.FindGameObjectWithTag ("RightHand");
		leftHand = GameObject.FindGameObjectWithTag ("LeftHand");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		map = GameObject.FindGameObjectWithTag ("Map");
		map.SetActive (false);
		tempRigid = rb.velocity;
		//Physics.gravity = new Vector3 (0f, -1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		//tempRigid.y -= gravity * Time.deltaTime;
		//player.transform.position += tempRigid * Time.deltaTime;

		if (currentTime >= timeTillPress)
			pressMap = false;
		//Movement
		if (OVRInput.Get (OVRInput.Button.Two)) {
			/*Vector3 temp = camera.transform.forward;
			temp.y = 0f;
			player.transform.position += temp * Time.deltaTime * speed;
			*/
			rb.MovePosition(player.transform.position + camera.transform.forward * Time.deltaTime * speed);
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


	void OnCollisionEnter(Collision col) {
		Debug.Log (col.gameObject.name);
	}
}
