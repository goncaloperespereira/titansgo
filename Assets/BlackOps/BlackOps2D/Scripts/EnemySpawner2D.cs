using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner2D : NetworkBehaviour {

	public GameObject enemyPrefab;
	public int numberOfEnemies;



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public override void OnStartServer()
	{
		for (int i = 0; i < numberOfEnemies; i++)
		{
			var spawnPosition = new Vector3 (Random.Range (-25.0f,25.0f), Random.Range (-30.0f,40.0f), 0.0f);
			var spawnRotation = Quaternion.Euler (0.0f, Random.Range (0, 0), 0.0f);

			var enemy = (GameObject)Instantiate (enemyPrefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn (enemy);
		}
	}
}
