using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyRanged : MonoBehaviour {

	//Game Engine Variables
	public GameObject target;	 //Current target, the Player
	float lookAtHeight = 1.3f;   //Height use for the enemy look at
	public NavMeshAgent agent;   //Tells the Enemy where to go on
	public Renderer rend;		 //For accessing material color for now

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
	public int damage = 10;
	//Speed
	public float speed = 2.0f;
	public float speedIdle = 0.5f;
	//Health
	public int health = 100;
	


	void Awake()
	{
		enemyWaypoint = gameObject.transform.parent.GetChild(0).GetComponent<enemyWaypoint>();
		patrolPosition = enemyWaypoint.nextWaypoint();
	}
	// Use this for initialization
	void Start () {

		//Find the player object
		target = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
		state = enemyRanged.State.IDLE;
		rend = GetComponent<Renderer>();
		rend.material.color = Color.green; 

	}
	
	// Update is called once per frame
	void Update ()
	{
		//lookAtProcedure();

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
		//this.transform.GetChild(0).GetComponent<enemyRangeLook>().updateMaterial(0);
		//if(Vector3.Distance(this.transform.position,target.transform.position) < 1.5)
		//{
			//state = enemyRanged.State.CHASE;
		//}
		agent.speed = speedIdle;
		if(Vector3.Distance(this.transform.position,target.transform.position) < 1.5)
		{
			state =enemyRanged.State.CHASE;
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
		//Visual changes
		//this.transform.GetChild(0).GetComponent<enemyRangeLook>().updateMaterial(1);

		//Movement changes
		agent.speed = speed;
		agent.SetDestination(target.transform.position);

		//When the enemy is close enough, attack the player
		if(Vector3.Distance(this.transform.position,target.transform.position) < 1.2)
		{
			state = enemyRanged.State.ATTACK;
		}
	
	}
	//Attack
	void Attack()
	{
		//Visual changes
		//this.transform.GetChild(0).GetComponent<enemyRangeLook>().updateMaterial(2);
		
		//Movement changes
		//agent.SetDestination(target.transform.position);

		//Needs some cleaning
		if(Vector3.Distance(this.transform.position,target.transform.position) < 1.2)
		{
			agent.SetDestination(this.transform.position);
			this.transform.GetChild(0).GetComponent<projectile>().attack();
		}
		//If the player starts moving away, chase them
		if(Vector3.Distance(this.transform.position,target.transform.position) > 1.2)
		{
			state = enemyRanged.State.CHASE;
		}
	}


}
