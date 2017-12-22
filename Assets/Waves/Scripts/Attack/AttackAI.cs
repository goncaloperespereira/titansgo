using UnityEngine;
using System.Collections;

public class AttackRequest : AIRequest{
	protected AttackAI attackAI;
	protected GameObject objectToAttack;
	protected FollowGameObjectRequest fgoRequest;
	public AttackRequest (float priority, AttackAI ai, GameObject targetObject) : base (priority){
		attackAI = ai;
		objectToAttack = targetObject;

		// check if we are in range
		float attackRange = MovingObjectStats.GetAttackRangeForObject (ai.gameObject);
		MoveAI moveAI = ai.GetComponent<MoveAI> ();
		fgoRequest = new FollowGameObjectRequest (0.5f, moveAI, attackRange);
		fgoRequest.targetObject = objectToAttack;
	}
		
	float attackFinishTimestamp;
	float attackColldownTimestamp;
	void StartAttack() {
		if (MovingObjectStats.IsInAttackCooldown(attackAI.gameObject)) {
			return;
		} else {
			Debug.Log (attackAI.gameObject.name + " Attack");

			attackFinishTimestamp = Time.time + MovingObjectStats.GetAttackActionTimeForObject (attackAI.gameObject);
			MovingObjectStats.StartAttackColldownForObject(attackAI.gameObject);
		}
	}

	public override void StartAction() {
		if (fgoRequest != null && fgoRequest.IsInRange ()) {
			fgoRequest = null;
			StartAttack ();
		} else {
			fgoRequest.StartAction ();
		}
	}

	public bool damageDelt = false;
	public override bool TickAction() {
		if (fgoRequest == null) {
			if (Time.time < attackFinishTimestamp) {
				if (!damageDelt) {
					MovingObjectStats.DealDamageFromObjectToObject (attackAI.gameObject, objectToAttack);
					damageDelt = true;
				}
				return true;
			}

			return false;
		} else {
			if (fgoRequest.TickAction ()) {
				return true;
			} else {
				fgoRequest = null;
				StartAttack ();
				return true;
			}
		}
	}

	public override bool CanInterrupt () {
		return false;
	}

	public override bool Interrupt () {
		return false;
	}
}

public class AttackAI : MonoBehaviour {

	//private Attack attack;
	private EvaluateVisibleObjects visibleObjectsPrioritiser;

	// Use this for initialization
	protected virtual void Start () {
		//attack = GetComponent<Attack> ();
		//Debug.Assert (attack != null, "AttackAI needs an Attack component");

		visibleObjectsPrioritiser = GetComponent<EvaluateVisibleObjects> ();

		// this should be done last to make sure the rest is properly initialized
		HighLevelFunction hlf = GetComponent<HighLevelFunction> ();
		hlf.SetAttackAI (this);
	}

	public virtual AttackRequest GetRequest () {
		if (visibleObjectsPrioritiser.GetBestTargetInSight () == null) {
			return null;
		}

		AttackRequest request = new AttackRequest (1.0f, this, visibleObjectsPrioritiser.GetBestTargetInSight());
		return request;

	}
}

