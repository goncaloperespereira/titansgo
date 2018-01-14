using UnityEngine;
using System;
using System.Collections;

public class Shot : MonoBehaviour {
	private Transform _endTransform;
	private float _velocity;
	private Action _onArrival;

	public void Startup(Transform endTansform, float velocity, Action onArrival) {
		_endTransform = endTansform;
		_velocity = velocity;
		_onArrival = onArrival;
	}

	void Update ()
	{
		try {
			if(!MovingObjectStats.IsObjectAlive(_endTransform.gameObject)) {
				Destroy(gameObject);
				return;
			}

			if(Vector3.Equals(gameObject.transform.position, _endTransform.position)) {
				if (_onArrival != null) {
					_onArrival ();
				}
			}

			gameObject.transform.position = Vector3.MoveTowards(
				gameObject.transform.position,
				_endTransform.position, 
				_velocity * Time.deltaTime);	
		}
		catch(Exception) {
			// is target destroyed?
			Destroy (gameObject);
		}
	}
}
