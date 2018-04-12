using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMelee : MonoBehaviour {

	//Game Engine Variables
	public GameObject target;	 //Current target, the Player
	float lookAtHeight;   //Height use for the enemy look at
	public NavMeshAgent agent;   //Tells the Enemy where to go on

	public Transform patrolPosition; //Adding in enemy movement
	public enemyWaypoint enemyWaypoint;

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
	public float damage = 10.0f;
	public float shotForce = 0.05f; //Force of the shot
	public float fireRate = 0.0f;  //The rate  of fire for ranged enemies
	//Speed
	public float speed = 0.8f;
	public float speedIdle = 0.5f;
	//Health
	public int health = 100;
	

	//void
	void Awake()
	{
		enemyWaypoint = gameObject.transform.parent.GetChild(0).GetComponent<enemyWaypoint>();
		patrolPosition = enemyWaypoint.nextWaypoint();
	}

	// Use this for initialization
	void Start () {

		//Find the player object
		target = GameObject.FindGameObjectWithTag("Player");
		lookAtHeight = target.transform.position[1];
		agent = GetComponent<NavMeshAgent>();
		state = enemyMelee.State.IDLE;
		//enemyWaypoint = gameObject.transform.parent.GetChild(0).GetComponent<enemyWaypoint>();
		//patrolPosition = enemyWaypoint.nextWaypoint();
		//rend = GetComponent<Renderer>();
		//rend.material.color = Color.green; 

	}
	
	// Update is called once per frame
	void Update ()
	{
		
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
		/*if(Vector3.Distance(this.transform.position,target.transform.position) < 1.0)
		{
			state = enemyMelee.State.CHASE;
		}
		else
		{
			agent.SetDestination(this.transform.position);
		}*/
		agent.speed = speedIdle;

		if(Vector3.Distance(this.transform.position,target.transform.position) < 1.0)
		{
			state = enemyMelee.State.CHASE;
		}
		else if(Vector3.Distance(this.transform.position, this.patrolPosition.position )>= 0.5)
		{
			agent.SetDestination(patrolPosition.position);
		}
		else if (Vector3.Distance(this.transform.position, this.patrolPosition.position ) < 0.5)
		{
			patrolPosition = enemyWaypoint.nextWaypoint();
		}


	}
	//Chase
	void Chase()
	{
		lookAtProcedure();
		//rend.material.color = Color.blue;  //Used only for visualization of state changes
		agent.speed = speed;
		agent.SetDestination(target.transform.position);

		//When the enemy is close enough, attack the player
		if(Vector3.Distance(this.transform.position,target.transform.position) <0.5)
		{
			state = enemyMelee.State.ATTACK;
		}
	
	}
	//Attack
	void Attack()
	{
		lookAtProcedure();
		//rend.material.color = Color.red; //Used only for visualization of state changes
		agent.SetDestination(target.transform.position);
		
		//Needs some cleaning
		if(Vector3.Distance(this.transform.position,target.transform.position) < 0.5)
		{
			agent.SetDestination(this.transform.position);
			//Remove health
			if(fireRate > 1.0f)
			{
				target.GetComponent<PlayerStats>().updateHealth(-1.0f*damage);
				fireRate = 0.0f;
			}
			fireRate = fireRate + Time.deltaTime;

		}
		//If the player starts moving away, chase them
		if(Vector3.Distance(this.transform.position,target.transform.position) > 0.5)
		{
			state = enemyMelee.State.CHASE;
		}
	}

	//METHODS
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

	public void setPosition()
	{
		patrolPosition = enemyWaypoint.nextWaypoint();
	}




}
