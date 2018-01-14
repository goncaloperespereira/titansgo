using UnityEngine;
using System.Collections;

public abstract class AIRequest{
	public float priority;

	public AIRequest(float prio) {
		priority = prio;
	}

	public abstract void StartAction();
	public abstract bool TickAction();
	public abstract bool CanInterrupt ();
	public abstract bool Interrupt ();

	/*
	private GameObject _gameObject;
	public GameObject gameObject {
		get { return _gameObject; }
		set { _gameObject = value; }
	}

	public virtual MovingObjectStats getMovingObjectStats () {
		if (_gameObject == null)
			return null;
		return _gameObject.GetComponent<MovingObjectStats> ();
	}
	*/
}