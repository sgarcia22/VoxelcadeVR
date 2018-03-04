using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {


	public GameObject enemy;
	public int spawnAmount = 5; 
	public GameObject target;
	public bool spawnCall = false;


	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player"); 
	}

	void Update()
	{
		if(Vector3.Distance(this.transform.position, target.transform.position) <= 10.0 && spawnCall == false)
		{
				Spawn();
		}
	}


    void Spawn ()
    {
		spawnCall = true;
		Vector3 position = new Vector3(0.0f, 1.3f, 0.0f);
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		for(int i = 0; i < spawnAmount; i++)
		{

        	Instantiate (enemy, position, target.transform.rotation);
			position[2] -= 2.0f;
		}
    }
}
