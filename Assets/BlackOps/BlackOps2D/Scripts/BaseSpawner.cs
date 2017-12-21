using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BaseSpawner : NetworkBehaviour 
{

	//private NetworkStartPosition baseSpawnPoint;

	public GameObject basePrefab;
	public int numberOfBases;


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

		for (int i = 0; i < numberOfBases; i++)
		{
				var spawnPosition = new Vector3 (0.0f, -45.0f , 0.0f);
				var spawnRotation = Quaternion.Euler (0.0f, Random.Range (0, 0), 0.0f);

				var playerBase = (GameObject)Instantiate (basePrefab, spawnPosition, spawnRotation);
				NetworkServer.Spawn (playerBase);
		}
			

	}
		
}
