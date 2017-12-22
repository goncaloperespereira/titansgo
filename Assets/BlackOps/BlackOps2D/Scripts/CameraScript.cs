using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraScript : NetworkBehaviour {

	public GameObject player;

	public float smoothTimeY;
	public float smoothTimeX;

	private Vector2 velocity;

	public bool bounds;

	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	public Transform playerTransform;
	public int depth = -20;

	
	// Update is called once per frame
/*	void FixedUpdate () 
	{


		if (player != null) {

				float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
				float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

				transform.position = new Vector3 (posX, posY, transform.position.z);

				if (bounds) {
					transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x),
						Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y),
						Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
				}
			}
	}

	public void OnStartClient()
	{
		player = GameObject.Find ("Player(Clone)");
	}*/

	void Update()
	{
		if(playerTransform != null)
		{
				transform.position = playerTransform.position + new Vector3(0,10,depth);
				if (bounds) {
					transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x),
						Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y),
						Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
				}
		}
	}

	public void setTarget(Transform target)
	{
		playerTransform = target;
	}
}
