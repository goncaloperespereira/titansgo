using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class PlayerController2D : NetworkBehaviour
{

	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	//public GameObject gobzillaPrefab;
	//public Transform gobzillaSpawn;

	public GameObject shootButton;
	public GameObject hopButton;
	public float moveSpeed;

	Transform target;
	Vector2 targetPos;

	public bool canMove = true;
	public bool hopped;

	//sprites
	public Sprite gobzillaSprite;
	public Sprite goblinSprite;
	public Sprite hopSprite;
	public Sprite unhopSprite;

	//Golems
	public GameObject Golem_1;
	public GameObject Golem_2;

	//storage for health before combination
	//[SerializeField]
	int storeTempHealth; 


	//bools to check which Golem the player can hop
	public bool canHopGolem1;
	public bool canHopGolem2;
	public bool hoppedGolem1;
	public bool hoppedGolem2;

	private GameObject donkeyGolem;

	public GameObject touchTarget;

	void Awake()	
	{
		GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag ("spawnPoint");
		var distance = Vector3.Distance(transform.position, spawnPoint[0].transform.position);
		if (distance < 1)
		{
			gameObject.layer = spawnPoint [0].layer;		
		}
		else		
		{
			gameObject.layer = spawnPoint [1].layer;		
		}
	}

	private void Start()
	{
		targetPos = transform.position;
	}

	void Update()
	{

		if(!isLocalPlayer)
		{
			return;
		}

		if (EventSystem.current.IsPointerOverGameObject() /* || EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)*/)
		{
			canMove = false;
		}
		else			
		{
			canMove = true;
		}

		if(Input.GetMouseButton(0) && canMove == true || Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Moved && canMove == true)
		{
			targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

				
			if((Vector2)transform.position != targetPos)
			{
				touchTarget.transform.position = targetPos;
			}
		}

		if (!Input.GetMouseButton (0) || Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && canMove == true) {
			touchTarget.transform.localPosition = new Vector3();
		}

		dontAllowHopOn2 ();

	}

	[Command]
	public void CmdHop()		
	{
		hopped = !hopped;
		if (hopped) {
			GameObject[] golem1s = GameObject.FindGameObjectsWithTag ("Golem_1");
			GameObject[] golem2s = GameObject.FindGameObjectsWithTag ("Golem_2");

			List<GameObject> golemList = new List<GameObject> ();
			foreach (GameObject golem in golem1s) {
				if (golem.layer == gameObject.layer) {
					golemList.Add (golem);
				}
			}
			foreach (GameObject golem in golem2s) {
				if (golem.layer == gameObject.layer) {
					golemList.Add (golem);
				}
			}

			foreach (GameObject golem in golemList) {
				if (canHopGolem1 == true) {
					if (golem.tag == "Golem_1") {
						donkeyGolem = golem;

						//change tag
						gameObject.tag = "Player_Combined"; 

						//Change Sprite and scale to the combined object
						gameObject.GetComponent<SpriteRenderer> ().sprite = gobzillaSprite;
						gameObject.transform.localScale = new Vector3 (6.5f, 6.5f, 6.5f);

						MovingObjectStats alternateStats = gameObject.GetComponent<GotanMovingObjectStats> ();
						gameObject.GetComponent<MovingObjectStats> ().SetSedirectStats (alternateStats);

						//store current health on a temporary variable
						storeTempHealth = gameObject.GetComponent<Health> ().currentHealth;

						//get health from Golem and assign it to Player
						gameObject.GetComponent<Health> ().currentHealth = golem.GetComponent<Health> ().currentHealth;

						golem.SetActive (false);

						hopButton.GetComponent<Button> ().interactable = true;

						hoppedGolem1 = true;
					}
				}

				if (canHopGolem2 == true) {
					if (golem.tag == "Golem_2") {
						donkeyGolem = golem;

						//change tag
						gameObject.tag = "Player_Combined"; 

						//Change Sprite and scale to the combined object
						gameObject.GetComponent<SpriteRenderer> ().sprite = gobzillaSprite;
						gameObject.transform.localScale = new Vector3 (6.5f, 6.5f, 6.5f);

						MovingObjectStats alternateStats = gameObject.GetComponent<GotanMovingObjectStats> ();
						gameObject.GetComponent<MovingObjectStats> ().SetSedirectStats (alternateStats);

						//store current health on a temporary variable
						storeTempHealth = gameObject.GetComponent<Health> ().currentHealth;

						//get health from Golem and assign it to Player
						gameObject.GetComponent<Health> ().currentHealth = golem.GetComponent<Health> ().currentHealth;

						golem.SetActive (false);

						hopButton.GetComponent<Button> ().interactable = true;

						hoppedGolem2 = true;
					}
				}
			}
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer> ().sprite = goblinSprite;
			gameObject.transform.localScale = new Vector3 (6.5f, 6.5f, 5.0f);

			donkeyGolem.SetActive (true);
			//get health from player and reassign to goelm
			donkeyGolem.GetComponent<Health>().currentHealth = gameObject.GetComponent<Health>().currentHealth;

			Vector3 newPosition = transform.position + new Vector3 (5.0f, 0.0f, 0.0f);
			donkeyGolem.transform.position = newPosition;

			gameObject.GetComponent<MovingObjectStats> ().SetSedirectStats (null);

			//reassign old health to the Goblin
			gameObject.GetComponent<Health> ().currentHealth = storeTempHealth;

			hopButton.GetComponent<Button> ().interactable = false;

			hoppedGolem1 = false;
			hoppedGolem2 = false;
			donkeyGolem = null;

			//change tag
			gameObject.tag = "Player"; 
		}
	} 

	public void dontAllowHopOn2()
	{
		if (canHopGolem1 == true)
		{
			canHopGolem2 = false;
		}

		if (canHopGolem2 == true)
		{
			canHopGolem1 = false;
		}
	}

	public void CheckHopSprite()
	{
		if (hopped == true)
		{
			hopButton.GetComponent<Image> ().sprite = unhopSprite;
		}
		else
		{
			hopButton.GetComponent<Image> ().sprite = hopSprite;
		}
	}
		

	public override void OnStartLocalPlayer()	
	{
		//GetComponent<SpriteRenderer> ().color = Color.blue;
		Camera.main.GetComponent<CameraScript> ().setTarget (gameObject.transform);


		shootButton = GameObject.Find ("ButtonA");
		hopButton = GameObject.Find ("ButtonB");
		Golem_1 = GameObject.FindGameObjectWithTag("Golem_1");
		Golem_2 = GameObject.FindGameObjectWithTag("Golem_2");

		shootButton.GetComponent<Button>().onClick.AddListener (() => {
			//CmdFire ();

		});

		hopButton.GetComponent<Button>().onClick.AddListener (() => {
			CmdHop();

		});
			
	}	
		

	private bool IsPointerOverUIObject() 
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
		
}
	

