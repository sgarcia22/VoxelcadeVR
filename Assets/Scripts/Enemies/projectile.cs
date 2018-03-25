using UnityEngine;
using System.Collections;

//BUG
//When moving towards the enemy, sometime projectile move in opposite direction.

public class projectile : MonoBehaviour
{
    public Rigidbody projectileBody;	//Prefab projectile gameobject
    public Transform shotPos; 		//Position of the projectile instantiation.
    public float shotForce = 0.05f; //Force of the shot
    public float fireRate = 0.0f;  //The rate  of fire for ranged enemies


	//TODO 
	//Add in randomizer for missing

	//Called in the enemyRanged attack script when in ATTACK state.
	public void attack()
	{
		if(fireRate > 0.9f)
		{
			Rigidbody shot = Instantiate(projectileBody, shotPos.position, shotPos.rotation) as Rigidbody;
			shot.AddForce(shotPos.forward * shotForce);
			fireRate = 0.0f;
		}
		fireRate = fireRate + Time.deltaTime;
	}

	/*void OnCollisionEnter (Collision other) 
	{
		if(other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<PlayerStats>().updateHealth(-1.0f*damage);
		}
    }*/
}