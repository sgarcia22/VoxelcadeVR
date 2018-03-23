using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {


	public GameObject enemyMelee;
	public GameObject enemyRange;
	public GameObject enemyMimic;
	public int spawnAmountMelee = 3;
	public int spawnAmountRange = 2;
	public int spawnAmountMimic = 1; 
	public GameObject target;
	public bool spawnCall = false;


	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player"); 
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
		Vector3 position = new Vector3(0.0f, 1.3f, 0.0f);
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		for(int i = 0; i < spawnAmountMelee; i++)
		{

        	Instantiate (enemyMelee, position, target.transform.rotation);
			position[0] -= 0.5f;
		}

		position[0] = 0.0f;
		position[1] = 1.3f;
		position[2] = -3.0f;
		//Spawning mimic enemies
		for(int i = 0; i < spawnAmountMimic; i++)
		{
			Instantiate (enemyMimic, position, target.transform.rotation);
			position[2] -= 2.0f;
		}

		//Spawning range enemies
		position[0] = 0.5f;
		position[1] = 1.3f;
		position[2] = 0.0f;
		for(int i = 0; i < spawnAmountRange; i++)
		{
			Instantiate (enemyRange, position, target.transform.rotation);
			position[0] -= 2.0f;
		}

    }
}
