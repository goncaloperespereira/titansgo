﻿using UnityEngine;
using System.Collections;

public class HighLevelFunction : MonoBehaviour {

	private MoveAI moveAI;
	public void SetMoveAI(MoveAI ai) {
		moveAI = ai;
		AttemptStartup ();
	}

	private AttackAI attackAI;
	public void SetAttackAI(AttackAI ai) {
		attackAI = ai;
		AttemptStartup ();
	}

	bool AttemptStartup() {
		if (moveAI != null && attackAI != null) {
			StartCoroutine(StartAI ());
			return true;
		}

		return false;
	}

	IEnumerator StartAI() {
		while (true) {
			MoveRequest moveRequest = moveAI.GetRequest ();
			AttackRequest attackRequest = attackAI.GetRequest();

			if (attackRequest != null) {
				attackRequest.StartAction ();

				do {
					yield return new WaitForFixedUpdate ();
				} while (attackRequest.TickAction ());
			} else {
				if (moveRequest != null) {
					moveRequest.StartAction ();

					do {
						yield return new WaitForFixedUpdate ();
						attackRequest = attackAI.GetRequest();
						if(attackRequest != null && moveRequest.CanInterrupt()) {
							moveRequest.Interrupt();
							break;
						}
					} while (moveRequest.TickAction ());
				} else {
					yield return new WaitForFixedUpdate ();
				}
			}
		}
	}
}
