using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWaypoint : MonoBehaviour {

	public Transform[] waypoints

	// Use this for initialization
	void Start () 
	{
		//Set the movable points based on spawn
		//Get the parent 
		waypoints = gameObject.transform.parent.GetChild(0). 	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
