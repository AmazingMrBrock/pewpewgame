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

	Vector3 distanceTo;
	RaycastHit targetInfo;


	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		velocity = new Vector2(Mathf.Max(velocity.x = acceleration, -speedMax), Mathf.Max(velocity.y = acceleration, -speedMax));
	}

	void VisionControl(){
		distanceTo = Raycaster.instance.PointChecker("WALL", sightRange, gameObject);
		targetInfo = Raycaster.instance.GetTarget("null", sightRange, gameObject);
//		Debug.Log ("Distance Info: " + distanceTo + " Target Info: " + targetInfo);
	}

	void WalkCycle(){
		VisionControl();
	}

	GameObject CharacterWatcher(){//targetInfo is watched, and returns any gameobject that is a character.

	}

	void NavWatcher(){//returns target info if game object is a wall, door, pit, etc. 

	}

	void ItemWatcher(){//returns target info if gameobject is an item. Checks if its weapon, health, other, etc

	} 

	int StatChecker(){//Checks units own health, energy, status, etc

	}

	void SelfClassChecker(){//checks units own class code to determine move set. May use dictionary for Move sets

	}

	void OtherClassChecker(){//Checks class of other characters to determine how to engage. Friend, foe, magic, melee, ranged, etc

	}
}
