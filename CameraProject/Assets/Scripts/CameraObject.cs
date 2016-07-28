using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	//For setting grabbing self and Camera -- Definitely easier way to do this I just forgot it
	[SerializeField] GameObject self;
	GameObject controller;

    BoxCollider2D boxCollider;

	//Limit X and Y coordinates to a box so they can't just pull objects to anywhere They'd like
	[SerializeField] float limitXLeft;
	[SerializeField] float limitXRight;
	[SerializeField] float limitYUp;
	[SerializeField] float limitYDown;
    [SerializeField] bool interactWhileLocked;


    //Hold onto initial location of object so we can calculate if it's out of its box
    Vector2 initialLocation;

	//Holds onto where the object is on the screen
	Vector2 difference;

	//Is the object locked to the screen or not? Used so objects don't become locked when they appear on screen and space is being held
	public enum State{
		still,
		locked
	}
	public State state;

    SpriteRenderer Mesh;

	// Use this for initialization
	void Start () {
        controller = GameObject.FindGameObjectWithTag("MainCamera");
		//initialize vectors and state
		difference = new Vector2 (0, 0);
		state = State.still;
		initialLocation = self.transform.position;

		//Force the limits to be negative or positive. Just error handler for my math
		limitXLeft = -Mathf.Abs (limitXLeft);
		limitXRight = Mathf.Abs (limitXRight);
		limitYUp = Mathf.Abs (limitYUp);
		limitYDown = -Mathf.Abs (limitYDown);

        Mesh = self.GetComponent<SpriteRenderer>();
        boxCollider = self.GetComponent<BoxCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {
		//Upon press of space lock all objects on screen to camera
		if (Input.GetButton("Action Camera") && 
		    (difference.x < 10.0f && difference.x > -10.0f) && 
		    (difference.y < 6.0f && difference.y > -6.0f)) {
			state = State.locked;
		}
		//Upon release of space release all objects locked to camera
		else if (!Input.GetButton ("Action Camera")){
			state = State.still;
		}
		//if we are locked move the object based on camera movement
		if (state == State.locked) {
            // The z is 0.0f so that the player is not affected by the block's movement
            //Perhaps turn off collider and mesh or make it appear transparent as to make it more obvious for the player
            if (interactWhileLocked)
            {
                self.transform.position = new Vector3(controller.transform.position.x + difference.x, controller.transform.position.y + difference.y,0.00f);
            } else
            {
                Mesh.enabled = false;
                boxCollider.enabled = false;
                self.transform.position = new Vector3(controller.transform.position.x + difference.x, controller.transform.position.y + difference.y, 1.00f);
            }
			//Limit the object to its zone
			if(initialLocation.x + limitXRight < self.transform.position.x) {
				self.transform.position = new Vector2(initialLocation.x + limitXRight, self.transform.position.y);
			}
			if(initialLocation.x + limitXLeft > self.transform.position.x) {
				self.transform.position = new Vector2(initialLocation.x + limitXLeft, self.transform.position.y);
			}
			if(initialLocation.y + limitYUp < self.transform.position.y) {
				self.transform.position = new Vector2(self.transform.position.x, initialLocation.y + limitYUp);
			}
			if(initialLocation.y + limitYDown > self.transform.position.y) { 
				self.transform.position = new Vector2(self.transform.position.x, initialLocation.y + limitYDown);
			}
		} else { // if we are not locked move the object back to the plane the player is in and calculate difference
			self.transform.position = new Vector2 (self.transform.position.x, self.transform.position.y);
			difference = new Vector2 (self.transform.position.x - controller.transform.position.x,
			                                  self.transform.position.y - controller.transform.position.y);
            Mesh.enabled = true;
            boxCollider.enabled = true;

        }
	}
}
