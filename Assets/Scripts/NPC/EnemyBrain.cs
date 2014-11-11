using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

	RaycastHit distanceToObj;
	GameObject gO;

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
	}

	void Navigation(){//Start making some if statements to direct how navigation works
		bool reverse = false;
		Debug.Log ("distance to object: " + distanceToObj.distance);	
		distanceToObj = NPCAI.instance.VisionControl(gameObject);
		if(distanceToObj.distance < 1f) NPCAI.instance.RotationControl(gameObject);
		
		if(distanceToObj.distance > 1f){
			Debug.Log("reverse: " + reverse);
			if(reverse == false) {
				NPCAI.instance.MoveControl(gameObject, 0.2f); Debug.Log("step 1");
				if(distanceToObj.distance < 1f) reverse = true;
			}
			if(reverse == true){ 
				NPCAI.instance.MoveControl(gameObject, -0.2f); Debug.Log("step 2");
				if(distanceToObj.distance > 1.5f) reverse = false;
			}
		}


	}


}
