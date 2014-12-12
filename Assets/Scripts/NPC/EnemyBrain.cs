using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

	RaycastHit visionHit;
	public GameObject gO;

//	Dictionary<string, RaycastHit> _wallHits = new Dictionary<string, RaycastHit>;

	float unitSpeed = 0f;
	Vector3 unitPos = Vector3.zero;
	float heading = 10f;

	//Merge all raycasts into dicionary, or array or something and romove duplicates
	//vision raycasts
	RaycastHit centerVisRH;
	RaycastHit rightPeRH;
	RaycastHit leftPeRH;

	//wall raycasts

	RaycastHit frontWallRH;
	RaycastHit rightWallRH;
	RaycastHit backWallRH;
	RaycastHit leftWallRH;

	float moveSpeed = 0.01f;

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
	void Update (){
		VisionReciever();
		Navigation();
	}

	void VisionReciever(){
		centerVisRH = NPCAI.instance.VisionControl(gO);
		NPCAI.instance.PeripheralControl(gO, out leftPeRH, out rightPeRH);
	}

	void Navigation(){//Start making some if statements to direct how navigation works
		//NEED TO INCORPERATE ROTATION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//Navigation needs to move npc, rotate npc, detect if stuck, turn around, stop once in a while, look around, move towards other visible units.
		// f, fR, fL, r, l, b, bR, bL, na
		string wallDir = NPCAI.instance.WallAwareness(gameObject);
		heading = NPCAI.instance.IsWalled(gO);
		if(stop == false){
			if(IsStuck() == false){
				NPCAI.instance.MoveControl(gO, gameObject.transform.up, moveSpeed);
				if(wallDir != "na"){
					NPCAI.instance.RotationControl(gO, heading);
					return;
				}
			}
			else if(IsStuck() == true){
//				heading = IsWalled();
				NPCAI.instance.MoveControl(gO, -gameObject.transform.up, moveSpeed);
				NPCAI.instance.RotationControl(gO, heading);
				return;
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
	
}
