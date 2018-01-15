using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FindCamera : NetworkBehaviour 
{

	public GameObject MainCamera;
	// Use this for initialization
	void Start () 
	{
		MainCamera = GameObject.Find ("Main Camera");
		if (isLocalPlayer)
		{
			MainCamera.transform.parent = this.transform;
			MainCamera.transform.localPosition = new Vector3 (0, 0, -10);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
