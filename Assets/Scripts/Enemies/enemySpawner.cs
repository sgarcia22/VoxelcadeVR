using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

	//Prefabs
	public GameObject enemyMelee;
	public GameObject enemyRange;
	public GameObject enemyMimic;
	//Amount of each character spawned
	public int spawnAmountMelee = 0;
	public int spawnAmountRange = 0;
	public int spawnAmountMimic = 0; 
	//Max amount of each enemie at one time
	public int maxEnemies = 6;
	public int minEnemies= 1;
	public GameObject target;	//Player target
	public bool spawnCall = true; //Spawn has been called.
	public int spawnTotal = 0; //Total spawnned monsters for spawn area
	public int currTotal = 0; //Total spawnned monsters for spawn area
	public int filterSpawn; //Spawns enemies in certain locations
	public bool zoneCheck = true;


	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player"); 
	}

	void Update()
	{
		if(spawnCall && Vector3.Distance(this.transform.position, target.transform.position) < 5 )
		{
			Debug.Log("SpawnCalled");
			Spawn();
		}
		if(zoneCheck && Vector3.Distance(this.transform.position, target.transform.position) > 5)
		{
				checkPlayerAttack();
				zoneCheck = false;
		}
	}


    void Spawn ()
    {
		//Spawn in room once
		spawnCall = false;
       
	   //Set enemy amount depending on player stats
		spawnAmountMelee = Mathf.Min(maxEnemies, Mathf.Max(minEnemies,(int)(maxEnemies*target.GetComponent<playerModel>().returnTrait("meleeEnemyKills"))));
		spawnAmountRange = Mathf.Min(maxEnemies, Mathf.Max(minEnemies,(int)(maxEnemies*target.GetComponent<playerModel>().returnTrait("rangeEnemyKills"))));
		//spawnAmountMimic = Mathf.Min(maxEnemies, Mathf.Max(minEnemies,(int)(maxEnemies*target.GetComponent<playerModel>().returnTrait("mimicEnemyKills"))));
		spawnAmountMimic =  Random.Range(0,3);
		//DEBUG 
		//Debug.Log("Melee"+ target.GetComponent<playerModel>().returnTrait("meleeEnemyKills") );
		//Debug.Log("Range"+ target.GetComponent<playerModel>().returnTrait("rangeEnemyKills") );
		//Debug.Log("Mimic"+ target.GetComponent<playerModel>().returnTrait("mimicEnemyKills") );

		spawnTotal = spawnAmountMelee + spawnAmountRange + spawnAmountMimic;
	   	currTotal = spawnTotal;
	    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		for(int i = 0; i < spawnAmountMelee; i++)
		{
			filterSpawn = Random.Range(0,transform.childCount);
			GameObject temp = Instantiate (enemyMelee, transform.GetChild(filterSpawn).position , target.transform.rotation, transform.parent);
			temp.tag = "Enemy";
			//temp.GetComponent<enemyMelee>().setPosition();
			//Instantiate (enemyMelee, transform.parent);
		}				    

		//Spawning mimic enemies
		for(int i = 0; i < spawnAmountMimic; i++)
		{
			filterSpawn = Random.Range(0,transform.childCount);
			GameObject temp = Instantiate (enemyMimic, transform.GetChild(filterSpawn).position , target.transform.rotation, transform.parent);
			temp.tag = "Enemy";
		}


		for(int i = 0; i < spawnAmountRange; i++)
		{
			filterSpawn = Random.Range(0,transform.childCount);

			GameObject temp = Instantiate (enemyRange, transform.GetChild(filterSpawn).position , target.transform.rotation, transform.parent);
			temp.tag = "Enemy";
		}

    }

	//Player check 
	void checkPlayerAttack()
	{
		if(currTotal == 0)
		{
			//Good 
			target.GetComponent<playerModel>().updateTrait("rangeEnemyKills", -0.5f);
			target.GetComponent<playerModel>().updateTrait("meleeEnemyKills", -0.5f);
		}
		else 
		{
			//Weight them more so more enemies spawn
			target.GetComponent<playerModel>().updateTrait("rangeEnemyKills", 1.0f);
			target.GetComponent<playerModel>().updateTrait("meleeEnemyKills", 1.0f);
		}
	}


}
