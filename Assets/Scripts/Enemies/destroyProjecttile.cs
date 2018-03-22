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

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, 3f);
	}
	
}
