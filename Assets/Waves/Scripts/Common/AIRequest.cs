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
}


//public abstract class AIAction {
//	public abstract AI
//}