using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

	RaycastHit visionHit;
	public GameObject gO;

	float unitSpeed = 0f;
	Vector3 unitPos = Vector3.zero;
	Vector3 heading = Vector3.zero;

	//vision raycasts
	RaycastHit centerVisRH;
	RaycastHit rightPeRH;
	RaycastHit leftPeRH;

	//wall raycasts
	RaycastHit frontWallRH;
	RaycastHit rightWallRH;
	RaycastHit backWallRH;
	RaycastHit leftWallRH;

	float moveSpeed = 0.1f;

	bool motion = false;
	Vector2 motionDir = new Vector2(0, 0); //0 none, 1 forward, 2 right...
	bool stop = false;
	bool rotation = false;
	bool rotationDir = false;
	bool stuck = true;
	bool walled = false;
	int wallDir = 0; //0 none, 1 forward, 2 right...


	/// <summary>
	/// Controls AI functions to make a semblance of conscious action.
	/// 
	/// TODO:
	/// Figure out how to organize function controls.
	/// 
	/// get motionDir from gO.transform.up. Do some debug comparisions between it and vector3.up
	/// </summary>


	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		VisionReciever();
		IsStuck();
		MotionAwareness();
		WallAwareness();
		Navigation();
	}

	void VisionReciever(){
		centerVisRH = NPCAI.instance.VisionControl(gO);
		NPCAI.instance.PeripheralControl(gO, out leftPeRH, out rightPeRH);
	}

	void Navigation(){//Start making some if statements to direct how navigation works
		if(stop == false){
			if(stuck == false){
//				NPCAI.instance.MoveControl(gO, Vector3.up, moveSpeed);
			}
			else if(stuck == true){
				WallAwareness();
				//need to turn the wallDir variable into a movement direction.
//				NPCAI.instance.MoveControl(gO, gameObject.transform.up, moveSpeed);
			}
		}
//		Debug.Log ("stop? " + stop);	
//		Debug.Log ("transform.up: " + gameObject.transform.up + "inversetransformpoint: " 
//		           + gameObject.transform.InverseTransformPoint(gameObject.transform.up));
	}
	void IsStuck(){
		unitSpeed = NPCAI.instance.SpeedCheck(gO);
		if(unitSpeed == 0){
			stuck = true;
		}
		else if(unitSpeed > 0.2f){
			stuck = false;
		}
//		Debug.Log ("Stuck? " + stuck);
	}
	void WallAwareness(){
		NPCAI.instance.WallCheck(gO, out frontWallRH, out rightWallRH, out backWallRH, out leftWallRH);
		if(frontWallRH.distance < 0.55f){
			wallDir = 1; // up
			heading = -gameObject.transform.up;
			stop = true;
		}
		else if(frontWallRH.distance > 0.55f){
			wallDir = 0;
			heading = gameObject.transform.up;
			stop = false;
		}
		else wallDir = 0;
		if(rightWallRH.distance < 0.55f){
			wallDir = 2;
			stop = true;
		}
		else if(frontWallRH.distance > 0.55f){
			wallDir = 0;
			stop = false;
		}
		else wallDir = 0;
		if(backWallRH.distance < 0.55f){
			wallDir = 3;
			stop = true;
		}
		else if(frontWallRH.distance > 0.55f){
			wallDir = 0;
			stop = false;
		}
		else wallDir = 0;
		if(leftWallRH.distance < 0.55f){
			wallDir = 4;
			stop = true;
		}
		else if(frontWallRH.distance > 0.55f){
			wallDir = 0;
			stop = false;
		}
		else wallDir = 0;
		Debug.Log ("frontwallrh " + frontWallRH.distance + "rightwallrh " + rightWallRH.distance + "backwallrh " + backWallRH.distance + "rightwallrh " + rightWallRH.distance);	
		Debug.Log ("walldir " + wallDir);
	}
	void MotionAwareness(){
		if(gameObject.transform.position.y > unitPos.y)motionDir.y = 1;
		if(gameObject.transform.position.y < unitPos.y)motionDir.y = 2;
		if(gameObject.transform.position.x > unitPos.x)motionDir.x = 3;
		if(gameObject.transform.position.x < unitPos.x)motionDir.x = 4;
		if(gameObject.transform.position.x == unitPos.x || gameObject.transform.position.y == unitPos.y)motionDir = new Vector2(0, 0);
	}
}
