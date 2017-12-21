using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WaveSpawn : NetworkBehaviour {
	public GameObject objectToSpawn;
	public int totalObjectsInWave;
	public float timeBetweenSpawn;
	public float timeBetweenWave;
	public GameObject[] lane1;
	//public GameObject[] lane2;

	// Use this for initialization
	public override void OnStartServer () {
		StartCoroutine (SpawnWave());
	}

	IEnumerator SpawnWave() {
		GameObject[] lastLane = lane1;
		while (true) {
			for (int i = 0; i < totalObjectsInWave; i++) {
				yield return new WaitForSeconds (timeBetweenSpawn);

				GameObject clone = Instantiate (objectToSpawn, gameObject.transform.position, Quaternion.identity) as GameObject;
				clone.layer = gameObject.layer;

				Movement movement = clone.GetComponent<Movement> ();
				movement.spawner = this;

				FollowPathMoveAI moveAI = clone.GetComponent<FollowPathMoveAI> ();
				/*moveAI.path = lastLane == lane1 ? lane2 : lane1;
				lastLane = moveAI.path;*/
				moveAI.path = lane1;
			}

			yield return new WaitForSeconds (timeBetweenWave);
		}

		//yield return null;
	}
}
