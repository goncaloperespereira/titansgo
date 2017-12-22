using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineOfSight : MonoBehaviour {
	[HideInInspector]
	public GameObject[] objectsInSight;

	[HideInInspector]
	public float radius;

	private HashSet<Collider2D> collidersInside;
	private bool setChanged = false;

	// Use this for initialization
	void Start () {
		collidersInside = new HashSet<Collider2D> ();
		CircleCollider2D collider = GetComponent<CircleCollider2D> ();
		radius = collider.radius;
	}

	void Update () {
		if (setChanged) {
			try{
				objectsInSight = new GameObject[collidersInside.Count];
				int i = 0;
				foreach(Collider2D collider in collidersInside) {
					objectsInSight [i] = collider.gameObject;
					i++;
				}
			} catch(System.Exception e) {
				collidersInside.Clear ();
			}
		} else {
			setChanged = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer != gameObject.transform.parent.gameObject.layer) {
			MovingObjectStats stats = other.GetComponent<MovingObjectStats> ();
			if (stats != null) {
				collidersInside.Add (other);
				setChanged = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		setChanged = true;
		collidersInside.Remove (other);
	}
}
