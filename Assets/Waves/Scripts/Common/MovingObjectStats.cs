using UnityEngine;
using System.Collections;

public class MovingObjectStats : MonoBehaviour {

	public float priority = 1; // lowest is more important
	public float maxHealth = 100;
	public float attackActionTime = 0.5f;
	public float attackCooldown = 2.0f;
	public float attackDamage = 30;
	public float movementVelocity = 3;
	public float timeBetweenAttackStartAndDamageIsDelt = 0.3f;
	public float attackRange = 1;

	public float health;
	void Start() {
		health = maxHealth;
	}


	public static float GetAttackRangeForObject(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.attackRange;
		} 

		return 1.0f;
	}

	public static float GetAttackActionTimeForObject(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.attackActionTime;
		} 

		return 1.0f;
	}

	public static float GetAttackColldownForObject(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.attackCooldown;
		} 

		return 1.0f;
	}

	public static float GetAttackDamageForObject(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.attackDamage;
		} 

		return 1.0f;
	}

	public static float GetMovementVelocityForObject(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.movementVelocity;
		} 

		return 1.0f;
	}

	// attack cooldown
	private float attackCooldownTimestamp = 0;
	public static void StartAttackColldownForObject(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			objectStats.attackCooldownTimestamp = Time.time + objectStats.attackCooldown;
		} 
	}

	public static bool IsInAttackCooldown(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.attackCooldownTimestamp > Time.time;
		}

		return false;
	}

	// health
	public static float GetHealthForObject(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.health;
		} 

		return -1.0f;
	}

	public static bool DealDamageFromObjectToObject(GameObject attackingObject, GameObject damagedObject) {
		MovingObjectStats damagedObj = damagedObject.GetComponent<MovingObjectStats> ();
		if (damagedObj != null) {
			float damage = MovingObjectStats.GetAttackDamageForObject(attackingObject);
			return damagedObj.DealDamage (damage);
		}

		return false;
	}

	public static bool IsObjectAlive(GameObject obj) {
		MovingObjectStats objectStats = obj.GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.health > 0;
		} 

		return true; // if the object is not alive, then is should be considered as a target anyway
	}

	public bool DealDamage(float damage) {
		health -= damage;
		if (health <= 0.0f) {
			health = 0.0f;
			//Debug.Log ("Object Destoyed " + this.name);
			Helpers.DestroyObject (this.gameObject);
			return true;
		}

		return false;
	}
}
