using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeRelations : MonoBehaviour {

	// small number first, larger number second (there are no equal numbers)
	[SerializeField]
	Vector2Int [] nodes;
	[SerializeField]
	GameObject [] connections;
	Hashtable nodeToConnector;

	void Awake () {
		nodeToConnector = new Hashtable ();
		for (int i = 0; i < nodes.Length; ++i) {
			nodeToConnector.Add (nodes [i], connections [i]);
		}
	}

	public void enableConnector (int node1, int node2) {

		Vector2Int key;
		GameObject go;

		if (node1 > node2) {
			key = new Vector2Int (node2, node1);
		} else {
			key = new Vector2Int (node1, node2);
		}

		go = ((GameObject)nodeToConnector [key]);

		if (go != null) {
			go.SetActive (true);
		} else {
			Debug.Log ("No map value at : " + key.ToString ());
		}
	}
}
