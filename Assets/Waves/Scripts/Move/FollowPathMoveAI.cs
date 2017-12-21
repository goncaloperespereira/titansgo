﻿using UnityEngine;
using System.Collections;

public class FollowPathMoveAI : MoveAI {
	[HideInInspector]
	public GameObject[] path;
	public float minDistanceToEachObject = 1.0f;
	public float timeBetweenAITicks = 0.2f;

	private int pathIndex = 0;
	public override MoveRequest GetRequest () {
		if (path == null || pathIndex >= path.Length) {
			return null;
		}

		FollowGameObjectRequest request = new FollowGameObjectRequest (0.5f, this, minDistanceToEachObject);
		request.targetObject = path [pathIndex];
		if (request.IsInRange ()) {
			if (++pathIndex < path.Length) {
				request.targetObject = path [pathIndex];
			} else {
				return null;
			}
		}
			
		return request;
	}
}

public class FollowGameObjectRequest : MoveRequest{
	private float squaredMinDistance;
	public FollowGameObjectRequest(float priority, MoveAI ai, float distance): base(priority, ai) {
		squaredMinDistance = distance*distance;
	}

	public GameObject targetObject;

	public override void StartAction() {
		TickAction ();
	}

	public override bool TickAction() {
		if (!MovingObjectStats.IsObjectAlive (targetObject)) {
			moveAI.ResetMovementTarget ();
			return false;
		}

		if (!IsInRange ()) {
			moveAI.SetMovementTarget (targetObject.transform.position);
			return true;
		} else {
			moveAI.ResetMovementTarget ();
			return false;
		}
	}

	public override bool CanInterrupt () {
		return true;
	}

	public override bool Interrupt () {
		moveAI.ResetMovementTarget ();
		return true;
	}


	public bool IsInRange() {
		Vector3 delta = targetObject.transform.position - moveAI.gameObject.transform.position;
		return delta.sqrMagnitude < squaredMinDistance;
	}
}
