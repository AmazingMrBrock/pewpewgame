using UnityEngine;
using System.Collections;

public class EnemyAI : UnitTemplate {
	public static EnemyAI instance;

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

	float sightRange = 100;


	RaycastHit targetInfo;


	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		velocity = new Vector2(Mathf.Max(velocity.x = acceleration, -speedMax), Mathf.Max(velocity.y = acceleration, -speedMax));
		VisionControl();
		ObjRecognition();
	}

	void VisionControl(){

		targetInfo = Raycaster.instance.GetTarget("null", sightRange, gameObject);
//		Debug.Log ("Distance Info: " + distanceTo + " Target Info: " + targetInfo);
	}

	void ObjRecognition(){
		//Checks whats under the target ray.
		string objTag = targetInfo.transform.tag;
		Debug.Log ("target name: " + targetInfo.transform.name + " Target tag: " + objTag);

	}




}
