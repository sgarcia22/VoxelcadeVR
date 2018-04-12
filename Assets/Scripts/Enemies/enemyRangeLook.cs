using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyRangeLook : MonoBehaviour {

	public GameObject target;	 //Current target, the Player
	float lookAtHeight = 1.3f;   //Height use for the enemy look at
	bool lookAt = false;
	//public Renderer rend;		 //For accessing material color for now
	

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player");
		//rend = GetComponent<Renderer>();
		//rend.material.color = Color.green; 	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lookAt)
		{
			lookAtProcedure();
		}
	}
	//METHODS

	//Change materials/animation
	//Called in enemyRanged script to changed color of material to display state.
	//TODO:
		//Use this for animation switching
	public void updateMaterial( int state)
	{
		if(state == 1)
		{
			lookAt = true; 
		}
		else 
		{
			lookAt = false;
		}

	}

	void lookAtProcedure()
	{
		//Basic Enemy Following
		//Enemy will always follow the player based on the Players position.
		Vector3 lookAtVector = new Vector3(target.transform.position.x, target.transform.position.y, 0.0f);
		//Check to see if the player is in a certain range to set the correct Z value
		if(Vector3.Distance(this.transform.position,target.transform.position) < 0.5 )
		{
			//Set the Z value to zero to avoid flipping.
			lookAtVector[2] = 0.0f;
		}	
		else
		{
			//Use player position
			lookAtVector[2] = target.transform.position.z;
		}
		
		this.transform.LookAt(lookAtVector);

	}
}