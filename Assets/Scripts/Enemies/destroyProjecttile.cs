using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProjecttile : MonoBehaviour
{
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
