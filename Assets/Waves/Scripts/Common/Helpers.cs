using UnityEngine;
using System.Collections;

public class Helpers
{
	public static void DestroyObject(GameObject obj) {
		Vector3 pos = new Vector3(10000, 10000);
		obj.transform.position = pos; // yes, we do this to make sure ontriggerexit is called before the object is actually destroyed
		//obj.SetActive (false);
		GameObject.Destroy(obj, 1.0f);
	}
}

