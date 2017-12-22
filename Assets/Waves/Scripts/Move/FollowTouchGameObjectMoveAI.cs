using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTouchGameObjectMoveAI : MoveAI {

	public GameObject gameObjectToFollow;

	public override MoveRequest GetRequest () {
		if (gameObjectToFollow == null) {
			return null;
		} else {
			float attackRange = MovingObjectStats.GetAttackRangeForObject (gameObject);

			if (gameObjectToFollow.tag == "Referencial") {
				attackRange = 3.0f;
			}

			FollowGameObjectRequest request = new FollowGameObjectRequest (0.5f, this, attackRange);
			request.targetObject = gameObjectToFollow;
			if (request.IsInRange ()) {
				return null;
			}
			return request;
		}
	}
}