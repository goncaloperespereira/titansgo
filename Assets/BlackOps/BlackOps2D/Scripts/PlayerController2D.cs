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
	[SerializeField]
	int storeTempHealth; 


	//bools to check which Golem the player can hop
	public bool canHopGolem1;
	public bool canHopGolem2;
	public bool hoppedGolem1;
	public bool hoppedGolem2;

	private GameObject donkeyGolem;

	public GameObject touchTarget;

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


			//if(golem.layer == this.gameObject.layer)
			//{
				//Reactivate Golem
			donkeyGolem.SetActive (true);
			//get health from player and reassign to goelm
			donkeyGolem.GetComponent<Health>().currentHealth = gameObject.GetComponent<Health>().currentHealth;

			Vector3 newPosition = transform.position + new Vector3 (5.0f, 0.0f, 0.0f);
			donkeyGolem.transform.position = newPosition;

			//}

			//reassign old health to the Goblin
			gameObject.GetComponent<Health> ().currentHealth = storeTempHealth;

			hopButton.GetComponent<Button> ().interactable = false;

			hoppedGolem1 = false;
			donkeyGolem = null;

			//change tag
			gameObject.tag = "Player"; 

		}


		/*if (hopped == true && canHopGolem1 == true)
		{
			

			foreach(GameObject golem in golemArray)
			{

				if(golem.layer == gameObject.layer && golem.tag == "Golem_1")
				{
					//change tag
					gameObject.tag = "Player_Combined"; 

					//Change Sprite and scale to the combined object
					gameObject.GetComponent<SpriteRenderer> ().sprite = gobzillaSprite;
					gameObject.transform.localScale = new Vector3 (6.5f, 6.5f, 6.5f);

					//store current health on a temporary variable
					storeTempHealth = gameObject.GetComponent<Health>().currentHealth;

					//get health from Golem and assign it to Player
					gameObject.GetComponent<Health>().currentHealth = golem.GetComponent<Health>().currentHealth;

					golem.SetActive (false);
				}

			}

			hopButton.GetComponent<Button> ().interactable = true;

			hoppedGolem1 = true;

		}

		if (hopped == true && canHopGolem2 == true)
		{


			foreach(GameObject golem in golemArray)
			{

				if(golem.layer == gameObject.layer && golem.tag == "Golem_2")
				{
					//change tag
					gameObject.tag = "Player_Combined"; 

					//Change Sprite and scale to the combined object
					gameObject.GetComponent<SpriteRenderer> ().sprite = gobzillaSprite;
					gameObject.transform.localScale = new Vector3 (6.5f, 6.5f, 6.5f);

					//store current health on a temporary variable
					storeTempHealth = gameObject.GetComponent<Health>().currentHealth;

					//get health from Golem and assign it to Player
					gameObject.GetComponent<Health>().currentHealth = golem.GetComponent<Health>().currentHealth;

					golem.SetActive (false);
				}
					
			}

			hopButton.GetComponent<Button> ().interactable = true;

			hoppedGolem2 = true;

		}

		if (hopped == false && hoppedGolem1 == true)
		{
			gameObject.GetComponent<SpriteRenderer> ().sprite = goblinSprite;
			gameObject.transform.localScale = new Vector3 (6.5f, 6.5f, 5.0f);

			foreach(GameObject golem in golemArray)
			{

				if(golem.layer == this.gameObject.layer)
				{
					//Reactivate Golem
					golem.SetActive (true);
					//get health from player and reassign to goelm
					golem.GetComponent<Health>().currentHealth = gameObject.GetComponent<Health>().currentHealth;

					Vector3 newPosition = transform.position + new Vector3 (5.0f, 0.0f, 0.0f);
					golem.transform.position = newPosition;

				}

			}

			//reassign old health to the Goblin
			gameObject.GetComponent<Health> ().currentHealth = storeTempHealth;

			hopButton.GetComponent<Button> ().interactable = false;

			hoppedGolem1 = false;

			//change tag
			gameObject.tag = "Player"; 

		}

		if (hopped == false && hoppedGolem2 == true)
		{
			gameObject.GetComponent<SpriteRenderer> ().sprite = goblinSprite;
			gameObject.transform.localScale = new Vector3 (6.5f, 6.5f, 5.0f);

			//Reactivate Golem
			Golem_2.SetActive (true);

			//get health from player and reassign to goelm
			Golem_2.GetComponent<Health>().currentHealth = gameObject.GetComponent<Health>().currentHealth;

			//reassign old health to the Goblin
			gameObject.GetComponent<Health> ().currentHealth = storeTempHealth;



			Vector3 newPosition = transform.position + new Vector3 (5.0f, 0.0f, 0.0f);
			Golem_2.transform.position = newPosition;

			hopButton.GetComponent<Button> ().interactable = false;

			hoppedGolem2 = false;

			//change tag
			gameObject.tag = "Player"; 

		}*/
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


	/*public void OnTriggerEnter2D (Collider2D other)
	{
		if(other.name == "Golem_hoparea" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,255f);
			hopButton.GetComponent<Button> ().interactable = true;
			Debug.Log ("Can combine!");
			canHopGolem1 = true;

		}

		if(other.tag == "Golem_2" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,255f);
			hopButton.GetComponent<Button> ().interactable = true;
			Debug.Log ("Can combine!");
			canHopGolem2 = true;

		}

	}

	public void OnTriggerExit2D (Collider2D other)
	{
		if(other.tag == "Golem_1" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,80f);
			hopButton.GetComponent<Button> ().interactable = false;

			Debug.Log ("Can't combine!");

			canHopGolem1 = false;

		}

		if(other.tag == "Golem_2" && other.gameObject.layer == this.gameObject.layer && gameObject.tag == "Player")	
		{

			hopButton.GetComponent<Image> ().color = new Color(255f,255f,255f,80f);
			hopButton.GetComponent<Button> ().interactable = false;

			Debug.Log ("Can't combine!");
			canHopGolem2 = false;

		}

	}*/
		
}
	

