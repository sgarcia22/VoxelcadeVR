﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNodes : MonoBehaviour {

	public static int done = 0;
	public static int roomsInPath = 0;
	public static int chestSpawned = 0;

	[SerializeField]
	private NeighborMap map;
	[SerializeField]
	private NodeRelations relation;
	[SerializeField]
	private int ID;
	[SerializeField]
	private int[] neighbors;
	[SerializeField]
	private GameObject possibleBossDoor;
	[SerializeField]
	private GameObject chestPrefab;
	[SerializeField]
	private GameObject healthPotPrefab;

	private ArrayList unconnectedNeighbors;
	private ArrayList connectedNeighbors;
	private bool inPath;
	private Vector3 playerStart;

	void Start () {
		connectedNeighbors = new ArrayList ();
		unconnectedNeighbors = new ArrayList ();
		playerStart = new Vector3 (-14.38f, 1.5f, -14.38f);

		for (int i = 0; i < neighbors.Length; ++i) {
			unconnectedNeighbors.Add (map.getNeighbors(neighbors[i]));
		}

		inPath = false;
		done++;
	}

	public int getID () {
		return ID;
	}


	public bool isInPath () {
		return inPath;
	}

	public void setInPath () {

		++roomsInPath;

		if (possibleBossDoor != null) {
			BossDoorSelection.addDoor (ID, possibleBossDoor);
		}

		if (Mathf.Abs (transform.position.x - playerStart.x) > 1 && Mathf.Abs (transform.position.z - playerStart.z) > 1) {
			if (Random.Range (0, 10) == Random.Range (0, 10) || ((float)chestSpawned / (float)roomsInPath) < 0.1) {
				++chestSpawned;
				Instantiate (chestPrefab, transform.position + new Vector3 (0f, 0.05f, 0f), chestPrefab.transform.rotation);
			} else if (Random.Range (0, 10) == Random.Range (0, 10)) {
				Instantiate (healthPotPrefab, transform.position + new Vector3 (2.5f, 0.25f, 2.5f), healthPotPrefab.transform.rotation);
			}
		}

		inPath = true;
	}

	// newConnect must be in the unconnected list
	// also connects given neighbor to this
	public bool connectTwoNeighbors (RoomNodes newConnect) {

		int count = unconnectedNeighbors.Count;
		unconnectedNeighbors.Remove (newConnect);

		if (count > unconnectedNeighbors.Count) {
			connectedNeighbors.Add (newConnect);
			enableCorridor (newConnect.getID ()); // activates gameobject
			inPath = true;
			newConnect.setInPath ();
			return true;
		} 

		return false;
	}

	public bool isNodeConnected (RoomNodes nodeToTest) {
		foreach (RoomNodes RN in connectedNeighbors) {
			if (RN == nodeToTest) {
				return true;
			}
		}
		return false;
	}

	public RoomNodes getNeighborInPath () {

		foreach (RoomNodes RN in unconnectedNeighbors) {
			if (RN.isInPath () && !RN.diagonalRoomConnected (this) && RN.notConnectedToVerticleNodes (this)) {
				return RN;
			}
		}

		return null;
	}

	// returns null
	public RoomNodes getRandomUnconnectedNeighbor (int fromID) {
		if (unconnectedNeighbors.Count > 0) {
			RoomNodes r;
			int i = Random.Range (0, unconnectedNeighbors.Count);
			int j = i;
			while ((r = (RoomNodes)unconnectedNeighbors [i]).isInPath () || ((fromID % RoomPathGenerator.LEVELS) == (r.getID () % RoomPathGenerator.LEVELS)) || diagonalRoomConnected (r)) {
				i = (i + 1) % unconnectedNeighbors.Count;

				if (j == i) {
					return null;
				}
			}

			return r;
		}

		return null;
	}

	private bool diagonalRoomConnected (RoomNodes nextNode) {

		RoomNodes badConnector;
		// current node above next node, could lead to possible conflict from node below this one.
		if (ID / RoomPathGenerator.LEVELS > nextNode.getID () / RoomPathGenerator.LEVELS) {
			if ((badConnector = map.getNeighbors (nextNode.getID () + RoomPathGenerator.LEVELS)) != null) {
				if (badConnector.isNodeConnected (map.getNeighbors (ID - RoomPathGenerator.LEVELS))) {
					return true;
				}
			}
		} // current node below next node, could lead to possible conflict from node above this one.
		else if (ID / RoomPathGenerator.LEVELS < nextNode.getID () / RoomPathGenerator.LEVELS) {
			if ((badConnector = map.getNeighbors (nextNode.getID () - RoomPathGenerator.LEVELS)) != null) {
				if (badConnector.isNodeConnected (map.getNeighbors (ID + RoomPathGenerator.LEVELS))) {
					return true;
				}
			}
		} 

		// straight corridor, no problems. (due to corridor generation, if that changes this will too)
		// also if diagonals dont interfer with each other.
		return false;

	}

	// checks if this node is connected to nextNode's verticle neighbors
	public bool notConnectedToVerticleNodes (RoomNodes nextNode) {
		int verticleID = nextNode.getID () % RoomPathGenerator.LEVELS; // gives 0 - 8.

		for (int i = 0; i < 3; ++i) {
			if (this.isNodeConnected (map.getNeighbors (verticleID + (RoomPathGenerator.LEVELS * i)))) {
				return false;
			}
		}

		return true;
	}
		
	// to use in hash table, one must override hash code method as well
	public override bool Equals (object other)
	{
		if (other is RoomNodes) {
			RoomNodes r = (RoomNodes) other;
			return this.ID == r.getID ();
		}

		return false;
	}

	private void enableCorridor (int toLevel) {
		relation.enableConnector (this.ID, toLevel);
	}
}
