using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {


	public GameObject enemyMelee;
	public GameObject enemyRange;
	public GameObject enemyMimic;
	public int spawnAmountMelee = 3;
	public int spawnAmountRange = 0;
	public int spawnAmountMimic = 0; 
	public GameObject target;
	public bool spawnCall = false;
	public Vector3 position;
	int filterSpawn;


	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player"); 
		//Get the locations of the rooms
		 position =  new Vector3(transform.position[0],
							     transform.position[1],
							     transform.position[2]);

	}

	void Update()
	{
		if(spawnCall == false)
		{
				Spawn();
		}
	}


    void Spawn ()
    {
		spawnCall = true;
		//Vector3 position = new Vector3(0.0f, 1.3f, 0.0f);
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		for(int i = 0; i < spawnAmountMelee; i++)
		{
			filterSpawn = Random.Range(0,transform.childCount);

        	Instantiate (enemyMelee, transform.GetChild(filterSpawn).position , target.transform.rotation, transform.parent);
			//Instantiate (enemyMelee, transform.parent);
			position[0] -= 0.5f;
		}

		//Reset 
		position[0] =  transform.transform.position[0];
		position[1] =  transform.transform.position[1];	
		position[2] =  transform.transform.position[2];					    

		//Spawning mimic enemies
		for(int i = 0; i < spawnAmountMimic; i++)
		{
			filterSpawn = Random.Range(0,transform.childCount);

        	Instantiate (enemyMimic, transform.GetChild(filterSpawn).position , target.transform.rotation, transform.parent);
			position[2] -= 2.0f;
		}

				//Reset 
		position[0] =  transform.transform.position[0];
		position[1] =  transform.transform.position[1];	
		position[2] =  transform.transform.position[2];	

		for(int i = 0; i < spawnAmountRange; i++)
		{
			filterSpawn = Random.Range(0,transform.childCount);

        	Instantiate (enemyRange, transform.GetChild(filterSpawn).position , target.transform.rotation, transform.parent);
			position[0] -= 2.0f;
		}

    }
}
