﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GolemSpawner : NetworkBehaviour {

	public GameObject golemPrefab;

	public override void OnStartServer()
	{
		var spawnRotation = Quaternion.identity;

		var playerBase = (GameObject)Instantiate (golemPrefab, gameObject.transform.position, spawnRotation);
		playerBase.layer = gameObject.layer;
		playerBase.tag = gameObject.tag;
		NetworkServer.Spawn (playerBase);

		Destroy (gameObject);
	}

}

