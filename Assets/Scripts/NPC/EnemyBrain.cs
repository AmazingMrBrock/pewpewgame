using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {
	public static EnemyBrain instance;

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

	void Navigation(){
		NPCAI.instance.VisionControl(gameObject);
		NPCAI.instance.MoveControl(gameObject);
		NPCAI.instance.RotationControl(gameObject);
	}
}
