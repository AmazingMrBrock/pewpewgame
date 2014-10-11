using UnityEngine;
using System.Collections;

public class UnitTemplate : MonoBehaviour {

	/// <summary>
	/// Is a template that units can draw base stats
	/// and skills from.
	/// 
	/// Health
	/// is calculate by taking a base health stat, adding item/aug modifiers, and adding it to a number derived from
	/// between strength and endurance.
	/// 
	/// Energy
	/// is calculated by taking a base stat, adding item/aug modifiers, and adding it to a number derived from
	/// intelligence and endurance.
	/// 
	/// Strength
	/// is derived from a base stat combined with item/aug modifiers and level additions
	/// adds 3 to health for each strength point
	/// 
	/// Intelligence
	/// is derived from a base stat combined with item/aug modifiers and level additions
	/// adds 5 to energy for each point.
	/// 
	/// Dexterity
	/// is derived from a base stat combined with item/aug modifiers and level additions
	/// 
	/// Endurance
	/// is derived from a base stat combined with item/aug modifiers and level additions
	/// adds 5 to health for each endurance point
	/// adds 3 to energy for each point
	/// 
	/// Charisma
	/// is derived from a base stat combined with item/aug modifiers and level additions
	/// 
	/// 
	/// </summary>

	#region BASE STATS
	//Base Stats
	protected float healthBase = -1;
	protected float energyBase = -1;
	protected float strengthBase = -1;
	protected float intelligenceBase = -1;
	protected float dexterityBase = -1;
	protected float enduranceBase = -1;
	protected float charismaBase = -1;
	#endregion

	#region STAT MODIFIERS
	//Stat modifiers
	protected float healthMod = -1;
	protected float energyMod = -1;
	protected float strengthMod = -1;
	protected float intelligenceMod = -1;
	protected float dexterityMod = -1;
	protected float enduranceMod = -1;
	protected float charismaMod = -1;

	//Level Modifiers
	protected float strengthLevel = -1;
	protected float intelligenceLevel = -1;
	protected float dexterityLevel = -1;
	protected float enduranceLevel = -1;
	protected float charismaLevel = -1;
	#endregion

	#region OPTIONS
	//Options
	protected bool augmented;
	protected bool cyberTech;
	protected bool biotech;
	protected bool psitech;
	protected bool robot;
	protected int gender = -1; //0 n/a, 1 m, 2 f, 3 t 
	protected int unitSize = -1; //0 n/a, 1 s, 2 m, 3 l, 4 xl
	protected string unitName;
	protected string characterName; //Two identical units may have different names for story purposes.
	#endregion

	#region MOVEMENT
	//Movement Variables
	protected float speedBase = -1f;
	protected float speedMod = -1f;
	protected float speedMax = -1f;
	protected float acceleration = -1f;
	protected Vector3 unitPos;
	protected Vector3 velocity;
	protected string unitGroup; //The layer the unit exists in
	#endregion

	//Action Variables
	protected string target;

	//Inventory Variabels


	//Misc Variables


	#region STAT CALCULATORS
	//Stats
	protected float Health(){
		float statHealth = healthBase + (Strength() * 3) + (Endurance() * 5);
		float totalHealth = statHealth + healthMod;

		return totalHealth;
	}

	protected float Energy(){
		float statEnergy = energyBase + (Endurance() * 2) + (Intelligence() * 5);
		float totalEnergy = statEnergy + energyMod;

		return totalEnergy;
	}

	protected float Strength(){
		float totalStrength = strengthBase + strengthLevel + strengthMod;

		return totalStrength;
	}

	protected float Intelligence(){
		float totalIntelligence = intelligenceBase + intelligenceLevel + intelligenceMod;

		return totalIntelligence;
	}

	protected float Dexterity(){
		float totalDexterity = dexterityBase + dexterityLevel + dexterityMod;

		return totalDexterity;
	}

	protected float Endurance(){
		float totalEndurance = enduranceBase + enduranceLevel + enduranceMod;

		return totalEndurance;
	}

	protected float Charisma(){
		float totalCharisma = charismaBase + charismaLevel + charismaMod;

		return totalCharisma;
	}
	#endregion

}
