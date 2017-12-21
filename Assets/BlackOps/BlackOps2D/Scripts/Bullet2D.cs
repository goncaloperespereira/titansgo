using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet2D : NetworkBehaviour {

	// Use this for initialization
	void Start () 
	{

	}


	void OnCollisionEnter2D(Collision2D collision)	
	{

		var hit = collision.gameObject;
		var health = hit.GetComponent<Health> ();

		if (health != null)
		{
			health.TakeDamage (10);
		}

		//Destroy (gameObject);
	}
}
