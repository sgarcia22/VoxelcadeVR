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
	public AudioClip soundEffect;
	public AudioSource soundSource;

	void Start()
	{
		soundSource = GetComponent<AudioSource>();
	}

	//Called in the enemyRanged attack script when in ATTACK state.
	public void attack()
	{
		if(fireRate > 0.9f)
		{
			//Movement changes
			if(!soundSource.isPlaying)
			{
				//Play sound
				soundSource.PlayOneShot(soundEffect, 0.5F);
			}
			Rigidbody shot = Instantiate(projectileBody, this.transform.position, this.transform.rotation) as Rigidbody;
			shot.AddForce(this.transform.forward * shotForce);
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