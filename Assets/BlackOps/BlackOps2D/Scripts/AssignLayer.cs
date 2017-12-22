using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignLayer : MonoBehaviour {

	public GameObject golem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		giveLayer ();
		giveTag ();
	}

	public void giveLayer () 
	{
		if (golem.layer == 8)	
		{
			gameObject.layer = 13;
		}

		if (golem.layer == 9)	
		{
			gameObject.layer = 14;
		}
	}

	public void giveTag()
	{
		if (golem.tag == "Golem_1")	
		{
			gameObject.tag = "Golem_1";
		}

		if (golem.tag == "Golem_2")	
		{
			gameObject.tag = "Golem_2";
		}
	}
}
