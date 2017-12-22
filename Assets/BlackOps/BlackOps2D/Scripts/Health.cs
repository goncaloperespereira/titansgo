using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour 
{

	//public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth;

	public RectTransform healthBar;

	public bool destroyOnDeath;

	private NetworkStartPosition[] spawnPoints;

	public GameObject Player;

	// Use this for initialization
	void Start () 
	{
		currentHealth = MovingObjectStats.GetMaxHealthForObject (gameObject);

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

		if(currentHealth <= 0 && this.gameObject.tag == "Player")
		{

			if(destroyOnDeath)
			{
				Helpers.DestroyObject (gameObject);
			}

			else

			{
				currentHealth = MovingObjectStats.GetMaxHealthForObject (gameObject);

				RpcRespawn ();
			}

			return true;
		}

		if(currentHealth <= 0 && this.gameObject.tag == "Player_Combined")
		{
			
			if(destroyOnDeath)
			{
				Helpers.DestroyObject (gameObject);
			}

			else

			{

				this.gameObject.GetComponent<PlayerController2D> ().CmdHop ();
				//RpcRespawn ();
			}
			return true;
		}

		if(currentHealth <= 0 && this.gameObject.tag == "Golem_1")
		{

			if(destroyOnDeath)
			{
				Helpers.DestroyObject (gameObject);
			}

			else

			{
				currentHealth = MovingObjectStats.GetMaxHealthForObject (gameObject);

				//RpcRespawn ();
			}
			return true;
		}

		if(currentHealth <= 0 && this.gameObject.tag == "Golem_2")
		{

			if(destroyOnDeath)
			{
				Helpers.DestroyObject (gameObject);
			}

			else

			{
				currentHealth = MovingObjectStats.GetMaxHealthForObject (gameObject);

				//RpcRespawn ();
			}

			return true;
		}

		return false;

	}

	void OnChangeHealth(int currentHealth)
	{
		healthBar.sizeDelta = new Vector2 (currentHealth, healthBar.sizeDelta.y);

	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			Vector3 spawnPoint = Vector3.zero;

			if(spawnPoints != null && spawnPoints.Length > 0)	
			{
				spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
			}

			transform.position = spawnPoint;
		}
	}
}
