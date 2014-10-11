using UnityEngine;
using System.Collections;

public class StructureInfo : UnitTemplate {
	public static StructureInfo instance;
	

	// Use this for initialization
	void Awake() {
		instance = this;
		unitGroup = "STRUCTURE";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
