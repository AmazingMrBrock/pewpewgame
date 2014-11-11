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

	Vector3 rotationDir;

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

	public void PeripheralControl(GameObject gO){
		//Peripheral lines
//		Vector3 leftDir = gO.transform.up + -gO.transform.right;
//		Vector3 rightDir = gO.transform.up + gO.transform.right;

		Vector3 leftDir = gO.transform.TransformDirection(Vector3.up) + gO.transform.TransformDirection(-Vector3.right);
		Vector3 rightDir = gO.transform.TransformDirection(Vector3.up) + gO.transform.TransformDirection(Vector3.right);

		Ray leftRay = Raycaster.instance.TargetRay(gO, leftDir);
		Ray rightRay = Raycaster.instance.TargetRay(gO, rightDir);

		RaycastHit leftSide;
		RaycastHit rightSide;

		Physics.Raycast(leftRay, out leftSide, 100);
		Physics.Raycast(rightRay, out rightSide, 100);

//		Debug.Log ("Left Side Dist: " + leftSide.distance + "Right Side Dist: " + rightSide.distance);
		Debug.Log ("Left vector; " + leftDir + "Right vector: " + rightDir);
		if(leftSide.distance > rightSide.distance) rotationDir = rightSide.point;
		if(rightSide.distance > leftSide.distance) rotationDir = leftSide.point;
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
		//Works but screws up raycasts
		float angle = Mathf.Atan2(rotationDir.y, rotationDir.x) * Mathf.Rad2Deg;
		Quaternion targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
		gO.transform.rotation = Quaternion.Slerp(gO.transform.rotation, targetRotation, Time.deltaTime * 2.0f);

		//works on weird angle stuff.
//		Vector3 direction; // I think you know how to set this 
//		float speed = 2.0f;
//		gO.transform.eulerAngles = new Vector3(Mathf.Lerp(gO.transform.eulerAngles.x, rotationDir.x, speed), Mathf.Lerp(gO.transform.eulerAngles.y, rotationDir.y, speed), gO.transform.eulerAngles.z);
//		gO.transform.eulerAngles = new Vector3(gO.transform.eulerAngles.x, gO.transform.eulerAngles.y, Mathf.Lerp(gO.transform.eulerAngles.z, rotationDir.z, speed));


		//could be made to work still screws up the angle business.
//		Vector3 rot = gO.rigidbody.rotation.eulerAngles;
//		rot.z = 45f;
//		gO.rigidbody.rotation = Quaternion.Euler(rot);
	}
}
