using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float timer;

	public Text timerText;
	public Text winnerText;


	GameObject spawnPoint1;
	GameObject spawnPoint2;

	public GameObject topBase;
	public GameObject bottomBase;

	// Use this for initialization
	void Start () 
	{
		timerText = GameObject.Find ("/Canvas/Timer").GetComponent<Text> ();
		winnerText = GameObject.Find ("/Canvas/Winner").GetComponent<Text> ();


		topBase = GameObject.Find ("Base2DTop(Clone)");
		bottomBase = GameObject.Find ("Base2DBottom(Clone)");
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

		identifyWinner ();
	}

	public void GameOver()
	{

	}

	public void GetSpawnPoint()	
	{

	}

	public void identifyWinner()
	{
		if (topBase.GetComponent<Health>().currentHealth <= 0 && bottomBase.GetComponent<Health>().currentHealth > 0)	
		{
			Debug.Log ("Bottom Wins!!");
			winnerText.text = ("P2 VICTORY!!");
			Time.timeScale = 0.0f;


		}

		if (topBase.GetComponent<Health>().currentHealth > 0 && bottomBase.GetComponent<Health>().currentHealth <= 0)	
		{
			Debug.Log ("Top Wins!!");
			winnerText.text = ("P1 VICTORY!");
			Time.timeScale = 0.0f;


		}
	}
}
