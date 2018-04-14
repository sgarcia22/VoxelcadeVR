using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWaypoint : MonoBehaviour {

	//public Transform[] waypoints
	public List<Transform> waypoints;

	// Use this for initialization
	void Start () 
	{
		//Set the movable points based on spawn
		//Get the parent 
		//waypoints = gameObject.transform.parent.GetChild(0).
		waypoints = new List<Transform>();
		int childCount = gameObject.transform.childCount;
		Debug.Log("WAYPOINT"+ childCount);

		for (int i = 0; i < childCount; i++)
		{
			//WayPointClass temp = tempPoints[i].GetComponent<WayPointClass>();
			waypoints.Add(gameObject.transform.GetChild(i).transform); 
			Debug.Log("WAYPOINT"+ waypoints[i]);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	//Send a random waypoint on request
	public Transform nextWaypoint()
	{
		int newWaypoint = Random.Range(0,waypoints.Count);
		return waypoints[newWaypoint];
	}
}
