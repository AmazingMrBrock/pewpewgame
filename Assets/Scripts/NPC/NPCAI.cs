using UnityEngine;
using System.Collections;

public class NPCAI : UnitTemplate {
	public static NPCAI instance;
	
	/// <summary>
	/// Needs to control unit movement and attack
	/// Unit should have a line of site
	/// and follow player if the enter line of site. 
	/// if the player is out of the line of site for a period of time
	/// the unit will no longer follow the player.
	/// it will go back to its pre defined walking route.
	/// 
	/// TODO:
	/// Alter name to unitAI
	/// Create UnitBrain script
	/// Set up view range distance
	/// 
	/// 
	/// 
	/// if it enters into attack range it will attack
	/// other stuff to be added as needed.
	/// </summary>
	
	float distanceFromObject;

	float sightRange = 100;
	
	RaycastHit targetInfo;
	
	
	// Use this for initialization
	void Awake () {
		instance = this;
		Physics.gravity = new Vector3(0, 0, -1);
		speedMax = 2f;
		acceleration = 2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		velocity = new Vector2(Mathf.Max(velocity.x = acceleration, -speedMax), Mathf.Max(velocity.y = acceleration, -speedMax));
		VisionControl();
		ObjRecognition();
	}
	
	void VisionControl(){
		targetInfo = Raycaster.instance.GetTarget("null", sightRange, gameObject);
	}
	
	void ObjRecognition(){
		//Checks whats under the target ray.
		string objTag = targetInfo.transform.tag;
//		Debug.Log ("target name: " + targetInfo.transform.name + " Target tag: " + objTag);
		
	}
	
	public void MoveControl(GameObject gO){
		//obbject referenece bla bla somethings wrong with the gameobject.
		DirectionalMovement.instance.MoveToPoint(targetInfo.point, 2, gO);
	}

	public void RotationControl(GameObject gO, bool direction, Vector3 target){ //for direction 0 is right left is 1
		//Rotates toward the target
		rigidbody.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((target.y - transform.position.y), (target.x - transform.position.x))*Mathf.Rad2Deg - 90);
		
		//Judge the distance from the object and the mouse
		distanceFromObject = target.magnitude;

	}
}
