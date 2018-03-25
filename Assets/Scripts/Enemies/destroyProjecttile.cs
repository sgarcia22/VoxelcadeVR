using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Destroys the Projectile object after 5 seconds.

public class destroyProjecttile : MonoBehaviour
{
	//TODO
	//Destory if object collided with player
	//Destory if object hits the floor/wall
	//Other enemies?
	public float damage = 5.0f;

	// Use this for initialization
	void Start () 
	{
		//Destroy(gameObject, 3f);
	}

	void OnCollisionEnter (Collision other) 
	{
		if(other.gameObject.CompareTag("Floor"))
		{
       	   Destroy(gameObject);
		}
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerStats>().updateHealth(-1.0f*damage);
			Destroy(gameObject);
		}
    }
	
}
