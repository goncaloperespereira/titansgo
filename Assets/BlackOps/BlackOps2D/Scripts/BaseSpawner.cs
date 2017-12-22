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

		var spawnRotation = Quaternion.identity;

		var playerBase = (GameObject)Instantiate (basePrefab, gameObject.transform.position, spawnRotation);
		playerBase.layer = gameObject.layer;
		playerBase.tag = gameObject.tag;
		NetworkServer.Spawn (playerBase);

		Destroy (gameObject);

	}
		
}
