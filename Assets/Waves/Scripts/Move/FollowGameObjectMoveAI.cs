using UnityEngine;
using System.Collections;

public class FollowGameObjectMoveAI : MoveAI {

	public GameObject gameObjectToFollow;

	public override MoveRequest GetRequest () {
		if (gameObjectToFollow == null) {
			return null;
		} else {
			float attackRange = MovingObjectStats.GetAttackRangeForObject (gameObject);
			FollowGameObjectRequest request = new FollowGameObjectRequest (0.5f, this, attackRange);
			request.targetObject = gameObjectToFollow;
			if (request.IsInRange ()) {
				return null;
			}
			return request;
		}
	}
}
