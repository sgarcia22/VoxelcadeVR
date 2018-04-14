using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FOR TESTING ENEMY SPAWN
public class playAttack : MonoBehaviour 
{
	

	void OnMouseDown ()
    {
		Debug.Log(gameObject.transform.parent.GetChild(0).GetComponent<enemySpawner>().spawnTotal);
		Debug.Log("CLICK");
		
		GameObject player =  GameObject.FindWithTag("Player");
		player.GetComponent<playerModel>().addKill();

		//Update Player Model
		if(gameObject.name == "EnemyMelee(Clone)")
		{
			player.GetComponent<playerModel>().updateTrait("meleeEnemyKills", -1.0f);
		}
		else if(gameObject.name =="Range(Clone)")
		{
			player.GetComponent<playerModel>().updateTrait("rangeEnemyKills", -1.0f);
		}
		else
		{
			player.GetComponent<playerModel>().updateTrait("mimicEnemyKills", -1.0f);
		}

		//Update killed in spawn
		gameObject.transform.parent.GetChild(0).GetComponent<enemySpawner>().currTotal--;

		DestroyObject(gameObject);
		
    }
}
