using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

	RaycastHit distanceToObj;

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
		distanceToObj = NPCAI.instance.VisionControl(gameObject);
		NPCAI.instance.PeripheralControl(gameObject);
		if(distanceToObj.distance < 3){
			NPCAI.instance.RotationControl(gameObject);
		}
		if(distanceToObj.distance > 3){
			NPCAI.instance.MoveControl(gameObject);
		}
	}
}
