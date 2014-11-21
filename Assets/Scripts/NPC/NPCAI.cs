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
	RaycastHit rightPeRH;
	RaycastHit leftPeRH;
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
	//NEED TO COMBINE VISION THINGS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	public RaycastHit VisionControl(GameObject gO){
		Ray ray = Raycaster.instance.TargetRay(gO, gO.transform.up);

		//Center Line
//		targetInfo = Raycaster.instance.GetTarget(Vector3.up, gameObject);
		Physics.Raycast(ray, out targetInfo, 100);

//		Debug.Log ("Rotation direction: " + rotationDir);
		return targetInfo;
	}

	public void PeripheralControl(GameObject gO, out RaycastHit leftSide, out RaycastHit rightSide){
		//Peripheral lines

		Vector3 leftDir = gO.transform.TransformDirection(Vector3.up) + gO.transform.TransformDirection(-Vector3.right);
		Vector3 rightDir = gO.transform.TransformDirection(Vector3.up) + gO.transform.TransformDirection(Vector3.right);

		Ray leftRay = Raycaster.instance.TargetRay(gO, leftDir);
		Ray rightRay = Raycaster.instance.TargetRay(gO, rightDir);

		Physics.Raycast(leftRay, out leftSide, 100);
		Physics.Raycast(rightRay, out rightSide, 100);
	}
	
	void ObjRecognition(){
		//Checks whats under the target ray.
		string objTag = targetInfo.transform.tag;
//		Debug.Log ("target name: " + targetInfo.transform.name + " Target tag: " + objTag);
		
	}
	
	public void MoveControl(GameObject gO, Vector3 moveTo, float speed){
		//need to pass a different target to the movement statement. 
		//need to set up some sort of trigger system to decide which target to pass no the statement.
		Vector3 heading = gameObject.transform.TransformDirection(moveTo); 
		Debug.Log ("heading " + heading);
		//send a raycast down heading.

		Ray headingRay = Raycaster.instance.TargetRay(gO, heading);
		RaycastHit headingRH;
		Physics.Raycast(headingRay, out headingRH, 100);

		DirectionalMovement.instance.MoveToPoint(headingRH.point, speed, gO);//Works well. Any speed over .1 is too fast
	}

	public void RotationControl(GameObject gO, Vector3 rotDir){ //for direction 0 is right left is 1
		//Rotates toward the target
		//Works but screws up raycasts
		float angle = Mathf.Atan2(leftPeRH.point.y, rightPeRH.point.x) * Mathf.Rad2Deg;
		Quaternion targetRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
		gO.transform.rotation = Quaternion.Slerp(gO.transform.rotation, targetRotation, Time.deltaTime * 2.0f);
	}

	public float SpeedCheck(GameObject gO){
		float speed = (gO.transform.position - lastPosition).magnitude;
		lastPosition = gO.transform.position;

//		Debug.Log ("speed = " + speed);
		return speed;
	}

	public void WallCheck(GameObject gO, out RaycastHit wallForw, out RaycastHit wallRight, out RaycastHit wallBack, out RaycastHit wallLeft){
		Ray frontRay = Raycaster.instance.TargetRay(gO, gO.transform.TransformDirection(Vector3.up));
		Ray rightRay = Raycaster.instance.TargetRay(gO, gO.transform.TransformDirection(Vector3.right));
		Ray backRay = Raycaster.instance.TargetRay(gO, gO.transform.TransformDirection(Vector3.down));
		Ray leftRay = Raycaster.instance.TargetRay(gO, gO.transform.TransformDirection(Vector3.left));

		RaycastHit frontRH;
		RaycastHit rightRH;
		RaycastHit backRH;
		RaycastHit leftRH;

		Physics.Raycast(frontRay, out wallForw, 100f);
		Physics.Raycast(rightRay, out wallRight, 100f);
		Physics.Raycast(backRay, out wallBack, 100f);
		Physics.Raycast(leftRay, out wallLeft, 100f);
	}
	public int WallAwareness(GameObject gO){
		RaycastHit frontWallRH;
		RaycastHit rightWallRH;
		RaycastHit backWallRH;
		RaycastHit leftWallRH;
		int wallDir = 0; //0 none, 1 forward, 2 right...
		WallCheck(gO, out frontWallRH, out rightWallRH, out backWallRH, out leftWallRH);

		if(frontWallRH.distance < 0.55f){
			wallDir = 1; 
			return wallDir;
		}
		if(rightWallRH.distance < 0.55f){
			wallDir = 2;
			return wallDir;
		}
		if(backWallRH.distance < 0.55f){
			wallDir = 3;
			return wallDir;
		}
		if(leftWallRH.distance < 0.55f){
			wallDir = 4;
			return wallDir;
		}
		if(frontWallRH.distance > 0.55f){
			wallDir = 0;
			return wallDir;
		}
		else wallDir = 0;
		return wallDir;
//		Debug.Log ("frontwallrh " + frontWallRH.distance + "rightwallrh " + rightWallRH.distance + "backwallrh " + backWallRH.distance + "rightwallrh " + rightWallRH.distance);	
//		Debug.Log ("walldir " + wallDir);

	}
}
