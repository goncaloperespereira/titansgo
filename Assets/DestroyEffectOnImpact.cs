using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffectOnImpact : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.gameObject.layer == 8)	
		{
			gameObject.GetComponent<Rigidbody2D> ().AddForce(-Vector3.up * speed * Time.deltaTime);
		}

		if (this.gameObject.layer == 9)	
		{
			gameObject.GetComponent<Rigidbody2D> ().AddForce(Vector3.up * speed * Time.deltaTime);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)	
	{
		if (collision.gameObject.layer != this.gameObject.layer)	
		{
			Destroy (gameObject);
		}
	}



}
