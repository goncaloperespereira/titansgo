using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MovingObjectStats : NetworkBehaviour {

	public float priority = 1; // lowest is more important
	public int maxHealth = 100;
	public float attackActionTime = 0.5f;
	public float attackCooldown = 2.0f;
	public int attackDamage = 30;
	public float movementVelocity = 3;
	public float attackRange = 1;

	private MovingObjectStats _redirectStats = null;

	public void SetSedirectStats(MovingObjectStats redirect) {
		_redirectStats = redirect;
	}

	//public float health;
	void Start() {
		//health = maxHealth;
	}


	public static float GetAttackRangeForObject(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.attackRange;
		} 

		return 1.0f;
	}

	public static float GetObjectPriority(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.priority;
		} 

		return 1.0f;
	}

	public static MovingObjectStats getMovingObjectStats(GameObject obj) {
		MovingObjectStats stats = obj.GetComponent<MovingObjectStats> ();
		if (stats != null && stats._redirectStats != null) {
			stats = stats._redirectStats;
		}

		return stats;
	}

	public static float GetAttackActionTimeForObject(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.attackActionTime;
		} 

		return 1.0f;
	}

	public static float GetAttackColldownForObject(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.attackCooldown;
		} 

		return 1.0f;
	}

	public static int GetAttackDamageForObject(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.attackDamage;
		} 

		return 1;
	}

	public static float GetMovementVelocityForObject(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.movementVelocity;
		} 

		return 1.0f;
	}

	public static int GetMaxHealthForObject(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.maxHealth;
		} 

		return 100;
	}

	// attack cooldown
	private float attackCooldownTimestamp = 0;
	public static void StartAttackColldownForObject(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			objectStats.attackCooldownTimestamp = Time.time + objectStats.attackCooldown;
		} 
	}

	public static bool IsInAttackCooldown(GameObject obj) {
		MovingObjectStats objectStats = getMovingObjectStats (obj);
		if (objectStats != null) {
			return objectStats.attackCooldownTimestamp > Time.time;
		}

		return false;
	}

	// health
	public static int GetHealthForObject(GameObject obj) {
		Health h = obj.GetComponent<Health> ();
		if (h != null) {
			return h.currentHealth;
		} 

		return 0;
	}

	public static bool DealDamageFromObjectToObject(GameObject attackingObject, GameObject damagedObject) {
		MovingObjectStats damagedObj = getMovingObjectStats (damagedObject);
		if (damagedObj != null) {
			int damage = MovingObjectStats.GetAttackDamageForObject(attackingObject);
			return damagedObj.DealDamage (damage);
		}

		return false;
	}

	public static bool IsObjectAlive(GameObject obj) {
		Health h = obj.GetComponent<Health> ();
		if (h != null) {
			return h.currentHealth > 0;
		} 

		return true; // if the object is not alive, then it should be considered as a target anyway
	}

	public bool DealDamage(int damage) {
		Health h = gameObject.GetComponent<Health> ();
		if (h) {
			h.TakeDamage (damage);

		}

		return false;
	}

	public static float ComputeRadiusOfObject(GameObject obj) {
		CircleCollider2D collider = obj.GetComponent<CircleCollider2D> ();
		if (collider != null) {
			return collider.radius;
		}

		BoxCollider2D bcollider = obj.GetComponent<BoxCollider2D> ();
		if (bcollider != null) {
			return bcollider.bounds.extents.x * 0.5f + bcollider.bounds.extents.y *0.5f;
		}

		return 0.0f;
	}
}
