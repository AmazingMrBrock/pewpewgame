using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

	RaycastHit distanceToObj;
	public GameObject gO;

	bool reverse = false;

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
		NPCAI.instance.speedCheck(gO);
	}

	void Navigation(){//Start making some if statements to direct how navigation works

		Debug.Log("reverse: " + reverse);
		Debug.Log ("distance to object: " + distanceToObj.distance);	
		distanceToObj = NPCAI.instance.VisionControl(gO);

		if(distanceToObj.distance < 0.6f){
			NPCAI.instance.RotationControl(gO);
			if(NPCAI.instance.speedCheck(gO) == 0){
				reverse = true;
				NPCAI.instance.RotationControl(gO);
			}
		}
		if(distanceToObj.distance > 0.6f){

			if(reverse == false) {
				NPCAI.instance.MoveControl(gO, 0.2f);
			}
			if(reverse == true){ 
				NPCAI.instance.MoveControl(gO, -0.2f);
				NPCAI.instance.RotationControl(gO);
				if(distanceToObj.distance > 1.6f)reverse = false;
			}
		}
	}
}
