using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GolemSpawner : NetworkBehaviour {

	public GameObject golemPrefab;
	public int numberOfGolems;


	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	public override void OnStartServer()
	{

		for (int i = 0; i < numberOfGolems; i++)
		{
			//var spawnPosition = new Vector3 (21.0f, -45.0f , 0.0f);
			var spawnRotation = Quaternion.Euler (0.0f, Random.Range (0, 0), 0.0f);

			var playerBase = (GameObject)Instantiate (golemPrefab, gameObject.transform.position, spawnRotation);
			NetworkServer.Spawn (playerBase);
		}


	}

}

