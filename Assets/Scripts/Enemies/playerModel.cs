using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerModel : MonoBehaviour {

	Dictionary<string, float> traitValue;
	private float learningRate;
	public int kills;

	// Use this for initialization
	void Start () {

		//Initialize dictionary
		traitValue = new Dictionary<string, float>();

		//Set the initial values
		traitValue.Add("meleeEnemyKills", 0.5f);  
		traitValue.Add("rangeEnemyKills", 0.5f); 
		traitValue.Add("mimicEnemyKills", 0.5f);   

		learningRate = 0.5f; //learning rate
		
	}
	
	public void updateTrait (string trait, float observed)
	{
		//LMS - Least mean squares 
		float currvalue = traitValue[trait]; 
		float delta = observed - currvalue;  		//Change in value 
		float weightedDelta = learningRate * delta; //Getting the weighted change of the evidence
		traitValue[trait] += weightedDelta;		//Reweight the current trait
	}
	//Return the selected trait value
	public float returnTrait (string traitIndex)
	{
		return traitValue[traitIndex];
	}

	public void addKill()
	{
		kills++;
		//Debug.Log(kills);
	}

}
