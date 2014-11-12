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

	Vector3 lastPosition = Vector3.zero;


	float rotationSpeed = 2;

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
	
	public RaycastHit VisionControl(GameObject gO){
		Ray ray = Raycaster.instance.TargetRay(gO, gO.transform.up);

		//Center Line
//		targetInfo = Raycaster.instance.GetTarget(Vector3.up, gameObject);
		Physics.Raycast(ray, out targetInfo, 100);

//		Debug.Log ("Rotation direction: " + rotationDir);
		return targetInfo;
	}

	public RaycastHit PeripheralControl(GameObject gO){
		//Peripheral lines
//		Vector3 leftDir = gO.transform.up + -gO.transform.right;
//		Vector3 rightDir = gO.transform.up + gO.transform.right;
		RaycastHit rotationDir;

		Vector3 leftDir = gO.transform.TransformDirection(Vector3.up) + gO.transform.TransformDirection(-Vector3.right);
		Vector3 rightDir = gO.transform.TransformDirection(Vector3.up) + gO.transform.TransformDirection(Vector3.right);


		Ray leftRay = Raycaster.instance.TargetRay(gO, leftDir);
		Ray rightRay = Raycaster.instance.TargetRay(gO, rightDir);

		RaycastHit leftSide;
		RaycastHit rightSide;

		Physics.Raycast(leftRay, out leftSide, 100);
		Physics.Raycast(rightRay, out rightSide, 100);
		rotationDir = rightSide;
//		Debug.Log ("Left Side Dist: " + leftSide.distance + "Right Side Dist: " + rightSide.distance);

		if(leftSide.distance > rightSide.distance) rotationDir = rightSide;
		if(rightSide.distance > leftSide.distance) rotationDir = leftSide;
		if(leftSide.distance == rightSide.distance) rotationDir = rightSide;

		return rotationDir;
	}
	
	void ObjRecognition(){
		//Checks whats under the target ray.
		string objTag = targetInfo.transform.tag;
//		Debug.Log ("target name: " + targetInfo.transform.name + " Target tag: " + objTag);
		
	}
	
	public void MoveControl(GameObject gO, float speed){
		DirectionalMovement.instance.MoveToPoint(targetInfo.point, speed, gO);//Works well. Any speed over .1 is too fast
	}

	public void RotationControl(GameObject gO){ //for direction 0 is right left is 1
		//Rotates toward the target
		//Works but screws up raycasts
		float angle = Mathf.Atan2(PeripheralControl(gO).point.y, PeripheralControl(gO).point.x) * Mathf.Rad2Deg;
		Quaternion targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
		gO.transform.rotation = Quaternion.Slerp(gO.transform.rotation, targetRotation, Time.deltaTime * 2.0f);
	}

	public float speedCheck(GameObject gO){
		float speed = (gO.transform.position - lastPosition).magnitude;
		lastPosition = gO.transform.position;

//		Debug.Log ("speed = " + speed);
		return speed;
	}
}
