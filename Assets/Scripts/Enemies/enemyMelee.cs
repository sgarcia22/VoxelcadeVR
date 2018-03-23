using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMelee : MonoBehaviour {

	//Game Engine Variables
	public GameObject target;	 //Current target, the Player
	float lookAtHeight = 1.3f;   //Height use for the enemy look at
	public NavMeshAgent agent;   //Tells the Enemy where to go on
	public Renderer rend;		 //For accessing material color for now

	//Enemy State Machine
	public enum State
	{
		ATTACK,
		CHASE,
		IDLE
	}
	public State state;  //Current State of the Enemy

	//Enemy Variables
	//Damage
	public int damage = 10;
	//Speed
	public float speed = 3.0f;
	//Health
	public int health = 100;
	

	// Use this for initialization
	void Start () {

		//Find the player object
		target = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
		state = enemyMelee.State.IDLE;
		rend = GetComponent<Renderer>();
		rend.material.color = Color.green; 

	}
	
	// Update is called once per frame
	void Update ()
	{
		lookAtProcedure();

		switch(state)
		{
			case State.IDLE:
				Idle ();
				break;
			case State.CHASE:
				Chase ();
				break;
			case State.ATTACK:
				Attack();
				break;

		}

	}

	//STATE MACHINE STATE DEFINITIONS
	void Idle()
	{
	    //If the player is within a certain distance move towards them
		//Eventually this will just be on room enter.
		if(Vector3.Distance(this.transform.position,target.transform.position) < 2.0)
		{
			state = enemyMelee.State.CHASE;
		}
	}
	//Chase
	void Chase()
	{
		rend.material.color = Color.blue;  //Used only for visualization of state changes
		agent.speed = speed;
		agent.SetDestination(target.transform.position);

		//When the enemy is close enough, attack the player
		if(Vector3.Distance(this.transform.position,target.transform.position) <1.0)
		{
			state = enemyMelee.State.ATTACK;
		}
	
	}
	//Attack
	void Attack()
	{
		rend.material.color = Color.red; //Used only for visualization of state changes
		agent.SetDestination(target.transform.position);
		
		//Needs some cleaning
		if(Vector3.Distance(this.transform.position,target.transform.position) < 1.0)
		{
			agent.SetDestination(this.transform.position);
		}
		//If the player starts moving away, chase them
		if(Vector3.Distance(this.transform.position,target.transform.position) > 1.0)
		{
			state = enemyMelee.State.CHASE;
		}
	}

	//METHODS
	void lookAtProcedure()
	{
		//Basic Enemy Following
		//Enemy will always follow the player based on the Players position.
		Vector3 lookAtVector = new Vector3(target.transform.position.x, lookAtHeight, 0.0f);
		//Check to see if the player is in a certain range to set the correct Z value
		if(Vector3.Distance(this.transform.position,target.transform.position) < 1.0 )
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
