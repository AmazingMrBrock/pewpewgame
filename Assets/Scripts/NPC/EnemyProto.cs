using UnityEngine;
using System.Collections;

public class EnemyProto : UnitTemplate {
	public static EnemyProto instance;

	/// <summary>
	/// A basic enemy unit script to handle vitals and whatnot
	/// 
	/// TODO
	/// 
	/// Set base stats
	/// 
	/// </summary>



	// Use this for initialization
	void Awake () {
		instance = this;
		__BaseStats();
		__Options();
	}
	
	// Update is called once per frame
	void Update () {
		__MovementVariables();
	}

	void FixedUpdate(){
		velocity = new Vector2(Mathf.Max(velocity.x = acceleration, -speedMax), Mathf.Max(velocity.y = acceleration, -speedMax));
		unitPos.x = transform.position.x; unitPos.y = transform.position.y;
	}
	

	void __BaseStats(){
		healthBase = 30;
		energyBase = 50;
		strengthBase = 5;
		intelligenceBase = 5;
		dexterityBase = 5;
		enduranceBase = 5;
		charismaBase = 5;
		speedBase = 2f;
		speedMax = 2f;
		acceleration = 1f;
		gameObject.GetComponent<CurrentStats>().SetBaseStats(healthBase, energyBase, strengthBase, intelligenceBase, dexterityBase, enduranceBase, charismaBase);
	}
	
	void __MovementVariables(){
		//Set up this function to calculate velocity

		unitPos = transform.position;
		velocity = rigidbody.velocity;
		unitGroup = "ENEMY";
	}

	void __Options(){
		gender = 0;
		unitSize = 1;
		unitName = "TestBox";
		characterName = "Testy THE TESTBOX";
	}
}
