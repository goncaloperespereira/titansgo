using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float timeBetweenAITick = 3;

	[HideInInspector]
	public WaveSpawn spawner;

	//private float movementVelocity = 2;
	
	// Interface
	public void SetMovementTarget(Vector2 target) {
		destination = target;
		isDestinationValid = true;
		if (gameObject.layer == LayerMask.NameToLayer ("BottomTeamUnits")) {
			//Debug.Log ("SetMovementTarget");
		}
	}

	public void ResetMovementTarget() {
		isDestinationValid = false;
		if (gameObject.layer == LayerMask.NameToLayer ("BottomTeamUnits")) {
			//Debug.Log ("ResetMovementTarget");
		}
	}

	//
	// private
	//

	private Rigidbody2D body;
	private Vector2 destination;
	private bool isDestinationValid = false;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();

		// To be able to distinguish the troops
		if (gameObject.layer == LayerMask.NameToLayer ("TopTeamUnits")) {
			SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer> ();
			//renderer.material.color = Color.blue;
		}

		StartCoroutine (Move ());
	}

	private IEnumerator Move() {
		while (true) {
			if (isDestinationValid) {
				Vector3 sourcePos = gameObject.transform.position;
				Vector2 direction = new Vector2 (destination.x - sourcePos.x, destination.y - sourcePos.y);
				MoveInTargetDirection (direction);
			} else {
				body.velocity = new Vector2 (0, 0);
				//MoveInTargetDirection (new Vector2 ());
			}

			yield return new WaitForSeconds (timeBetweenAITick);
		}
	}

	private void MoveInTargetDirection(Vector2 direction) {
		direction.Normalize ();
		float movementVelocity = MovingObjectStats.GetMovementVelocityForObject (gameObject);
		body.velocity = new Vector2 (direction.x * movementVelocity, direction.y * movementVelocity);

	}
}
