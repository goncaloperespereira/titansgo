using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour 
{

	private int maxHealth;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth;

	public RectTransform healthBar;

	public bool destroyOnDeath;

	private NetworkStartPosition[] spawnPoints;

	public GameObject Player;

	public GameObject takeDamageParticleEffect;
	public GameObject dieParticleEffect;

	private float healthBarWidth;
	// Use this for initialization
	void Start () 
	{
		healthBarWidth =healthBar.sizeDelta.x;

		maxHealth = MovingObjectStats.GetMaxHealthForObject (gameObject);
		currentHealth = maxHealth;

		if(isLocalPlayer)	
		{
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(currentHealth <= 0 && this.gameObject.tag == "Golem_1")
		{
			Player.GetComponent<PlayerController2D> ().canHopGolem1 = false;
			Helpers.DestroyObject (gameObject);

		}

		if(currentHealth <= 0 && this.gameObject.tag == "Golem_2")
		{
			Player.GetComponent<PlayerController2D> ().canHopGolem2 = false;
			Helpers.DestroyObject (gameObject);
		}
	}
		
	public bool TakeDamage (int amount) 
	{
		if(!isServer)
			return false;
		
		//Reduce Health by the amount of damage
		currentHealth -= amount;

		Instantiate (takeDamageParticleEffect, transform.position, transform.rotation);

		if (currentHealth <= 0) {

			Instantiate (dieParticleEffect, transform.position, transform.rotation);

			if (this.gameObject.tag == "Player_Combined") {
				this.gameObject.GetComponent<PlayerController2D> ().CmdHop ();
			} else {
				if(destroyOnDeath)
				{
					Helpers.DestroyObject (gameObject);
				} else 
				{
					currentHealth = maxHealth;

					RpcRespawn ();
				}

				return true;
			}
		}

		return false;
	}

	void OnChangeHealth(int currentHealth)
	{
		healthBar.sizeDelta = new Vector2 (((float)currentHealth)/((float)maxHealth)*healthBarWidth, healthBar.sizeDelta.y);
		healthBar.transform.parent.gameObject.SetActive (currentHealth != maxHealth);
			
	}

	[ClientRpc]
	void RpcRespawn()
	{
		Vector3 spawnPoint = Vector3.zero;

		if (isLocalPlayer)
		{
			if (gameObject.layer == 8)
			{
				spawnPoint = spawnPoints [0].transform.position;
				transform.position = spawnPoint;
			}

			if (gameObject.layer == 9)
			{
				spawnPoint = spawnPoints [1].transform.position;
				transform.position = spawnPoint;
			}
				
		}
			
	}
}
