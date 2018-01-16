using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	public int health;

	// Use this for initialization
	void Start () 
	{
		health = gameObject.GetComponent<Health> ().currentHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)	
		{
			Destroy (gameObject);
		}
	}
		
}
