using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	//For setting grabbing self and Camera -- Definitely easier way to do this I just forgot it
	[SerializeField] GameObject self;
	[SerializeField] GameObject controller;

	//Limit X and Y coordinates to a box so they can't just pull objects to anywhere They'd like
	[SerializeField] float limitXLeft;
	[SerializeField] float limitXRight;
	[SerializeField] float limitYUp;
	[SerializeField] float limitYDown;

    public bool isViewedObject;

	//Hold onto initial location of object so we can calculate if it's out of its box
	Vector3 initialLocation;

	//Holds onto where the object is on the screen
	Vector2 difference;

	//Is the object locked to the screen or not? Used so objects don't become locked when they appear on screen and space is being held
	public enum State{
		still,
		locked
	}
	public State state;

	// Use this for initialization
	void Start () {
		//Get the renderer so we can make the objects blue
		Renderer rend = GetComponent<Renderer>();
		rend.material.color = Color.blue;
		//initialize vectors and state
		difference = new Vector2 (0, 0);
		state = State.still;
		initialLocation = self.transform.position;

		//Force the limits to be negative or positive. Just error handler for my math
		limitXLeft = -Mathf.Abs (limitXLeft);
		limitXRight = Mathf.Abs (limitXRight);
		limitYUp = Mathf.Abs (limitYUp);
		limitYDown = -Mathf.Abs (limitYDown);
	}
	
	// Update is called once per frame
	void Update () {
		//Upon press of space lock all objects on screen to camera
		if (Input.GetKeyDown ("space") && 
		    (difference.x < 10.0f && difference.x > -10.0f) && 
		    (difference.y < 6.0f && difference.y > -6.0f)) {
			state = State.locked;
		}
		//Upon release of space release all objects locked to camera
		if (Input.GetKeyUp ("space")){
			state = State.still;
		}
		//if we are locked move the object based on camera movement
		if (state == State.locked) {
			// The z is 0.0f so that the player is not affected by the block's movement
			self.transform.position = new Vector3 (controller.transform.position.x + difference.x, controller.transform.position.y + difference.y, 1.0f);
			//Limit the object to its zone
			if(initialLocation.x + limitXRight < self.transform.position.x) {
				self.transform.position = new Vector3(initialLocation.x + limitXRight, self.transform.position.y, self.transform.position.z);
			}
			if(initialLocation.x + limitXLeft > self.transform.position.x) {
				self.transform.position = new Vector3(initialLocation.x + limitXLeft, self.transform.position.y, self.transform.position.z);
			}
			if(initialLocation.y + limitYUp < self.transform.position.y) {
				self.transform.position = new Vector3(self.transform.position.x, initialLocation.y + limitYUp, self.transform.position.z);
			}
			if(initialLocation.y + limitYDown > self.transform.position.y) { 
				self.transform.position = new Vector3(self.transform.position.x, initialLocation.y + limitYDown, self.transform.position.z);
			}
		} else { // if we are not locked move the object back to the plane the player is in and calculate difference
			self.transform.position = new Vector3 (self.transform.position.x, self.transform.position.y, 0.0f);
			difference = new Vector2 (self.transform.position.x - controller.transform.position.x,
			                                  self.transform.position.y - controller.transform.position.y);
		}
	}

	IEnumerator MoveCube(float inTime)
	{
		Vector3 from = self.transform.position;
		Vector3 to;
		if (state == State.locked) {
			to = new Vector3(self.transform.position.x, self.transform.position.y, 1.0f);;
		} else {
			to = new Vector3(self.transform.position.x, self.transform.position.y, 0.0f);;
		}
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			self.transform.position = Vector3.Lerp(from, to, t);
			yield return null ;
		}
	}
}
