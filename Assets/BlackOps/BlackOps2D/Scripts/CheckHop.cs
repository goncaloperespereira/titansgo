using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckHop : MonoBehaviour {


	public GameObject player;

	void Update () 
	{
		giveLayer ();
	}

	public void giveLayer () 
	{
		if (player.layer == 8)	
		{
			gameObject.layer = 13;
		}

		if (player.layer == 9)	
		{
			gameObject.layer = 14;
		}
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Golem_1" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,255f);
			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Button> ().interactable = true;
			Debug.Log ("Can combine!");
			player.GetComponent<PlayerController2D>().canHopGolem1 = true;

		}

		if(other.tag == "Golem_2" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,255f);
			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Button> ().interactable = true;
			Debug.Log ("Can combine!");
			player.GetComponent<PlayerController2D>().canHopGolem2 = true;

		}

	}

	public void OnTriggerExit2D (Collider2D other)
	{
		if(other.tag == "Golem_1" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,80f);
			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Button> ().interactable = false;

			Debug.Log ("Can't combine!");

			player.GetComponent<PlayerController2D>().canHopGolem1 = false;

		}

		if(other.tag == "Golem_2" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,80f);
			player.GetComponent<PlayerController2D>().hopButton.GetComponent<Button> ().interactable = false;

			Debug.Log ("Can't combine!");
			player.GetComponent<PlayerController2D>().canHopGolem2 = false;

		}

	}
}
