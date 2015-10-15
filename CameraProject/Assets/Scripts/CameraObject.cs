using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	[SerializeField] GameObject self;
	[SerializeField] GameObject controller;
	Vector2 difference;

	public enum State{
		still,
		locked
	}
	public State state;

	// Use this for initialization
	void Start () {
		Renderer rend = GetComponent<Renderer>();
		rend.material.color = Color.blue;
		difference = new Vector2 (0, 0);
		state = State.still;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space") && 
		    (difference.x < 10.0f && difference.x > -10.0f) && 
		    (difference.y < 6.0f && difference.y > -6.0f)) {
			state = State.locked;
		}
		if (Input.GetKeyUp ("space")){
			state = State.still;
		}
		if (state == State.locked) {
			self.transform.position = new Vector3 (controller.transform.position.x + difference.x, controller.transform.position.y + difference.y, 0.0f);
		} else {
			self.transform.position = new Vector3 (self.transform.position.x, self.transform.position.y, 16.0f);
			difference = new Vector2 (self.transform.position.x - controller.transform.position.x,
			                                  self.transform.position.y - controller.transform.position.y);
		}
	}
}
