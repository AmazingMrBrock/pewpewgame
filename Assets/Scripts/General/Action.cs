using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour {
	public static Action instance;

	/// <summary>
	/// Handles unit actions. Attacking, healing, activations, pickups, interactions of all sorts.
	/// 
	/// TODO:
	/// Additional actions
	/// Reduce overloads.
	/// </summary>

	RaycastHit target;
	bool actionOn;



	/// <summary>
	/// Handles unit actions. 
	/// From physical and magical attacks to healing and buffs.
	/// Also potions and etc, essentially anything that modifies stats
	/// 
	/// TODO
	/// 
	/// When an action is exectuted it needs to pull the stats from the unit that is hit by the ray.
	/// 
	/// 
	/// </summary>

	// Use this for initialization
	void Awake () {
		instance = this;
		actionOn = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ClearTarget(){
		target = new RaycastHit();
	}


	//SET UP ATTACK SO THAT IT PULLS TARGETING RAYCAST FROM A VISION SUBROUTINE
	public void Attack(Vector3 rangeDamageArea, Vector3 timeDistanceDuration, string layer, GameObject gO, AudioClip soundEffect){
		float finalDamage = 0;
		Ray ray = Raycaster.instance.TargetRay(gO);
//		Debug.Log ("game object location: " + gO.transform.position);
		RaycastHit targetHit;

		if(actionOn == false)
		{
			if (Physics.Raycast(ray, out targetHit, 100, 1 << LayerMask.NameToLayer(layer))){
				GameObject targetGo = targetHit.transform.gameObject;
				Debug.Log("target.distance; " + targetHit.distance);
//				Debug.Log("Attack fired");
				if(targetHit.distance < rangeDamageArea.x){
					Debug.Log("Damage dealt");

					targetGo.GetComponent<CurrentStats>().RecieveDamage(rangeDamageArea.y);
				}
				StartCoroutine(ActionTimer(soundEffect, timeDistanceDuration.x));
//				Debug.Log ("Target gameobject is: " + targetGO);

			}
		}
	}
	IEnumerator ActionTimer(AudioClip soundEffect, float actionTime){
		actionOn = true;
		audio.PlayOneShot(soundEffect);
		yield return new WaitForSeconds(actionTime);
		actionOn = false;
		StopCoroutine(ActionTimer(soundEffect, actionTime));
	}
}
