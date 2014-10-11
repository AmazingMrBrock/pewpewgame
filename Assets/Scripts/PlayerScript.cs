using UnityEngine;
using System.Collections;

public class PlayerScript : UnitTemplate {
public static PlayerScript instance;

	/// <summary>
	/// TODO
	/// Movement controls
	/// raycasting
	/// weapon
	/// TOREAD
	/// 
	/// </summary>

	public GameObject player;
	public AudioClip attackSound;
	bool actionOn;

	void Awake(){
		instance = this;
		Physics.gravity = new Vector3(0, 0, -1);
		speedMax = 2f;
		acceleration = 2f;
		unitGroup = "Player";

	}

	void Update(){
		CameraMover.instance.SetTarget(transform);

	}

	void FixedUpdate (){
		velocity = new Vector2(Mathf.Max(velocity.x = acceleration, -speedMax), Mathf.Max(velocity.y = acceleration, -speedMax));
		ControlManager();
	}

	void ControlManager(){
		float horizontalAxis = Input.GetAxisRaw("Horizontal");
		float verticalAxis = Input.GetAxisRaw("Vertical");
		float fire1 = Input.GetAxisRaw("Fire1");
		if( horizontalAxis != 0 || verticalAxis != 0){
			DirectionalMovement.instance.Move(horizontalAxis, verticalAxis, gameObject, speedMax);
		}
		if(fire1 != 0){
			Action.instance.Attack(new Vector3(2.4f, 5f, 1f), new Vector3(.8f, 10f), "ENEMY", player, attackSound);
//			Debug.Log("Attack fired by player");
		}
	}

	void DebugLogger(){
//		Debug.Log("Mouse position: " + mouseHit);
//		Debug.Log("Unit position: " + unitPos);
//		Debug.Log("Mouse ray layer: " + hitInfo.collider);
//		Debug.Log(horizontalAxis);
	}
}