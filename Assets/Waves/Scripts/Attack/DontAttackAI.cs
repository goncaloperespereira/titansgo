using System;
using UnityEngine;

public class DontAttackAI : AttackAI {
	public override AttackRequest GetRequest () {
		return null;
	}
}
