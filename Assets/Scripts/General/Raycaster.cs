using UnityEngine;
using System.Collections;

public class Raycaster : MonoBehaviour {
	public static Raycaster instance;

	/// <summary>
	/// TODO:
	/// 
	/// Re arrange raycastfire and how it deals with target layer issues. Consider moving to tags.
	/// 
	/// CURRENT BUGS:
	/// 
	/// 
	/// INSTRUCTIONS:
	/// 
	/// Hit detection features are activated by using a method call similar to this one
	/// it will return the appropriate variable value.
	/// Raycaster.instance.Whatever(layer, hitDist, gO); 
	/// 
	/// </summary>


	
	bool hitResponse = false;
	float dCheck;
	float hitDistance;
	Vector3 hitPoint;

	public static RaycastHit[] hitInfo = new RaycastHit[18]; //the size of the array decides how many rays to fire.
	
	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

	// Mouse Click Ray
	public RaycastHit MouseRay(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit mouseHit;
		Physics.Raycast(ray, out mouseHit, 1 << LayerMask.NameToLayer("rayMask"));
//		Debug.Log ("mousehit point; " + mouseHit.point);
		
		return mouseHit;
	}

	//Target Ray
	public Ray TargetRay(GameObject gO){
		Vector3 actionVector = MouseRay().point; 
		actionVector.x = actionVector.x - gO.transform.position.x;
		actionVector.y = actionVector.y - gO.transform.position.y;
		actionVector.z = 0;
		Ray ray = new Ray(gO.transform.position, actionVector);
		Debug.DrawRay(gO.transform.position, actionVector, Color.red, 1f);

		return ray;
	}

	//Hit detection	

	public float DistanceCheck(float hitDistance, GameObject gO){
		Vector3 origin = gO.transform.position;
		dCheck = 0f;
		RaycastLoop(origin, hitDistance, gO);
		return hitDistance;
	}

	public Vector3 PointChecker(float hitDistance, GameObject gO){
		Vector3 origin = gO.transform.position;
		hitPoint = new Vector3(0, 0, 0);
		RaycastLoop(origin, hitDistance, gO);
		return hitPoint;
	}

	public bool HitChecker(float hitDistance, GameObject gO){
		Vector3 origin = gO.transform.position;
		hitResponse = false;
		RaycastLoop(origin, hitDistance, gO);
//		Debug.Log("Layer check " + layer);
		return hitResponse;
	}

	public RaycastHit GetTarget(Vector3 direction, GameObject gO){
		Vector3 origin = gO.transform.position;
		RaycastHit newHit = RaycastFire(Vector3.up, origin);
//		Debug.Log("Target distance: " + newHit.distance + " hitDistance: " + hitDistance);
//		Debug.Log("Target name: " + newHit.collider);
		return newHit;
	}

	void HitCheck(float hitDistance, int i, RaycastHit rH){
		hitResponse = false;
		hitPoint = new Vector3(0, 0, 0);
		dCheck = 0f;
		hitDistance = rH.distance;
		if(rH.distance != 0 && rH.distance <= hitDistance){
			hitResponse = true;
			hitPoint = rH.point;
			dCheck = rH.distance;
//			Debug.Log("RayNumber: " + i + " Hit distance: " + rH.distance + " Hit colission: " + rH.collider);
		}
		else if(rH.distance > hitDistance) {
			hitResponse = false;
			hitPoint = rH.point;
			dCheck = rH.distance;
		}
		else hitResponse = false;
	}

	//Raycast Engine

	void RaycastLoop(Vector3 origin, float hitDistance, GameObject gO){
		Vector3 vec = Vector3.up;
		float a = 0f;
		for(int i = 0; i < hitInfo.Length; i ++){
			a = + 19f;
			vec = Quaternion.AngleAxis(a, Vector3.forward) * vec;
			hitInfo[i] = RaycastFire(vec, origin);
			HitCheck(hitDistance, i, hitInfo[i]);
		}
	}
	 
	RaycastHit RaycastFire(Vector3 dir, Vector3 origin){
		RaycastHit hitIn;
		Physics.Raycast(origin, dir, out hitIn, 100); 
		Debug.DrawRay(origin, dir, Color.cyan, 1f);
		return hitIn;
	}

	void DebugLogger(Vector3 dir, Vector3 ori, bool hit){
		if(!hit){Debug.DrawRay(ori, dir, Color.cyan);}
		if(hit){Debug.DrawRay(ori, dir, Color.yellow);}

		if(!hit){Debug.DrawRay(ori, dir, Color.cyan);}
		if(hit){Debug.DrawRay(ori, dir, Color.red);}
	}
}
