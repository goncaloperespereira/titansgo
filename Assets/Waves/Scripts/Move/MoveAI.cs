using UnityEngine;
using System.Collections;

public abstract class MoveRequest : AIRequest{
	protected MoveAI moveAI;
	public MoveRequest (float priority, MoveAI ai) : base (priority){
		moveAI = ai;
	}

}

public abstract class MoveAI : MonoBehaviour {

	private Movement movement;

	// Use this for initialization
	protected virtual void Start () {
		movement = GetComponent<Movement> ();
		Debug.Assert (movement != null, "MovementAI needs a Movement component");

		// this should be done last to make sure the rest is properly initialized
		HighLevelFunction hlf = GetComponent<HighLevelFunction> ();
		hlf.SetMoveAI (this);
	}

	public void SetMovementTarget(Vector2 target) {
		movement.SetMovementTarget (target);
	}

	public void ResetMovementTarget() {
		movement.ResetMovementTarget ();
	}

	public abstract MoveRequest GetRequest ();
}
