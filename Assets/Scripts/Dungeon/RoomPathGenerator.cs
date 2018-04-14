using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPathGenerator : MonoBehaviour {

	public static readonly int LEVELS = 9;

	[SerializeField]
	private NeighborMap map;
	[SerializeField]
	private BossDoorSelection BDS;
	[SerializeField]
	private Transform hammer;
	private int previousNodeID;
	private ArrayList notInPath;
	private ArrayList inPath;
	private RoomNodes current;
	private bool lev0_HasStairsUP;
	private bool lev1_HasStairsUP;
	private Vector3 playerStart;

	void Start () {
		lev0_HasStairsUP = false;
		lev1_HasStairsUP = false;
		playerStart = new Vector3 (-14.38f, 0f, -14.38f);

		inPath = new ArrayList ();
		notInPath = new ArrayList ();

		for (int i = 0; i < 27; ++i) {
			notInPath.Add (map.getNeighbors(i));
		}

		StartCoroutine (waitForNodes ());
	}

	private IEnumerator waitForNodes () {
		while (RoomNodes.done < 27) {
			yield return new WaitForSeconds (1);
		}
		createDungeonPath ();
			yield return null;
		int doorNode = BDS.pickDoor ();
		pickHammerLocal (doorNode);
	}

	private void createDungeonPath () {
		// begin algorithm get first node of graph
		current = (RoomNodes) notInPath [Random.Range (0, notInPath.Count)];
		notInPath.Remove (current);
		inPath.Add (current);

		// keep connecting nodes -- ends when reaches node with no unconnected neighbors that are not already in the path
		RoomNodes previous = current;
		previousNodeID = previous.getID ();
		while ((current = current.getRandomUnconnectedNeighbor (previousNodeID)) != null) {
			previousNodeID = previous.getID ();

			if (previous.connectTwoNeighbors (current)) {
				current.connectTwoNeighbors (previous);

				notInPath.Remove (current);
				inPath.Add (current);

				previous = current;
			} else {
				break;
			}
		}


		// some nodes may still be unconnected, the next stage is to connect them to a neighbor whom is attached until all remaining nodes are connected.
		connectUnconnectedNodes (2);
	}

	private void pickHammerLocal (int bossRoom) {
		int roomNum = Random.Range (0, inPath.Count);

		while (((RoomNodes) inPath [roomNum]).getID () == bossRoom && !withPlayer (((RoomNodes) inPath [roomNum]).transform.position)) {
			roomNum = Random.Range (0, inPath.Count);
		}

		hammer.position = ((RoomNodes)inPath [roomNum]).transform.position + new Vector3 (0.3f, 0.2f, 1f);
	}

	private bool withPlayer (Vector3 hammerLoc) {
		return (Mathf.Abs (hammerLoc.x - playerStart.x) < 0.5f && Mathf.Abs (hammerLoc.z - playerStart.z) < 0.5f);
	}

	private void connectUnconnectedNodes (int iterations) {
		RoomNodes inPathNeighbor;
		ArrayList toBeRemoved = new ArrayList ();
		for (int i = 0; i < iterations; ++i) {
			toBeRemoved.Clear ();

			foreach (RoomNodes RN in notInPath) {

				RoomNodes choice;
				if ((choice = RN.getNeighborInPath ()) != null) {
					if (choice.connectTwoNeighbors (RN)) {
						RN.connectTwoNeighbors (choice);

						toBeRemoved.Add (RN);
						inPath.Add (RN);

					} else {
						break;
					}
				}
			}

			// cannot remove during secondary adding as it causes threading problems
			foreach (RoomNodes RN in toBeRemoved) {
				notInPath.Remove (RN);
			}
		}
	}
}
