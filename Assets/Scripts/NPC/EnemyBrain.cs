using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

	RaycastHit distanceToObj;
	public GameObject gO;

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
	void FixedUpdate () {
		Navigation();
		NPCAI.instance.speedCheck(gO);
	}

	void Navigation(){//Start making some if statements to direct how navigation works
		bool reverse = false;
		Debug.Log("reverse: " + reverse);
		Debug.Log ("distance to object: " + distanceToObj.distance);	
		distanceToObj = NPCAI.instance.VisionControl(gO);

		if(distanceToObj.distance < 1f){
			NPCAI.instance.RotationControl(gO);
			if(NPCAI.instance.speedCheck(gO) == 0){
				reverse = true;
			}
		}
		if(distanceToObj.distance > 1f){

			if(reverse == false) {
				NPCAI.instance.MoveControl(gO, 0.2f);
			}
			if(reverse == true){ 
				NPCAI.instance.MoveControl(gO, -0.2f);
				if(distanceToObj.distance > 3)reverse = false;
			}
		}

	}


}
