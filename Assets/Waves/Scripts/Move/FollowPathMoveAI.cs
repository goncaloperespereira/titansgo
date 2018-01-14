using UnityEngine;
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
	//private float squaredMinDistance;
	private float minDistance;
	public FollowGameObjectRequest(float priority, MoveAI ai, float distance): base(priority, ai) {
		minDistance = distance;
		//squaredMinDistance = distance*distance;
	}

	private GameObject _targetObject;
	public GameObject targetObject
	{
		get { return _targetObject; }
		set { 
			_targetObject = value;
			if (_targetObject != null) {
				targetObjectRadius = MovingObjectStats.ComputeRadiusOfObject (_targetObject);
			} else {
				targetObjectRadius = 0.0f;
			}
		}
	}
	private float targetObjectRadius;

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
		return DistanceToObject () < (minDistance + targetObjectRadius);
	}

	public float DistanceToObject() {
		Vector3 delta = targetObject.transform.position - moveAI.gameObject.transform.position;
		return delta.magnitude;
	}
}
