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

	int rotationDir;
	
	
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
	}
	
	public void VisionControl(GameObject gO){
		//Center Line
		targetInfo = Raycaster.instance.GetTarget(Vector3.up, gameObject);


		//Peripheral lines
		RaycastHit leftSide = Raycaster.instance.GetTarget(new Vector3(0.8f, 1, 0), gO);
		RaycastHit rightSide = Raycaster.instance.GetTarget(new Vector3(-0.8f, 1, 0), gO);
//		Debug.Log ("Lefts side: " + leftSide.distance + " Right side: " + rightSide.distance);

		if(leftSide.distance > rightSide.distance) rotationDir = 1;
		if(rightSide.distance > leftSide.distance) rotationDir = -1;

//		Debug.Log ("Rotation direction: " + rotationDir);
	}
	
	void ObjRecognition(){
		//Checks whats under the target ray.
		string objTag = targetInfo.transform.tag;
//		Debug.Log ("target name: " + targetInfo.transform.name + " Target tag: " + objTag);
		
	}
	
	public void MoveControl(GameObject gO){
		DirectionalMovement.instance.MoveToPoint(targetInfo.point, .05f, gO);//Works well. Any speed over .1 is too fast
	}

	public void RotationControl(GameObject gO){ //for direction 0 is right left is 1
		//Rotates toward the target
//		rigidbody.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((target.y - transform.position.y), (target.x - transform.position.x))*Mathf.Rad2Deg - 90);
		gO.transform.rigidbody.MoveRotation(Quaternion.AngleAxis((.5f * rotationDir), Vector3.up));
		//Judge the distance from the object and the mouse
//		distanceFromObject = target.magnitude;
	}
}
