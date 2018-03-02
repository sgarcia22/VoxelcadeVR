using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborMap : MonoBehaviour {

	[SerializeField]
	private RoomNodes[] nodes;
	private Hashtable nodeTable;

	void Awake () {
		nodeTable = new Hashtable ();
		for (int i = 0; i < nodes.Length; ++i) {
			nodeTable.Add (nodes [i].getID (), nodes [i]);
		}
	}

	public RoomNodes getNeighbors (int ID) {
		if (nodeTable [ID] == null) {
			Debug.Log ("Map is missing node " + ID);
		}
		return (RoomNodes) nodeTable [ID];
	}
}
