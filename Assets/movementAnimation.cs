using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementAnimation : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		{
			anim.SetTrigger("move_down");
		}
	}
}
