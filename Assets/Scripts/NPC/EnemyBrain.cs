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

	bool walled = false;



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
		Navigation();
	}

	void VisionReciever(){
		centerVisRH = NPCAI.instance.VisionControl(gO);
		NPCAI.instance.PeripheralControl(gO, out leftPeRH, out rightPeRH);
	}

	void Navigation(){//Start making some if statements to direct how navigation works
		//NEED TO INCORPERATE ROTATION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		if(stop == false){
			if(IsStuck() == false){
				NPCAI.instance.MoveControl(gO, heading, moveSpeed);
			}
			else if(IsStuck() == true){
				heading = IsWalled();
				NPCAI.instance.MoveControl(gO, heading, moveSpeed);
			}
		}
	}
	bool IsStuck(){
		bool stuck = false;
		unitSpeed = NPCAI.instance.SpeedCheck(gO);
		if(unitSpeed == 0){
			stuck = true;
			return stuck;
		}
		else if(unitSpeed > 0.2f){
			stuck = false;
			return stuck;
		}
		return stuck;
//		Debug.Log ("Stuck? " + stuck);
	}
	//not sure if I need this... re evaluate.
//	Vector3 MotionAwareness(){
//		if(gameObject.transform.position.y > unitPos.y)motionDir.y = 1;
//		if(gameObject.transform.position.y < unitPos.y)motionDir.y = 2;
//		if(gameObject.transform.position.x > unitPos.x)motionDir.x = 3;
//		if(gameObject.transform.position.x < unitPos.x)motionDir.x = 4;
//		if(gameObject.transform.position.x == unitPos.x || gameObject.transform.position.y == unitPos.y)motionDir = new Vector2(0, 0);
//	}

	Vector3 IsWalled(){
		Vector3 heading = Vector3.zero;
		int wallDir = NPCAI.instance.WallAwareness(gameObject);
		switch(wallDir){
		case 0:
			heading = gameObject.transform.up;
			break;
		case 1:	
			heading = -gameObject.transform.up;
			break;
		case 2:
			heading = -gameObject.transform.right;
			break;
		case 3:
			heading = gameObject.transform.up;
			break;
		case 4:
			heading = gameObject.transform.right;
			break;
		}
//		Debug.Log ("theres a wall at " + wallDir);
		return heading;
	}
}
