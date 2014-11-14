using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

	RaycastHit visionHit;
	public GameObject gO;

	float unitSpeed = 0f;
	Vector3 unitPos = Vector3.zero;

	//vision raycasts
	RaycastHit centerVisRH;
	RaycastHit rightPeRH;
	RaycastHit leftPeRH;

	//wall raycasts
	RaycastHit frontWallRH;
	RaycastHit rightWallRH;
	RaycastHit backWallRH;
	RaycastHit leftWallRH;

	bool motion = false;
	Vector2 motionDir = new Vector2(0, 0); //0 none, 1 forward, 2 right...
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
	/// </summary>


	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		Navigation();
	}

	void VisionReciever(){
		centerVisRH = NPCAI.instance.VisionControl(gO);
		NPCAI.instance.PeripheralControl(gO, out leftPeRH, out rightPeRH);
	}

	void Navigation(){//Start making some if statements to direct how navigation works
		if(stuck == false){
			NPCAI.instance.MoveControl(gO, 1.1f);
		}
		else if(stuck == true){
			WallAwareness();
			//need to turn the wallDir variable into a movement direction.
		}
		//

	}
	void IsStuck(){
		unitSpeed = NPCAI.instance.SpeedCheck(gO);
		if(unitSpeed == 0){
			stuck = true;
		}
	}
	void WallAwareness(){
		NPCAI.instance.WallCheck(gO, out frontWallRH, out rightWallRH, out backWallRH, out leftWallRH);
		if(frontWallRH.distance < 0.5f)wallDir = 1;
		else if(frontWallRH.distance > 0.5f)wallDir = 0;
		else wallDir = 0;
		if(rightWallRH.distance < 0.5f)wallDir = 2;
		else if(frontWallRH.distance > 0.5f)wallDir = 0;
		else wallDir = 0;
		if(backWallRH.distance < 0.5f)wallDir = 3;
		else if(frontWallRH.distance > 0.5f)wallDir = 0;
		else wallDir = 0;
		if(leftWallRH.distance < 0.5f)wallDir = 4;
		else if(frontWallRH.distance > 0.5f)wallDir = 0;
		else wallDir = 0;
	}
	void MotionAwareness(){
		if(gameObject.transform.position.y > unitPos.y)motionDir.y = 1;
		if(gameObject.transform.position.y < unitPos.y)motionDir.y = 2;
		if(gameObject.transform.position.x > unitPos.x)motionDir.x = 3;
		if(gameObject.transform.position.x < unitPos.x)motionDir.x = 4;
		if(gameObject.transform.position.x == unitPos.x || gameObject.transform.position.y == unitPos.y)motion = new Vector2(0, 0);
	}
}
