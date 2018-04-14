using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayerReloadOnDeath : MonoBehaviour {

	[SerializeField]
	private PlayerStats player;

	void Update () {
		if (player.getHealth () <= 0) {
			RoomNodes.done = 0;
			BossDoorSelection.clearList ();
			SceneManager.LoadScene (0);
		}
	}

}
