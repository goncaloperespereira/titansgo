using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvaluateVisibleObjects : MonoBehaviour {

	[HideInInspector]
	public Dictionary<GameObject, float> objectEvaluation;
	public GameObject objectWithLowestScore;

	private LineOfSight lineOfSight;

	// Use this for initialization
	void Start () {
		lineOfSight = GetComponentInChildren<LineOfSight> ();
		Debug.Assert (lineOfSight != null, "Line of sight script not found. Cannot evaluate anyone.");
	}

	float GetObjectPriorityStat(GameObject obj) {
		MovingObjectStats objectStats = GetComponent<MovingObjectStats> ();
		if (objectStats != null) {
			return objectStats.priority;
		} 

		return 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		objectEvaluation = new Dictionary<GameObject, float> ();
		float minScore = float.MaxValue;
		objectWithLowestScore = null;
		foreach(GameObject obj in lineOfSight.objectsInSight) {
			if (obj != null) {
				
				Vector3 delta = obj.transform.position - gameObject.transform.position;
				float score = delta.sqrMagnitude * GetObjectPriorityStat(obj);
				objectEvaluation [obj] = score;
				if (score < minScore) {
					minScore = score;
					objectWithLowestScore = obj;
				}
			}
		}
	}

	public GameObject GetBestTargetInSight() {
		return objectWithLowestScore;
	}
}
