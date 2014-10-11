using UnityEngine;
using System.Collections;

public class MouseRotate : MonoBehaviour {
	public static MouseRotate instance;
	//Mouse Rotation variables
	public Camera camera;
	public float speed;
	
	//Private Vars
	private Vector3 mousePosition;
	private Vector3 direction;
	private float distanceFromObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RotateToMouse();
	}

	private void RotateToMouse(){   
		// May have to scrap the raycast mouse rotator. Its kinda broken because of local / world space problems I don't know how to fix.
		
		//Grab the current mouse position on the screen
		mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z - camera.transform.position.z));
		
		//Rotates toward the mouse
		rigidbody.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x))*Mathf.Rad2Deg - 90);
		
		//Judge the distance from the object and the mouse
		distanceFromObject = (Input.mousePosition - camera.WorldToScreenPoint(transform.position)).magnitude;
		
		//Move towards the mouse
		rigidbody.AddForce(direction * speed * distanceFromObject * Time.deltaTime);
		
	}
}
