using UnityEngine;
using System.Collections;

public class DirectionalMovement : MonoBehaviour {
	public static DirectionalMovement instance;
	
	float hitDistance = 0;
	
	public Vector2 velocity;
	BoxCollider boxCollider;

	bool walled;

	RaycastHit hitInfo = new RaycastHit();

	// Use this for initialization
	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	}

	public void Move(float hAxis, float vAxis, GameObject gO, float maxSpeed){
		Vector3 hDirection = Vector3.left * hAxis;
		Vector3 vDirection = Vector3.up * vAxis;
		Vector2 velocity = gO.rigidbody.velocity;
		 
		if(hAxis != 0){
			gO.transform.Translate((hDirection * 10) * Time.deltaTime);
			
			if(Mathf.Abs(gO.rigidbody.velocity.x) > maxSpeed){
				velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
			}
		}
		
		if(vAxis != 0){
			gO.transform.Translate((vDirection * 10) * Time.deltaTime);
			if(Mathf.Abs(velocity.x) > maxSpeed){
				gO.rigidbody.velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
			}
		}

	}
}
