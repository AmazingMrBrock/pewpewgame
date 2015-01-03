using UnityEngine;
using System.Collections;

public class NPCNavigator : MonoBehaviour {

	/// <summary>
	/// Creates 12 raycasts out from the npc arranged like hours on a clock.
	/// will rotate and walk towards ray hits depending on distance.
	/// </summary>

	RaycastHit[] hitDir = new RaycastHit[12];
	Vector3[] rayDir = new Vector3[12];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SomeRays(){
		for(int i = 0; i1 < hitDir.Length; i1++){
			hitDir(i) = Raycaster.instance.GetTarget(rayDirection[i], gameObject);
		}
	}
	void GetDirs(){

	}
}
