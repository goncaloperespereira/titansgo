using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float timer;

	public Text timerText;


	// Use this for initialization
	void Start () 
	{
		timerText = GameObject.Find ("/Canvas/Timer").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;
		if ( timer <= 0 )
		{
			GameOver();
		}

		float minutes = timer/60f;
		float seconds = timer%60f;
		timerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
	}

	public void GameOver()
	{

	}
}
