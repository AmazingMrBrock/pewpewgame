using UnityEngine;
using System.Collections;

public class CurrentStats : MonoBehaviour {
	public static CurrentStats instance;


	float health;
	float energy;
	float strength;
	float intelligence;
	float dexterity;
	float endurace;
	float charisma;

	bool dead;

	public AudioClip death;


	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		ZeroHealth();
	}

	public void SetBaseStats(float baseHealth, float baseEnergy, float baseStrength, float baseIntelligence, float baseDexterity, float baseEndurace, float baseCharisma){
		health = baseHealth;
		energy = baseEnergy;
		strength = baseStrength;
		intelligence = baseIntelligence;
		dexterity = baseDexterity;
		endurace = baseEnergy;
		charisma = baseCharisma;
	}

	public void RecieveDamage(float damageIn){
		float tempHealth = health;
		tempHealth = tempHealth - damageIn;
		if(tempHealth < 0)tempHealth = 0;
		health = tempHealth;
		Debug.Log ("current health: " + health);
	}

	void ZeroHealth(){
		if(health <= 0 && !dead){
//			Debug.Log("DEATH DEATH DEATH DEATH DEATH");
			dead = true;
			StartCoroutine(Death());
		}
	}

	IEnumerator Death(){
		audio.PlayOneShot(death); //object is destroyed before sound is played. Consider using a coroutine. Maybe set up a dedicated sound script
		yield return new WaitForSeconds(1f);
		DestroyObject(gameObject);
		StopCoroutine(Death());
	}
}
